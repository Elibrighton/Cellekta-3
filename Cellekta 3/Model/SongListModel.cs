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
        public int ListViewHeight { get; set; }
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

        public SongListModel(ITraktorLibrary traktorLibrary, IXmlWrapper xmlWrapper)
        {
            TraktorLibrary = traktorLibrary;
            _xmlWrapper = xmlWrapper;
            ImportedTrackCollection = new ObservableCollection<ISong>();
            FilteredTrackCollection = new ObservableCollection<ISong>();
            PreparationCollection = new ObservableCollection<ISong>();
            WindowHeight = 412;
            WindowWidth = 1316;
            ListViewHeight = 250;
            ListViewWidth = 1292;
            ProgressBarWidth = 1294;
            IsLoadButtonEnabled = false;
            IsDeleteButtonEnabled = false;
            SelectedTabControlIndex = 0;
            IsAddNextButtonEnabled = false;
            IsMixableRangeCheckboxChecked = false;
        }

        public ObservableCollection<ISong> GetAddNextTrackCollection()
        {
            return new ObservableCollection<ISong>(ImportedTrackCollection.Where(t =>
                (!PreparationCollection.Contains(t)
                && ((t.LeadingTempo <= SelectedPreparationItem.MixableRange.FastestTempo
                && t.LeadingTempo >= SelectedPreparationItem.MixableRange.SlowestTempo)
                || (t.LeadingTempo <= SelectedPreparationItem.MixableRange.FastestHalfTempo
                && t.LeadingTempo >= SelectedPreparationItem.MixableRange.SlowestHalfTempo)
                || (t.LeadingTempo <= SelectedPreparationItem.MixableRange.FastestDoubleTempo
                && t.LeadingTempo >= SelectedPreparationItem.MixableRange.SlowestDoubleTempo))
                && (t.LeadingHarmonicKey == SelectedPreparationItem.MixableRange.InnerCircleHarmonicKey
                || t.LeadingHarmonicKey == SelectedPreparationItem.MixableRange.OuterCircleHarmonicKey
                || t.LeadingHarmonicKey == SelectedPreparationItem.MixableRange.PlusOneHarmonicKey
                || t.LeadingHarmonicKey == SelectedPreparationItem.MixableRange.MinusOneHarmonicKey))));
        }

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
            var slowestTempoSliderValue = Math.Round(Convert.ToDouble(TempoSliderValue), 3);
            var fastestTempoSliderValue = Math.Round(Convert.ToDouble(TempoSliderValue + 1), 3);
            var slowestTempoSliderRangeValue = Math.Round((slowestTempoSliderValue - 3.0), 3); // replace 3 with prop
            var fastestTempoSliderRangeValue = Math.Round((fastestTempoSliderValue + 3.0), 3); // replace 3 with prop

            return new ObservableCollection<ISong>(ImportedTrackCollection.Where(t => ((TempoSliderValue == 0) 
                                                                                || (!IsMixableRangeCheckboxChecked
                                                                                && t.LeadingTempo >= slowestTempoSliderValue
                                                                                && t.LeadingTempo <= fastestTempoSliderValue)
                                                                                || (IsMixableRangeCheckboxChecked
                                                                                && ((t.LeadingTempo >= slowestTempoSliderRangeValue
                                                                                && t.LeadingTempo <= fastestTempoSliderRangeValue))))));
        }
    }
}
