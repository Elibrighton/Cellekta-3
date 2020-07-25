using PlaylistInterface;
using SongInterface;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Cellekta_3.ViewModel
{
    public interface ISongListViewModel
    {
        ICommand ClearMenuCommand { get; set; }
        ICommand ImportMenuCommand { get; set; }
        ICommand ExitMenuCommand { get; set; }
        ICommand LoadButtonCommand { get; set; }
        ICommand DeleteButtonCommand { get; set; }
        ICommand AddNextButtonCommand { get; set; }
        ICommand TempoSliderValueCommand { get; set; }
        ICommand MixableRangeCheckboxCheckedCommand { get; set; }
        ICommand ClearButtonCommand { get; set; }
        ICommand HarmonicKeyComboBoxSelectionChangedCommand { get; set; }
        ICommand RangeOfThreeMenuCommand { get; set; }
        ICommand RangeOfSixMenuCommand { get; set; }
        ICommand RangeOfTwelveMenuCommand { get; set; }
        ICommand PlaylistComboBoxSelectionChangedCommand { get; set; }
        ICommand MixDiscClearButtonCommand { get; set; }
        ICommand MixDiscPlaylistComboBoxSelectionChangedCommand { get; set; }
        ICommand MixButtonCommand { get; set; }
        ICommand IntensityComboBoxSelectionChangedCommand { get; set; }
        ObservableCollection<ISong> ImportedTrackCollection { get; set; }
        ObservableCollection<ISong> FilteredTrackCollection { get; set; }
        ObservableCollection<ISong> PreparationCollection { get; set; }
        ObservableCollection<ISong> MixDiscCollection { get; set; }
        ObservableCollection<IPlaylist> PlaylistCollection { get; set; }
        int ProgressBarMax { get; set; }
        int ProgressBarValue { get; set; }
        bool IsProgressBarIndeterminate { get; set; }
        string ProgressBarMessage { get; set; }
        int WindowHeight { get; set; }
        int WindowWidth { get; set; }
        int TrackCollectionListViewHeight { get; set; }
        int PreparationListViewHeight { get; set; }
        int MixDiscListViewHeight { get; set; }
        int PlaylistListViewHeight { get; set; }
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
        string PlaytimeTextBoxText { get; set; }
        ObservableCollection<string> IntensityComboBoxCollection { get; set; }
        string SelectedIntensityComboBoxItem { get; set; }
        string MixLengthTextBoxText { get; set; }
    }
}
