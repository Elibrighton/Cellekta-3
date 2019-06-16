using MixDiscInterface;
using SongInterface;
using System.Collections.ObjectModel;
using TraktorLibraryInterface;

namespace Cellekta_3.Model
{
    public interface ISongListModel
    {
        ITraktorLibrary TraktorLibrary { get; set; }
        ObservableCollection<ISong> ImportedTrackCollection { get; set; }
        ObservableCollection<ISong> FilteredTrackCollection { get; set; }
        ObservableCollection<ISong> PreparationCollection { get; set; }
        ObservableCollection<ISong> MixDiscCollection { get; set; }
        int ProgressBarMax { get; set; }
        int ProgressBarValue { get; set; }
        bool IsProgressBarIndeterminate { get; set; }
        string ProgressBarMessage { get; set; }
        int WindowHeight { get; set; }
        int WindowWidth { get; set; }
        int TrackCollectionListViewHeight { get; set; }
        int PreparationListViewHeight { get; set; }
        int MixDiscListViewHeight { get; set; }
        int ListViewWidth { get; set; }
        int ProgressBarWidth { get; set; }
        bool IsLoadButtonEnabled { get; set; }
        ISong SelectedTrackCollectionItem { get; set; }
        bool IsDeleteButtonEnabled { get; set; }
        ISong SelectedPreparationItem { get; set; }
        int SelectedTabControlIndex { get; set; }
        bool IsAddNextButtonEnabled { get; set; }
        int TempoSliderValue { get; set; }
        string TempoSliderValueText { get; set; }
        bool IsMixableRangeCheckboxChecked { get; set; }
        bool IsClearButtonEnabled { get; set; }
        ObservableCollection<string> HarmonicKeyComboBoxCollection { get; set; }
        string SelectedHarmonicKeyComboBoxItem { get; set; }
        bool IsRangeOfThreeMenuChecked { get; set; }
        bool IsRangeOfSixMenuChecked { get; set; }
        bool IsRangeOfTwelveMenuChecked { get; set; }
        bool IsRangeOfThreeMenuEnabled { get; set; }
        bool IsRangeOfSixMenuEnabled { get; set; }
        bool IsRangeOfTwelveMenuEnabled { get; set; }
        ObservableCollection<string> PlaylistComboBoxCollection { get; set; }
        string SelectedPlaylistComboBoxItem { get; set; }
        string SearchTextBoxText { get; set; }
        ObservableCollection<string> MixDiscPlaylistComboBoxCollection { get; set; }
        bool IsMixDiscClearButtonEnabled { get; set; }
        string SelectedMixDiscPlaylistComboBoxItem { get; set; }
        bool IsMixButtonEnabled { get; set; }
        IMixDisc MixDisc { get; set; }
        string PlaytimeTextBoxText { get; set; }
        ObservableCollection<string> IntensityComboBoxCollection { get; set; }
        string SelectedIntensityComboBoxItem { get; set; }

        ObservableCollection<ISong> GetFilteredTrackCollection();
        int GetRandomRowIndex();
    }
}
