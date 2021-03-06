﻿using MixableRangeInterface;
using MixDiscImplementation;
using MixDiscInterface;
using PlaylistInterface;
using SongInterface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using TrackSearchInterface;
using TraktorLibraryInterface;
using XmlWrapperInterface;

namespace Cellekta_3.Model
{
    public class SongListModel : ISongListModel
    {
        private const double TempoRangeThree = 3.0;
        private const double TempoRangeSix = 6.0;
        private const double TempoRangeTwelve = 12.0;

        private IXmlWrapper _xmlWrapper;
        private IHarmonicKeyRange _harmonicKeyRange;
        private ITrackSearch _trackSearch;

        public ITraktorLibrary TraktorLibrary { get; set; }
        public ObservableCollection<ISong> ImportedTrackCollection { get; set; }
        public ObservableCollection<ISong> FilteredTrackCollection { get; set; }
        public ObservableCollection<ISong> PreparationCollection { get; set; }
        public ObservableCollection<ISong> MixDiscCollection { get; set; }
        public ObservableCollection<IPlaylist> PlaylistCollection { get; set; }
        public int ProgressBarMax { get; set; }
        public int ProgressBarValue { get; set; }
        public bool IsProgressBarIndeterminate { get; set; }
        public string ProgressBarMessage { get; set; }
        public int WindowHeight { get; set; }
        public int WindowWidth { get; set; }
        public int TrackCollectionListViewHeight { get; set; }
        public int PreparationListViewHeight { get; set; }
        public int MixDiscListViewHeight { get; set; }
        public int PlaylistListViewHeight { get; set; }
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
        public ObservableCollection<string> HarmonicKeyComboBoxCollection { get; set; }
        public string SelectedHarmonicKeyComboBoxItem { get; set; }
        public bool IsRangeOfThreeMenuChecked { get; set; }
        public bool IsRangeOfSixMenuChecked { get; set; }
        public bool IsRangeOfTwelveMenuChecked { get; set; }
        public bool IsRangeOfThreeMenuEnabled { get; set; }
        public bool IsRangeOfSixMenuEnabled { get; set; }
        public bool IsRangeOfTwelveMenuEnabled { get; set; }
        public ObservableCollection<string> PlaylistComboBoxCollection { get; set; }
        public string SelectedPlaylistComboBoxItem { get; set; }
        public string SearchTextBoxText { get; set; }
        public ObservableCollection<string> MixDiscPlaylistComboBoxCollection { get; set; }
        public bool IsMixDiscClearButtonEnabled { get; set; }
        public string SelectedMixDiscPlaylistComboBoxItem { get; set; }
        public bool IsMixButtonEnabled { get; set; }
        public string PlaytimeTextBoxText { get; set; }
        public ObservableCollection<string> IntensityComboBoxCollection { get; set; }
        public string SelectedIntensityComboBoxItem { get; set; }
        public string MixLengthTextBoxText { get; set; }
        public IMixDisc MixDisc { get; set; }

