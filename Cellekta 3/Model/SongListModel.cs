using HarmonicKeyImplementation;
using HarmonicKeyInterface;
using MixableRangeImplementation;
using MixableRangeInterface;
using SongInterface;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using TraktorLibraryInterface;
using XmlWrapperInterface;

namespace Cellekta_3.Model
{
    public class SongListModel : ISongListModel
    {
        private IXmlWrapper _xmlWrapper;

        public ITraktorLibrary TraktorLibrary { get; set; }
        public ObservableCollection<ISong> ImportedTrackCollection { get; set; }
        public ObservableCollection<ISong> FilteredTrackCollection { get; set; }
        public ObservableCollection<ISong> PreparationCollection { get; set; }
        public int ProgressBarMax { get; set; }
        public int ProgressBarValue { get; set; }
        public bool IsProgressBarIndeterminate { get; set; }
        public string ProgressBarMessage { get; set; }
        public int WindowHeight { get; set; }
        public int WindowWidth { get; set; }
        public int TrackCollectionListViewHeight { get; set; }
        public int PreparationListViewHeight { get; set; }
        public int ListViewWidth { get; set; }
        public int ProgressBarWidth { get; set; }
        public bool IsLoadButtonEnabled { get; set; }
        public ISong SelectedTrackCollectionItem { get; set; }
        public bool IsDeleteButtonEnabled { get; set; }
        public ISong SelectedPreparationItem { get; set; }
        public int SelectedTabControlIndex { get; set; }
        public bool IsAddNextButtonEnabled { get; set; }
        public int TempoSliderValue { get; set; }
        public string TempoSliderValueText
        {
            get
            {
                return string.Concat(TempoSliderValue.ToString(), " BPM");
            }
            set
            {
                TempoSliderValueText = value;
            }
        }
        public bool IsMixableRangeCheckboxChecked { get; set; }
        public bool IsClearButtonEnabled { get; set; }
        public ObservableCollection<IHarmonicKey> HarmonicKeyComboBoxCollection { get; set; }
        public IHarmonicKey SelectedHarmonicKeyComboBoxItem { get; set; }

        public SongListModel(ITraktorLibrary traktorLibrary, IXmlWrapper xmlWrapper)
        {
            TraktorLibrary = traktorLibrary;
            _xmlWrapper = xmlWrapper;
            ImportedTrackCollection = new ObservableCollection<ISong>();
            FilteredTrackCollection = new ObservableCollection<ISong>();
            PreparationCollection = new ObservableCollection<ISong>();
            WindowHeight = 412;
            WindowWidth = 1316;
            TrackCollectionListViewHeight = 250;
            PreparationListViewHeight = 278;
            ListViewWidth = 1292;
            ProgressBarWidth = 1294;
            IsLoadButtonEnabled = false;
            IsDeleteButtonEnabled = false;
            SelectedTabControlIndex = 0;
            IsAddNextButtonEnabled = false;
            IsMixableRangeCheckboxChecked = false;
            IsClearButtonEnabled = false;
            HarmonicKeyComboBoxCollection = GetHarmonicKeyComboBoxCollection();
        }

        // To be merged into GetFilteredTrackCollection()
        //public ObservableCollection<ISong> GetAddNextTrackCollection()
        //{
        //    return new ObservableCollection<ISong>(ImportedTrackCollection.Where(t =>
        //        (!PreparationCollection.Contains(t)
        //        && ((t.LeadingTempo <= SelectedPreparationItem.MixableRange.FastestTempo
        //        && t.LeadingTempo >= SelectedPreparationItem.MixableRange.SlowestTempo)
        //        || (t.LeadingTempo <= SelectedPreparationItem.MixableRange.FastestHalfTempo
        //        && t.LeadingTempo >= SelectedPreparationItem.MixableRange.SlowestHalfTempo)
        //        || (t.LeadingTempo <= SelectedPreparationItem.MixableRange.FastestDoubleTempo
        //        && t.LeadingTempo >= SelectedPreparationItem.MixableRange.SlowestDoubleTempo))
        //        && (t.LeadingHarmonicKey == SelectedPreparationItem.MixableRange.InnerCircleHarmonicKey
        //        || t.LeadingHarmonicKey == SelectedPreparationItem.MixableRange.OuterCircleHarmonicKey
        //        || t.LeadingHarmonicKey == SelectedPreparationItem.MixableRange.PlusOneHarmonicKey
        //        || t.LeadingHarmonicKey == SelectedPreparationItem.MixableRange.MinusOneHarmonicKey))));
        //}