        public SongListModel(ITraktorLibrary traktorLibrary, IXmlWrapper xmlWrapper, IHarmonicKeyRange harmonicKeyRange, ITrackSearch trackSearch)
        {
            TraktorLibrary = traktorLibrary;
            _xmlWrapper = xmlWrapper;
            _harmonicKeyRange = harmonicKeyRange;
            _trackSearch = trackSearch;
            ImportedTrackCollection = new ObservableCollection<ISong>();
            FilteredTrackCollection = new ObservableCollection<ISong>();
            PreparationCollection = new ObservableCollection<ISong>();
            MixDiscCollection = new ObservableCollection<ISong>();
            PlaylistCollection = new ObservableCollection<IPlaylist>();
            WindowHeight = 412;
            WindowWidth = 1316;
            TrackCollectionListViewHeight = 250;
            PreparationListViewHeight = 278;
            MixDiscListViewHeight = 250;
            PlaylistListViewHeight = 278;
            ListViewWidth = 1292;
            ProgressBarWidth = 1294;
            IsLoadButtonEnabled = false;
            IsDeleteButtonEnabled = false;
            SelectedTabControlIndex = 0;
            IsAddNextButtonEnabled = false;
            IsMixableRangeCheckboxChecked = false;
            IsClearButtonEnabled = false;
            HarmonicKeyComboBoxCollection = GetHarmonicKeyComboBoxCollection();
            IsRangeOfThreeMenuChecked = true;
            IsRangeOfSixMenuChecked = false;
            IsRangeOfTwelveMenuChecked = false;
            IsRangeOfThreeMenuEnabled = false;
            IsRangeOfSixMenuEnabled = true;
            IsRangeOfTwelveMenuEnabled = true;
            PlaylistComboBoxCollection = new ObservableCollection<string>
            {
                "",
            };
            MixDiscPlaylistComboBoxCollection = new ObservableCollection<string>();
            IsMixDiscClearButtonEnabled = false;
            IsMixButtonEnabled = false;
            IntensityComboBoxCollection = GetIntensityComboBoxCollection();
            MixDisc = new MixDisc();
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

            // refactor ito Range project
            var tempoRangeValue = GetTempoRangeValue();

            var slowestTempoSliderRangeValue = Math.Round((slowestTempoSliderValue - tempoRangeValue), 3);
            var fastestTempoSliderRangeValue = Math.Round((fastestTempoSliderValue + tempoRangeValue), 3);

            _harmonicKeyRange.Load(SelectedHarmonicKeyComboBoxItem);

            _trackSearch.Reset();
            _trackSearch.Text = SearchTextBoxText;
            _trackSearch.Load();

            var selectedPlaylists = PlaylistCollection.Where(p => p.Selected);

            return new ObservableCollection<ISong>(ImportedTrackCollection.Where(t =>
                    // not in preparation list
                    (!PreparationCollection.Contains(t)
                    // and playlist is selected
                    && (selectedPlaylists.FirstOrDefault(p => p.Path == Path.GetDirectoryName(t.Path)) != null)
                    // and (exact filter match
                    && ((!IsMixableRangeCheckboxChecked 
                    && (TempoSliderValue == 0 
                    || (Math.Round(t.LeadingTempo, 3) >= slowestTempoSliderValue 
                    && Math.Round(t.LeadingTempo, 3) <= fastestTempoSliderValue)) 
                    && (string.IsNullOrEmpty(SelectedHarmonicKeyComboBoxItem)
                    || t.LeadingHarmonicKey == SelectedHarmonicKeyComboBoxItem))
                    // or mixable range filter match)
                    || (IsMixableRangeCheckboxChecked 
                    && ((TempoSliderValue == 0
                    || (Math.Round(t.LeadingTempo, 3) >= slowestTempoSliderRangeValue
                    && Math.Round(t.LeadingTempo, 3) <= fastestTempoSliderRangeValue))
                    && ((string.IsNullOrEmpty(SelectedHarmonicKeyComboBoxItem)
                    || (t.LeadingHarmonicKey == _harmonicKeyRange.InnerCircleHarmonicKey
                    || t.LeadingHarmonicKey == _harmonicKeyRange.OuterCircleHarmonicKey
                    || t.LeadingHarmonicKey == _harmonicKeyRange.PlusOneHarmonicKey
                    || t.LeadingHarmonicKey == _harmonicKeyRange.MinusOneHarmonicKey))))))
                    // and matches playlist
                    && (string.IsNullOrEmpty(SelectedPlaylistComboBoxItem)
                    || t.Playlist == SelectedPlaylistComboBoxItem)
                    // and matches search text
                    && (string.IsNullOrEmpty(_trackSearch.Text)
                    || t.Artist.ToLower().Contains(_trackSearch.Text.ToLower().Trim())
                    || t.Title.ToLower().Contains(_trackSearch.Text.ToLower().Trim()))
                    || (!string.IsNullOrEmpty(_trackSearch.Artist)
                    && !string.IsNullOrEmpty(_trackSearch.Title)
                    && ((t.Artist.ToLower().Contains(_trackSearch.Artist.ToLower())
                    && t.Title.ToLower().Contains(_trackSearch.Title.ToLower()))
                    || (t.Artist.ToLower().Contains(_trackSearch.Title.ToLower())
                    && t.Title.ToLower().Contains(_trackSearch.Artist.ToLower()))))
                )));
        }

        public List<ISong> GetMixDiscTracks(List<ISong> baseTrackList, List<ISong> playlistTracks, string intensityStyle, int minPlaytime, int mixLength, List<ISong> longestTrackCombinationList, List<List<ISong>> culledMatchingTrackCombinationList)
        {
            MixDisc = new MixDisc
            {
                BaseTrackList = baseTrackList,
                PlaylistTracks = playlistTracks,
                IntensityStyle = intensityStyle,
                MinPlaytime = minPlaytime,
                MixLength = mixLength
            };

            MixDisc.LongestTrackCombinationList = longestTrackCombinationList;
            MixDisc.CulledMatchingTrackCombinationList = culledMatchingTrackCombinationList;

            return MixDisc.GetBestMatch();
        }

        public List<ISong> GetBestMixDiscTracks(List<ISong> baseTrackList, List<List<ISong>> mixDiscTracksList, string intensityStyle, List<List<ISong>> culledMatchingTrackCombinationList)
        {

            IMixDisc mixDisc = new MixDisc
            {
                BaseTrackList = baseTrackList,
                IntensityStyle = intensityStyle,
                MatchingTrackCombinationList = mixDiscTracksList
            };

            return mixDisc.GetFinalBestMatch(culledMatchingTrackCombinationList);
        }

        internal ObservableCollection<string> GetHarmonicKeyComboBoxCollection()
        {
            return new ObservableCollection<string>()
            {
                "",
                "1A",
                "1B",
                "2A",
                "2B",
                "3A",
                "3B",
                "4A",
                "4B",
                "5A",
                "5B",
                "6A",
                "6B",
                "7A",
                "7B",
                "8A",
                "8B",
                "9A",
                "9B",
                "10A",
                "10B",
                "11A",
                "11B",
                "12A",
                "12B"
            };
        }

        internal double GetTempoRangeValue()
        {
            var tempoRange = TempoRangeThree;

            if (IsRangeOfSixMenuChecked)
            {
                tempoRange = TempoRangeSix;
            }
            else if (IsRangeOfTwelveMenuChecked)
            {
                tempoRange = TempoRangeTwelve;
            }

            return tempoRange;
        }

        internal ObservableCollection<string> GetIntensityComboBoxCollection()
        {
            return new ObservableCollection<string>()
            {
                "",
                "Highest",
                "Lowest",
                "Random"
            };
        }
    }
}