        public int GetRandomRowIndex()
        {
            var randomRowIndex = 0;

            if (FilteredTrackCollection.Count > 1)
            {
                var random = new Random();
                randomRowIndex = random.Next(FilteredTrackCollection.Count);
            }

            return randomRowIndex;
        }

        public ObservableCollection<ISong> GetFilteredTrackCollection()
        {
            // merge in mixableRange tempo so we get the track tempo as a double rather than the tempo slider value as a int, for when getting next 
            var slowestTempoSliderValue = Math.Round(Convert.ToDouble(TempoSliderValue), 3);
            var fastestTempoSliderValue = Math.Round(Convert.ToDouble(TempoSliderValue + 1), 3);
            var slowestTempoSliderRangeValue = Math.Round((slowestTempoSliderValue - 3.0), 3); // replace 3 with prop for increasing tempo range from 3 to 6 etc
            var fastestTempoSliderRangeValue = Math.Round((fastestTempoSliderValue + 3.0), 3); // replace 3 with prop for increasing tempo range from 3 to 6 etc

            return new ObservableCollection<ISong>(ImportedTrackCollection.Where(t => 
                                                                                    // not in preparation list
                                                                                    (!PreparationCollection.Contains(t)
                                                                                    // and (cleared filter
                                                                                    && ((TempoSliderValue == 0 
                                                                                    && string.IsNullOrEmpty(SelectedHarmonicKeyComboBoxItem.Name)) 
                                                                                    // or exact filter match
                                                                                    || (!IsMixableRangeCheckboxChecked
                                                                                    && (TempoSliderValue == 0 
                                                                                    || (t.LeadingTempo >= slowestTempoSliderValue
                                                                                    && t.LeadingTempo <= fastestTempoSliderValue))
                                                                                    && (string.IsNullOrEmpty(SelectedHarmonicKeyComboBoxItem.Name) 
                                                                                    || t.LeadingHarmonicKey == SelectedHarmonicKeyComboBoxItem.Name))
                                                                                    // or mixable range filter match)
                                                                                    || (IsMixableRangeCheckboxChecked
                                                                                    && ((TempoSliderValue == 0 
                                                                                    || (t.LeadingTempo >= slowestTempoSliderRangeValue
                                                                                    && t.LeadingTempo <= fastestTempoSliderRangeValue)) 
                                                                                    && ((string.IsNullOrEmpty(SelectedHarmonicKeyComboBoxItem.Name) 
                                                                                    || (t.LeadingHarmonicKey == SelectedHarmonicKeyComboBoxItem.HarmonicKeyRange.InnerCircleHarmonicKey
                                                                                    || t.LeadingHarmonicKey == SelectedHarmonicKeyComboBoxItem.HarmonicKeyRange.OuterCircleHarmonicKey
                                                                                    || t.LeadingHarmonicKey == SelectedHarmonicKeyComboBoxItem.HarmonicKeyRange.PlusOneHarmonicKey
                                                                                    || t.LeadingHarmonicKey == SelectedHarmonicKeyComboBoxItem.HarmonicKeyRange.MinusOneHarmonicKey)))))))));
        }

        internal ObservableCollection<IHarmonicKey> GetHarmonicKeyComboBoxCollection()
        {
            var harmonicKeyComboBoxCollection = new ObservableCollection<IHarmonicKey>();

            IHarmonicKeyRange harmonicKeyRange = new HarmonicKeyRange();
            IHarmonicKey harmonicKey = new HarmonicKey(harmonicKeyRange) { Id = 0, Name = "" };
            harmonicKey.HarmonicKeyRange.Load(harmonicKey.Name);
            harmonicKeyComboBoxCollection.Add(harmonicKey);

            harmonicKeyRange = new HarmonicKeyRange();
            harmonicKey = new HarmonicKey(harmonicKeyRange) { Id = 1, Name = "1A" };
            harmonicKey.HarmonicKeyRange.Load(harmonicKey.Name);
            harmonicKeyComboBoxCollection.Add(harmonicKey);

            harmonicKeyRange = new HarmonicKeyRange();
            harmonicKey = new HarmonicKey(harmonicKeyRange) { Id = 2, Name = "1B" };
            harmonicKey.HarmonicKeyRange.Load(harmonicKey.Name);
            harmonicKeyComboBoxCollection.Add(harmonicKey);

            harmonicKeyRange = new HarmonicKeyRange();
            harmonicKey = new HarmonicKey(harmonicKeyRange) { Id = 3, Name = "2A" };
            harmonicKey.HarmonicKeyRange.Load(harmonicKey.Name);
            harmonicKeyComboBoxCollection.Add(harmonicKey);

            harmonicKeyRange = new HarmonicKeyRange();
            harmonicKey = new HarmonicKey(harmonicKeyRange) { Id = 4, Name = "2B" };
            harmonicKey.HarmonicKeyRange.Load(harmonicKey.Name);
            harmonicKeyComboBoxCollection.Add(harmonicKey);

            harmonicKeyRange = new HarmonicKeyRange();
            harmonicKey = new HarmonicKey(harmonicKeyRange) { Id = 5, Name = "3A" };
            harmonicKey.HarmonicKeyRange.Load(harmonicKey.Name);
            harmonicKeyComboBoxCollection.Add(harmonicKey);

            harmonicKeyRange = new HarmonicKeyRange();
            harmonicKey = new HarmonicKey(harmonicKeyRange) { Id = 6, Name = "3B" };
            harmonicKey.HarmonicKeyRange.Load(harmonicKey.Name);
            harmonicKeyComboBoxCollection.Add(harmonicKey);

            harmonicKeyRange = new HarmonicKeyRange();
            harmonicKey = new HarmonicKey(harmonicKeyRange) { Id = 7, Name = "4A" };
            harmonicKey.HarmonicKeyRange.Load(harmonicKey.Name);
            harmonicKeyComboBoxCollection.Add(harmonicKey);

            harmonicKeyRange = new HarmonicKeyRange();
            harmonicKey = new HarmonicKey(harmonicKeyRange) { Id = 8, Name = "4B" };
            harmonicKey.HarmonicKeyRange.Load(harmonicKey.Name);
            harmonicKeyComboBoxCollection.Add(harmonicKey);

            harmonicKeyRange = new HarmonicKeyRange();
            harmonicKey = new HarmonicKey(harmonicKeyRange) { Id = 9, Name = "5A" };
            harmonicKey.HarmonicKeyRange.Load(harmonicKey.Name);
            harmonicKeyComboBoxCollection.Add(harmonicKey);

            harmonicKeyRange = new HarmonicKeyRange();
            harmonicKey = new HarmonicKey(harmonicKeyRange) { Id = 10, Name = "5B" };
            harmonicKey.HarmonicKeyRange.Load(harmonicKey.Name);
            harmonicKeyComboBoxCollection.Add(harmonicKey);

            harmonicKeyRange = new HarmonicKeyRange();
            harmonicKey = new HarmonicKey(harmonicKeyRange) { Id = 11, Name = "6A" };
            harmonicKey.HarmonicKeyRange.Load(harmonicKey.Name);
            harmonicKeyComboBoxCollection.Add(harmonicKey);

            harmonicKeyRange = new HarmonicKeyRange();
            harmonicKey = new HarmonicKey(harmonicKeyRange) { Id = 12, Name = "6B" };
            harmonicKey.HarmonicKeyRange.Load(harmonicKey.Name);
            harmonicKeyComboBoxCollection.Add(harmonicKey);

            harmonicKeyRange = new HarmonicKeyRange();
            harmonicKey = new HarmonicKey(harmonicKeyRange) { Id = 13, Name = "7A" };
            harmonicKey.HarmonicKeyRange.Load(harmonicKey.Name);
            harmonicKeyComboBoxCollection.Add(harmonicKey);

            harmonicKeyRange = new HarmonicKeyRange();
            harmonicKey = new HarmonicKey(harmonicKeyRange) { Id = 14, Name = "7B" };
            harmonicKey.HarmonicKeyRange.Load(harmonicKey.Name);
            harmonicKeyComboBoxCollection.Add(harmonicKey);

            harmonicKeyRange = new HarmonicKeyRange();
            harmonicKey = new HarmonicKey(harmonicKeyRange) { Id = 15, Name = "8A" };
            harmonicKey.HarmonicKeyRange.Load(harmonicKey.Name);
            harmonicKeyComboBoxCollection.Add(harmonicKey);

            harmonicKeyRange = new HarmonicKeyRange();
            harmonicKey = new HarmonicKey(harmonicKeyRange) { Id = 16, Name = "8B" };
            harmonicKey.HarmonicKeyRange.Load(harmonicKey.Name);
            harmonicKeyComboBoxCollection.Add(harmonicKey);

            harmonicKeyRange = new HarmonicKeyRange();
            harmonicKey = new HarmonicKey(harmonicKeyRange) { Id = 17, Name = "9A" };
            harmonicKey.HarmonicKeyRange.Load(harmonicKey.Name);
            harmonicKeyComboBoxCollection.Add(harmonicKey);

            harmonicKeyRange = new HarmonicKeyRange();
            harmonicKey = new HarmonicKey(harmonicKeyRange) { Id = 18, Name = "9B" };
            harmonicKey.HarmonicKeyRange.Load(harmonicKey.Name);
            harmonicKeyComboBoxCollection.Add(harmonicKey);

            harmonicKeyRange = new HarmonicKeyRange();
            harmonicKey = new HarmonicKey(harmonicKeyRange) { Id = 19, Name = "10A" };
            harmonicKey.HarmonicKeyRange.Load(harmonicKey.Name);
            harmonicKeyComboBoxCollection.Add(harmonicKey);

            harmonicKeyRange = new HarmonicKeyRange();
            harmonicKey = new HarmonicKey(harmonicKeyRange) { Id = 20, Name = "10B" };
            harmonicKey.HarmonicKeyRange.Load(harmonicKey.Name);
            harmonicKeyComboBoxCollection.Add(harmonicKey);

            harmonicKeyRange = new HarmonicKeyRange();
            harmonicKey = new HarmonicKey(harmonicKeyRange) { Id = 21, Name = "11A" };
            harmonicKey.HarmonicKeyRange.Load(harmonicKey.Name);
            harmonicKeyComboBoxCollection.Add(harmonicKey);

            harmonicKeyRange = new HarmonicKeyRange();
            harmonicKey = new HarmonicKey(harmonicKeyRange) { Id = 22, Name = "11B" };
            harmonicKey.HarmonicKeyRange.Load(harmonicKey.Name);
            harmonicKeyComboBoxCollection.Add(harmonicKey);

            harmonicKeyRange = new HarmonicKeyRange();
            harmonicKey = new HarmonicKey(harmonicKeyRange) { Id = 23, Name = "12A" };
            harmonicKey.HarmonicKeyRange.Load(harmonicKey.Name);
            harmonicKeyComboBoxCollection.Add(harmonicKey);

            harmonicKeyRange = new HarmonicKeyRange();
            harmonicKey = new HarmonicKey(harmonicKeyRange) { Id = 24, Name = "12B" };
            harmonicKey.HarmonicKeyRange.Load(harmonicKey.Name);
            harmonicKeyComboBoxCollection.Add(harmonicKey);

            return harmonicKeyComboBoxCollection;
        }
    }
}