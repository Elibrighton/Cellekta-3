using Cellekta_3.Base;
using Cellekta_3.Model;
using SongInterface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using XmlWrapperInterface;

namespace Cellekta_3.ViewModel
{
    public class SongListViewModel : ObservableObject, ISongListViewModel
    {
        private const int InitialProgressBarMax = 1;
        private const int InitialProgressBarValue = 0;
        private const int TrackCollectionTabControlIndex = 0;
        private const int PreparationTabControlIndex = 1;

        private ISongListModel _songListModel;
        private IXmlWrapper _xmlWrapper;
        private List<List<ISong>> _mixDiscTracks;

        public ICommand ClearMenuCommand { get; set; }
        public ICommand ImportMenuCommand { get; set; }
        public ICommand ExitMenuCommand { get; set; }
        public ICommand LoadButtonCommand { get; set; }
        public ICommand DeleteButtonCommand { get; set; }
        public ICommand AddNextButtonCommand { get; set; }
        public ICommand TempoSliderValueCommand { get; set; }
        public ICommand MixableRangeCheckboxCheckedCommand { get; set; }
        public ICommand ClearButtonCommand { get; set; }
        public ICommand HarmonicKeyComboBoxSelectionChangedCommand { get; set; }
        public ICommand RangeOfThreeMenuCommand { get; set; }
        public ICommand RangeOfSixMenuCommand { get; set; }
        public ICommand RangeOfTwelveMenuCommand { get; set; }
        public ICommand PlaylistComboBoxSelectionChangedCommand { get; set; }
        public ICommand MixDiscClearButtonCommand { get; set; }
        public ICommand MixDiscPlaylistComboBoxSelectionChangedCommand { get; set; }
        public ICommand MixButtonCommand { get; set; }
        public ICommand IntensityComboBoxSelectionChangedCommand { get; set; }

        public ObservableCollection<ISong> ImportedTrackCollection
        {
            get { return _songListModel.ImportedTrackCollection; }
            set
            {
                _songListModel.ImportedTrackCollection = value;
                NotifyPropertyChanged("ImportedTrackCollection");
            }
        }

        public ObservableCollection<ISong> FilteredTrackCollection
        {
            get { return _songListModel.FilteredTrackCollection; }
            set
            {
                _songListModel.FilteredTrackCollection = value;
                NotifyPropertyChanged("FilteredTrackCollection");
            }
        }

        public ObservableCollection<ISong> PreparationCollection
        {
            get { return _songListModel.PreparationCollection; }
            set
            {
                _songListModel.PreparationCollection = value;
                NotifyPropertyChanged("PreparationCollection");
            }
        }

        public ObservableCollection<ISong> MixDiscCollection
        {
            get { return _songListModel.MixDiscCollection; }
            set
            {
                _songListModel.MixDiscCollection = value;
                NotifyPropertyChanged("MixDiscCollection");
            }
        }

        public int ProgressBarMax
        {
            get { return _songListModel.ProgressBarMax; }
            set
            {
                if (_songListModel.ProgressBarMax != value)
                {
                    _songListModel.ProgressBarMax = value;
                    NotifyPropertyChanged("ProgressBarMax");
                }
            }
        }

        public int ProgressBarValue
        {
            get { return _songListModel.ProgressBarValue; }
            set
            {
                if (_songListModel.ProgressBarValue != value)
                {
                    _songListModel.ProgressBarValue = value;
                    NotifyPropertyChanged("ProgressBarValue");
                }
            }
        }

        public bool IsProgressBarIndeterminate
        {
            get { return _songListModel.IsProgressBarIndeterminate; }
            set
            {
                if (_songListModel.IsProgressBarIndeterminate != value)
                {
                    _songListModel.IsProgressBarIndeterminate = value;
                    NotifyPropertyChanged("IsProgressBarIndeterminate");
                }
            }
        }

        public string ProgressBarMessage
        {
            get { return _songListModel.ProgressBarMessage; }
            set
            {
                _songListModel.ProgressBarMessage = value;
                NotifyPropertyChanged("ProgressBarMessage");
            }
        }

        public int WindowHeight
        {
            get { return _songListModel.WindowHeight; }
            set
            {
                if (_songListModel.WindowHeight != value)
                {
                    _songListModel.WindowHeight = value;
                    NotifyPropertyChanged("WindowHeight");

                    TrackCollectionListViewHeight = (value - 162);
                    PreparationListViewHeight = (value - 134);
                    MixDiscListViewHeight = (value - 162);
                    NotifyPropertyChanged("TrackCollectionListViewHeight");
                    NotifyPropertyChanged("PreparationListViewHeight");
                    NotifyPropertyChanged("MixDiscListViewHeight");
                }
            }
        }

        public int WindowWidth
        {
            get { return _songListModel.WindowWidth; }
            set
            {
                if (_songListModel.WindowWidth != value)
                {
                    _songListModel.WindowWidth = value;
                    NotifyPropertyChanged("WindowWidth");

                    ListViewWidth = (value - 24);
                    NotifyPropertyChanged("ListViewWidth");

                    ProgressBarWidth = (value - 22);
                    NotifyPropertyChanged("ProgressBarWidth");
                }
            }
        }

        public int TrackCollectionListViewHeight
        {
            get { return _songListModel.TrackCollectionListViewHeight; }
            set
            {
                if (_songListModel.TrackCollectionListViewHeight != value)
                {
                    _songListModel.TrackCollectionListViewHeight = value;
                    NotifyPropertyChanged("TrackCollectionListViewHeight");
                }
            }
        }

        public int PreparationListViewHeight
        {
            get { return _songListModel.PreparationListViewHeight; }
            set
            {
                if (_songListModel.PreparationListViewHeight != value)
                {
                    _songListModel.PreparationListViewHeight = value;
                    NotifyPropertyChanged("PreparationListViewHeight");
                }
            }
        }

        public int MixDiscListViewHeight
        {
            get { return _songListModel.MixDiscListViewHeight; }
            set
            {
                if (_songListModel.MixDiscListViewHeight != value)
                {
                    _songListModel.MixDiscListViewHeight = value;
                    NotifyPropertyChanged("MixDiscListViewHeight");
                }
            }
        }

        public int ListViewWidth
        {
            get { return _songListModel.ListViewWidth; }
            set
            {
                if (_songListModel.ListViewWidth != value)
                {
                    _songListModel.ListViewWidth = value;
                    NotifyPropertyChanged("ListViewWidth");
                }
            }
        }

        public int ProgressBarWidth
        {
            get { return _songListModel.ProgressBarWidth; }
            set
            {
                if (_songListModel.ProgressBarWidth != value)
                {
                    _songListModel.ProgressBarWidth = value;
                    NotifyPropertyChanged("ProgressBarWidth");
                }
            }
        }

        public bool IsLoadButtonEnabled
        {
            get { return _songListModel.IsLoadButtonEnabled; }
            set
            {
                if (_songListModel.IsLoadButtonEnabled != value)
                {
                    _songListModel.IsLoadButtonEnabled = value;
                    NotifyPropertyChanged("IsLoadButtonEnabled");
                }
            }
        }

        public ISong SelectedTrackCollectionItem
        {
            get { return _songListModel.SelectedTrackCollectionItem; }
            set
            {
                if (_songListModel.SelectedTrackCollectionItem != value)
                {
                    _songListModel.SelectedTrackCollectionItem = value;
                    NotifyPropertyChanged("SelectedTrackCollectionItem");
                }
            }
        }

        public bool IsDeleteButtonEnabled
        {
            get { return _songListModel.IsDeleteButtonEnabled; }
            set
            {
                if (_songListModel.IsDeleteButtonEnabled != value)
                {
                    _songListModel.IsDeleteButtonEnabled = value;
                    NotifyPropertyChanged("IsDeleteButtonEnabled");
                }
            }
        }

        public ISong SelectedPreparationItem
        {
            get { return _songListModel.SelectedPreparationItem; }
            set
            {
                if (_songListModel.SelectedPreparationItem != value)
                {
                    _songListModel.SelectedPreparationItem = value;
                    NotifyPropertyChanged("SelectedPreparationItem");
                }
            }
        }

        public int SelectedTabControlIndex
        {
            get { return _songListModel.SelectedTabControlIndex; }
            set
            {
                if (_songListModel.SelectedTabControlIndex != value)
                {
                    _songListModel.SelectedTabControlIndex = value;
                    NotifyPropertyChanged("SelectedTabControlIndex");
                }
            }
        }

        public bool IsAddNextButtonEnabled
        {
            get { return _songListModel.IsAddNextButtonEnabled; }
            set
            {
                if (_songListModel.IsAddNextButtonEnabled != value)
                {
                    _songListModel.IsAddNextButtonEnabled = value;
                    NotifyPropertyChanged("IsAddNextButtonEnabled");
                }
            }
        }

        public int TempoSliderValue
        {
            get { return _songListModel.TempoSliderValue; }
            set
            {
                if (_songListModel.TempoSliderValue != value)
                {
                    _songListModel.TempoSliderValue = value;
                    NotifyPropertyChanged("TempoSliderValue");
                    NotifyPropertyChanged("TempoSliderValueText");
                }
            }
        }

        public string TempoSliderValueText
        {
            get { return _songListModel.TempoSliderValueText; }
            set
            {
                if (_songListModel.TempoSliderValueText != value)
                {
                    _songListModel.TempoSliderValueText = value;
                    NotifyPropertyChanged("TempoSliderValueText");
                }
            }
        }

        public bool IsMixableRangeCheckboxChecked
        {
            get { return _songListModel.IsMixableRangeCheckboxChecked; }
            set
            {
                if (_songListModel.IsMixableRangeCheckboxChecked != value)
                {
                    _songListModel.IsMixableRangeCheckboxChecked = value;
                    NotifyPropertyChanged("IsMixableRangeCheckboxChecked");
                }
            }
        }

        public bool IsClearButtonEnabled
        {
            get { return _songListModel.IsClearButtonEnabled; }
            set
            {
                if (_songListModel.IsClearButtonEnabled != value)
                {
                    _songListModel.IsClearButtonEnabled = value;
                    NotifyPropertyChanged("IsClearButtonEnabled");
                }
            }
        }

        public ObservableCollection<string> HarmonicKeyComboBoxCollection
        {
            get { return _songListModel.HarmonicKeyComboBoxCollection; }
            set
            {
                _songListModel.HarmonicKeyComboBoxCollection = value;
                NotifyPropertyChanged("HarmonicKeyComboBoxCollection");
            }
        }

        public string SelectedHarmonicKeyComboBoxItem
        {
            get { return _songListModel.SelectedHarmonicKeyComboBoxItem; }
            set
            {
                if (_songListModel.SelectedHarmonicKeyComboBoxItem != value)
                {
                    _songListModel.SelectedHarmonicKeyComboBoxItem = value;
                    NotifyPropertyChanged("SelectedHarmonicKeyComboBoxItem");
                }
            }
        }

        public bool IsRangeOfThreeMenuChecked
        {
            get { return _songListModel.IsRangeOfThreeMenuChecked; }
            set
            {
                if (_songListModel.IsRangeOfThreeMenuChecked != value)
                {
                    _songListModel.IsRangeOfThreeMenuChecked = value;
                    NotifyPropertyChanged("IsRangeOfThreeMenuChecked");
                }
            }
        }

        public bool IsRangeOfSixMenuChecked
        {
            get { return _songListModel.IsRangeOfSixMenuChecked; }
            set
            {
                if (_songListModel.IsRangeOfSixMenuChecked != value)
                {
                    _songListModel.IsRangeOfSixMenuChecked = value;
                    NotifyPropertyChanged("IsRangeOfSixMenuChecked");
                }
            }
        }

        public bool IsRangeOfTwelveMenuChecked
        {
            get { return _songListModel.IsRangeOfTwelveMenuChecked; }
            set
            {
                if (_songListModel.IsRangeOfTwelveMenuChecked != value)
                {
                    _songListModel.IsRangeOfTwelveMenuChecked = value;
                    NotifyPropertyChanged("IsRangeOfTwelveMenuChecked");
                }
            }
        }

        public bool IsRangeOfThreeMenuEnabled
        {
            get { return _songListModel.IsRangeOfThreeMenuEnabled; }
            set
            {
                if (_songListModel.IsRangeOfThreeMenuEnabled != value)
                {
                    _songListModel.IsRangeOfThreeMenuEnabled = value;
                    NotifyPropertyChanged("IsRangeOfThreeMenuEnabled");
                }
            }
        }

        public bool IsRangeOfSixMenuEnabled
        {
            get { return _songListModel.IsRangeOfSixMenuEnabled; }
            set
            {
                if (_songListModel.IsRangeOfSixMenuEnabled != value)
                {
                    _songListModel.IsRangeOfSixMenuEnabled = value;
                    NotifyPropertyChanged("IsRangeOfSixMenuEnabled");
                }
            }
        }

        public bool IsRangeOfTwelveMenuEnabled
        {
            get { return _songListModel.IsRangeOfTwelveMenuEnabled; }
            set
            {
                if (_songListModel.IsRangeOfTwelveMenuEnabled != value)
                {
                    _songListModel.IsRangeOfTwelveMenuEnabled = value;
                    NotifyPropertyChanged("IsRangeOfTwelveMenuEnabled");
                }
            }
        }

        public ObservableCollection<string> PlaylistComboBoxCollection
        {
            get { return _songListModel.PlaylistComboBoxCollection; }
            set
            {
                _songListModel.PlaylistComboBoxCollection = value;
                NotifyPropertyChanged("PlaylistComboBoxCollection");
            }
        }

        public string SelectedPlaylistComboBoxItem
        {
            get { return _songListModel.SelectedPlaylistComboBoxItem; }
            set
            {
                if (_songListModel.SelectedPlaylistComboBoxItem != value)
                {
                    _songListModel.SelectedPlaylistComboBoxItem = value;
                    NotifyPropertyChanged("SelectedPlaylistComboBoxItem");
                }
            }
        }

        public string SearchTextBoxText
        {
            get { return _songListModel.SearchTextBoxText; }
            set
            {
                if (_songListModel.SearchTextBoxText != value)
                {
                    _songListModel.SearchTextBoxText = value;
                    NotifyPropertyChanged("SearchTextBoxText");
                    Filter();
                }
            }
        }

        public ObservableCollection<string> MixDiscPlaylistComboBoxCollection
        {
            get { return _songListModel.MixDiscPlaylistComboBoxCollection; }
            set
            {
                _songListModel.MixDiscPlaylistComboBoxCollection = value;
                NotifyPropertyChanged("MixDiscPlaylistComboBoxCollection");
            }
        }

        public bool IsMixDiscClearButtonEnabled
        {
            get { return _songListModel.IsMixDiscClearButtonEnabled; }
            set
            {
                if (_songListModel.IsMixDiscClearButtonEnabled != value)
                {
                    _songListModel.IsMixDiscClearButtonEnabled = value;
                    NotifyPropertyChanged("IsMixDiscClearButtonEnabled");
                }
            }
        }

        public string SelectedMixDiscPlaylistComboBoxItem
        {
            get { return _songListModel.SelectedMixDiscPlaylistComboBoxItem; }
            set
            {
                if (_songListModel.SelectedMixDiscPlaylistComboBoxItem != value)
                {
                    _songListModel.SelectedMixDiscPlaylistComboBoxItem = value;
                    NotifyPropertyChanged("SelectedMixDiscPlaylistComboBoxItem");
                }
            }
        }

        public bool IsMixButtonEnabled
        {
            get { return _songListModel.IsMixButtonEnabled; }
            set
            {
                if (_songListModel.IsMixButtonEnabled != value)
                {
                    _songListModel.IsMixButtonEnabled = value;
                    NotifyPropertyChanged("IsMixButtonEnabled");
                }
            }
        }

        public string PlaytimeTextBoxText
        {
            get { return _songListModel.PlaytimeTextBoxText; }
            set
            {
                if (_songListModel.PlaytimeTextBoxText != value)
                {
                    _songListModel.PlaytimeTextBoxText = value;
                    NotifyPropertyChanged("PlaytimeTextBoxText");
                    EnableMixDiscControls();
                }
            }
        }

        public ObservableCollection<string> IntensityComboBoxCollection
        {
            get { return _songListModel.IntensityComboBoxCollection; }
            set
            {
                _songListModel.IntensityComboBoxCollection = value;
                NotifyPropertyChanged("IntensityComboBoxCollection");
            }
        }

        public string SelectedIntensityComboBoxItem
        {
            get { return _songListModel.SelectedIntensityComboBoxItem; }
            set
            {
                if (_songListModel.SelectedIntensityComboBoxItem != value)
                {
                    _songListModel.SelectedIntensityComboBoxItem = value;
                    NotifyPropertyChanged("SelectedIntensityComboBoxItem");
                }
            }
        }

        public string MixLengthTextBoxText
        {
            get { return _songListModel.MixLengthTextBoxText; }
            set
            {
                if (_songListModel.MixLengthTextBoxText != value)
                {
                    _songListModel.MixLengthTextBoxText = value;
                    NotifyPropertyChanged("MixLengthTextBoxText");
                    EnableMixDiscControls();
                }
            }
        }

        public SongListViewModel(ISongListModel songListModel, IXmlWrapper xmlWrapper)
        {
            _songListModel = songListModel;
            _xmlWrapper = xmlWrapper;
            ClearMenuCommand = new RelayCommand(OnClearMenuCommand);
            ImportMenuCommand = new RelayCommand(OnImportMenuCommand);
            ExitMenuCommand = new RelayCommand(OnExitMenuCommand);
            LoadButtonCommand = new RelayCommand(OnLoadButtonCommand);
            DeleteButtonCommand = new RelayCommand(OnDeleteButtonCommand);
            AddNextButtonCommand = new RelayCommand(OnAddNextButtonCommand);
            TempoSliderValueCommand = new RelayCommand(OnTempoSliderValueCommand);
            MixableRangeCheckboxCheckedCommand = new RelayCommand(OnMixableRangeCheckboxCheckedCommand);
            ClearButtonCommand = new RelayCommand(OnClearButtonCommand);
            HarmonicKeyComboBoxSelectionChangedCommand = new RelayCommand(OnHarmonicKeyComboBoxSelectionChangedCommand);
            RangeOfThreeMenuCommand = new RelayCommand(OnRangeOfThreeMenuCommand);
            RangeOfSixMenuCommand = new RelayCommand(OnRangeOfSixMenuCommand);
            RangeOfTwelveMenuCommand = new RelayCommand(OnRangeOfTwelveMenuCommand);
            PlaylistComboBoxSelectionChangedCommand = new RelayCommand(OnPlaylistComboBoxSelectionChangedCommand);
            MixDiscClearButtonCommand = new RelayCommand(OnMixDiscClearButtonCommand);
            MixDiscPlaylistComboBoxSelectionChangedCommand = new RelayCommand(OnMixDiscPlaylistComboBoxSelectionChangedCommand);
            MixButtonCommand = new RelayCommand(OnMixButtonCommand);
            IntensityComboBoxSelectionChangedCommand = new RelayCommand(OnIntensityComboBoxSelectionChangedCommand);
            ResetProgressBar();
            ProgressBarMessage = "Ready to import";
            SelectedHarmonicKeyComboBoxItem = HarmonicKeyComboBoxCollection[0];
            _mixDiscTracks = new List<List<ISong>>();
        }

        internal void OnClearMenuCommand(object param)
        {
            if (ImportedTrackCollection.Count() > 0)
            {
                if (MessageBox.Show("Are you sure you want to clear the playlists?", "Clear playlists", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    ClearPlaylists();
                }
            }
        }

        internal async void OnImportMenuCommand(object param)
        {
            ClearPlaylists();
            _songListModel.TraktorLibrary.SetCollectionPath();

            if (_songListModel.TraktorLibrary.IsCollectionFound())
            {
                _songListModel.IsProgressBarIndeterminate = true;
                await Task.Run(() => _songListModel.TraktorLibrary.DeleteWorkingCollection());
                await Task.Run(() => _songListModel.TraktorLibrary.CreateWorkingCollection());
                await Task.Run(() => _songListModel.TraktorLibrary.LoadWorkingCollection());
                ProgressBarMax = await Task.Run(() => _songListModel.TraktorLibrary.GetSongCount());
                IsProgressBarIndeterminate = false;

                if (!string.IsNullOrEmpty(_songListModel.TraktorLibrary.WorkingCollection))
                {
                    _xmlWrapper.XmlPath = _songListModel.TraktorLibrary.WorkingCollectionPath;
                    _xmlWrapper.Load();

                    foreach (XmlNode collectionNode in _xmlWrapper.XmlDocument.DocumentElement.SelectNodes("/NML/COLLECTION"))
                    {
                        foreach (XmlNode entryNode in collectionNode.SelectNodes("ENTRY"))
                        {
                            ISong song = await Task.Run(() => GetSong(entryNode));
                            ImportedTrackCollection.Add(song);

                            if (!PlaylistComboBoxCollection.Contains(song.Playlist))
                            {
                                PlaylistComboBoxCollection.Add(song.Playlist);
                            }

                            ProgressBarValue++;
                        }
                    }
                }

                PlaylistComboBoxCollection = new ObservableCollection<string>(PlaylistComboBoxCollection.OrderBy(p => p));
                MixDiscPlaylistComboBoxCollection = new ObservableCollection<string>(PlaylistComboBoxCollection);
                Filter();

                if (FilteredTrackCollection.Count > 0)
                {
                    SelectedTrackCollectionItem = FilteredTrackCollection[0];
                }

                EnableControls();
                var statusMessage = string.Concat(ImportedTrackCollection.Count.ToString(), " tracks imported from Traktor collection");
                ProgressBarMessage = statusMessage;
                MessageBox.Show(string.Concat(statusMessage, "."));
            }
            else
            {
                ProgressBarMessage = "No Traktor collection found";
                MessageBox.Show("No Traktor collection found.");
            }

            SelectedTabControlIndex = TrackCollectionTabControlIndex;
            ResetProgressBar(false);
        }

        internal void OnExitMenuCommand(object param)
        {
            Application.Current.Shutdown();
        }

        internal void OnLoadButtonCommand(object param)
        {
            if (SelectedTrackCollectionItem != null)
            {
                if (SelectedPreparationItem != null)
                {
                    var index = PreparationCollection.IndexOf(SelectedPreparationItem);
                    PreparationCollection.Insert(index + 1, SelectedTrackCollectionItem);
                }
                else
                {
                    PreparationCollection.Add(SelectedTrackCollectionItem);
                }

                EnableControls();
                ProgressBarMessage = string.Concat("Loaded ", SelectedTrackCollectionItem.FullNameText);
                SelectedPreparationItem = SelectedTrackCollectionItem;
                SelectedTabControlIndex = PreparationTabControlIndex;
            }
            else
            {
                MessageBox.Show("No track selected to load.");
            }
        }

        internal void OnDeleteButtonCommand(object param)
        {
            if (SelectedPreparationItem != null)
            {
                var fullNameText = SelectedPreparationItem.FullNameText;
                PreparationCollection.Remove(SelectedPreparationItem);
                var preparationCollectionCount = PreparationCollection.Count();

                if (preparationCollectionCount > 0)
                {
                    SelectedPreparationItem = PreparationCollection[preparationCollectionCount - 1];
                }
                EnableControls();
                ProgressBarMessage = string.Concat("Deleted ", fullNameText);
            }
            else
            {
                MessageBox.Show("No track selected to delete.");
            }
        }

        internal void OnAddNextButtonCommand(object param)
        {
            if (SelectedPreparationItem != null)
            {
                if (!IsMixableRangeCheckboxChecked)
                {
                    IsMixableRangeCheckboxChecked = true;
                }

                TempoSliderValue = SelectedPreparationItem.RoundedTrailingTempo;
                SelectedHarmonicKeyComboBoxItem = HarmonicKeyComboBoxCollection[(HarmonicKeyComboBoxCollection.IndexOf(SelectedPreparationItem.TrailingHarmonicKey))];
                Filter();
                SelectRandomTrackCollectionItem();

                if (FilteredTrackCollection.Count() > 0)
                {
                    SelectedTabControlIndex = TrackCollectionTabControlIndex;
                }
            }
            else
            {
                MessageBox.Show("No track selected to find mixable tracks for.");
            }
        }

        internal void OnTempoSliderValueCommand(object param)
        {
            Filter();
        }

        internal void OnMixableRangeCheckboxCheckedCommand(object param)
        {
            Filter();
        }

        internal void OnClearButtonCommand(object param)
        {
            ClearFilter();
        }

        internal void OnHarmonicKeyComboBoxSelectionChangedCommand(object param)
        {
            Filter();
        }

        internal void OnPlaylistComboBoxSelectionChangedCommand(object param)
        {
            Filter();
        }

        internal void OnRangeOfThreeMenuCommand(object param)
        {
            if (IsRangeOfSixMenuChecked)
            {
                IsRangeOfSixMenuChecked = false;
                IsRangeOfSixMenuEnabled = true;
            }
            else if (IsRangeOfTwelveMenuChecked)
            {
                IsRangeOfTwelveMenuChecked = false;
                IsRangeOfTwelveMenuEnabled = true;
            }

            if (IsRangeOfThreeMenuChecked)
            {
                IsRangeOfThreeMenuEnabled = false;
                Filter();
                SelectRandomTrackCollectionItem();
            }
        }

        internal void OnRangeOfSixMenuCommand(object param)
        {
            if (IsRangeOfThreeMenuChecked)
            {
                IsRangeOfThreeMenuChecked = false;
                IsRangeOfThreeMenuEnabled = true;
            }
            else if (IsRangeOfTwelveMenuChecked)
            {
                IsRangeOfTwelveMenuChecked = false;
                IsRangeOfTwelveMenuEnabled = true;
            }

            if (IsRangeOfSixMenuChecked)
            {
                IsRangeOfSixMenuEnabled = false;
                Filter();
                SelectRandomTrackCollectionItem();
            }
        }

        internal void OnRangeOfTwelveMenuCommand(object param)
        {
            if (IsRangeOfThreeMenuChecked)
            {
                IsRangeOfThreeMenuChecked = false;
                IsRangeOfThreeMenuEnabled = true;
            }
            else if (IsRangeOfSixMenuChecked)
            {
                IsRangeOfSixMenuChecked = false;
                IsRangeOfSixMenuEnabled = true;
            }

            if (IsRangeOfTwelveMenuChecked)
            {
                IsRangeOfTwelveMenuEnabled = false;
                Filter();
                SelectRandomTrackCollectionItem();
            }
        }

        internal void OnMixDiscClearButtonCommand(object param)
        {
            ClearMixDiscFilter();
        }

        internal void OnMixDiscPlaylistComboBoxSelectionChangedCommand(object param)
        {
            EnableMixDiscControls();
        }

        internal async void OnMixButtonCommand(object param)
        {
            if (IsMixDiscFilterValid())
            {
                var playlistTracks = new ObservableCollection<ISong>(ImportedTrackCollection.Where(t => t.Playlist == SelectedMixDiscPlaylistComboBoxItem)).ToList();
                var playlistTrackCount = playlistTracks.Count();
                var tasks = new List<Task>(playlistTrackCount);

                ResetProgressBar();
                ProgressBarMax = playlistTrackCount;
                var minPlaytime = (Convert.ToInt32(PlaytimeTextBoxText) * 60);
                var mixLength = Convert.ToInt32(MixLengthTextBoxText);

                foreach (var track in playlistTracks)
                {
                    var task = FindMixDiscTracksAsync(track, playlistTracks, SelectedIntensityComboBoxItem, minPlaytime, mixLength);
                    tasks.Add(task);
                }

                await Task.WhenAll(tasks);
            }
        }

        internal void OnIntensityComboBoxSelectionChangedCommand(object param)
        {
            EnableMixDiscControls();
        }

        internal void ResetProgressBar(bool isClearingProgressMessage = true)
        {
            ProgressBarValue = InitialProgressBarValue;
            ProgressBarMax = InitialProgressBarMax;

            if (isClearingProgressMessage)
            {
                ProgressBarMessage = "";
            }
        }

        internal ISong GetSong(XmlNode entryNode)
        {
            ISong song = _songListModel.TraktorLibrary.GetSong(entryNode);
            ProgressBarMessage = song.Path;

            return song;
        }

        internal void ClearPlaylists()
        {
            FilteredTrackCollection.Clear();
            ImportedTrackCollection.Clear();
            PreparationCollection.Clear();
            MixDiscCollection.Clear();
            ClearFilter();
            ClearMixDiscFilter();
            _mixDiscTracks = new List<List<ISong>>();
            EnableControls();
            EnableMixDiscControls();
            ProgressBarMessage = "Ready to import";
        }

        internal void Filter()
        {
            FilteredTrackCollection = _songListModel.GetFilteredTrackCollection();
            var filteredTrackCollectionCount = FilteredTrackCollection.Count();

            if (filteredTrackCollectionCount > 0)
            {
                SelectedTrackCollectionItem = FilteredTrackCollection[0];
            }

            UpdateStatusMessage();
            EnableControls();
        }

        internal void ClearFilter()
        {
            TempoSliderValue = 0;
            IsMixableRangeCheckboxChecked = false;

            if (HarmonicKeyComboBoxCollection.Count > 0)
            {
                SelectedHarmonicKeyComboBoxItem = HarmonicKeyComboBoxCollection[0];
            }

            if (PlaylistComboBoxCollection.Count > 0)
            {
                SelectedPlaylistComboBoxItem = PlaylistComboBoxCollection[0];
            }

            SearchTextBoxText = "";
            Filter();
            ProgressBarMessage = "Filters cleared";
        }

        internal void EnableControls()
        {
            IsLoadButtonEnabled = FilteredTrackCollection.Count > 0;
            IsAddNextButtonEnabled = FilteredTrackCollection.Count > 0;
            IsDeleteButtonEnabled = PreparationCollection.Count > 0;
            IsAddNextButtonEnabled = PreparationCollection.Count > 0;
            IsClearButtonEnabled = (TempoSliderValue != 0 || IsMixableRangeCheckboxChecked || !string.IsNullOrEmpty(SelectedHarmonicKeyComboBoxItem) || !string.IsNullOrEmpty(SelectedPlaylistComboBoxItem) || !string.IsNullOrEmpty(SearchTextBoxText));
        }

        internal void SelectRandomTrackCollectionItem()
        {
            if (FilteredTrackCollection.Count() > 0)
            {
                var randomRowIndex = _songListModel.GetRandomRowIndex();
                SelectedTrackCollectionItem = FilteredTrackCollection[randomRowIndex];
                UpdateStatusMessage();
            }
            else if (SelectedPreparationItem != null)
            {
                var statusMessage = string.Concat("No tracks are mixable with ", SelectedPreparationItem.FullNameText);
                ProgressBarMessage = statusMessage;
                MessageBox.Show(string.Concat(statusMessage, "."));
            }
        }

        internal void UpdateStatusMessage()
        {
            if (SelectedPreparationItem != null)
            {
                ProgressBarMessage = string.Concat("Find mixable track for ", SelectedPreparationItem.FullNameText, " from ", FilteredTrackCollection.Count.ToString(), " tracks");
            }
        }

        internal void ClearMixDiscFilter()
        {
            if (MixDiscPlaylistComboBoxCollection.Count > 0)
            {
                SelectedMixDiscPlaylistComboBoxItem = MixDiscPlaylistComboBoxCollection[0];
            }

            if (IntensityComboBoxCollection.Count > 0)
            {
                SelectedIntensityComboBoxItem = IntensityComboBoxCollection[0];
            }

            PlaytimeTextBoxText = "";
            MixLengthTextBoxText = "";
            ResetProgressBar();
            ProgressBarMessage = "Mix disc filters cleared";
        }

        internal void EnableMixDiscControls()
        {
            var isEnabled = (!string.IsNullOrEmpty(SelectedMixDiscPlaylistComboBoxItem) || !string.IsNullOrEmpty(PlaytimeTextBoxText) || !string.IsNullOrEmpty(SelectedIntensityComboBoxItem) || !string.IsNullOrEmpty(MixLengthTextBoxText));
            IsMixDiscClearButtonEnabled = isEnabled;
            IsMixButtonEnabled = isEnabled;
        }

        internal bool IsMixDiscFilterValid()
        {
            var isMixDiscFilterValid = true;
            var validationMessage = string.Empty;

            if (string.IsNullOrEmpty(SelectedMixDiscPlaylistComboBoxItem))
            {
                validationMessage = "You must select a playlist.";
            }
            else if (string.IsNullOrEmpty(PlaytimeTextBoxText))
            {
                validationMessage = "You must enter a playtime.";
            }
            else if (string.IsNullOrEmpty(MixLengthTextBoxText))
            {
                validationMessage = "You must enter a mix length.";
            }

            if (!string.IsNullOrEmpty(validationMessage))
            {
                MessageBox.Show(validationMessage);
                isMixDiscFilterValid = false;
            }

            return isMixDiscFilterValid;
        }

        internal async Task FindMixDiscTracksAsync(ISong track, List<ISong> playlistTracks, string intensityStyle, int minPlaytime, int mixLength)
        {
            var mixDiscTracks = await Task.Run(() => GetMixDiscTracks(track, playlistTracks, intensityStyle, minPlaytime, mixLength));

            ProgressBarValue++;
            ProgressBarMessage = string.Concat("Finding Mix disc for track ", ProgressBarValue, " of ", ProgressBarMax);

            if (mixDiscTracks.Count > 0)
            {
                _mixDiscTracks.Add(mixDiscTracks);
                DisplayBestMixDiscTracks();
            }
        }

        internal List<ISong> GetMixDiscTracks(ISong track, List<ISong> playlistTracks, string intensityStyle, int minPlaytime, int mixLength)
        {
            return _songListModel.GetMixDiscTracks(track, playlistTracks, intensityStyle, minPlaytime, mixLength);
        }

        internal void DisplayBestMixDiscTracks()
        {
            MixDiscCollection.Clear();
            var bestMixDisc = _songListModel.GetBestMixDiscTracks(_mixDiscTracks, SelectedIntensityComboBoxItem);

            foreach (var track in bestMixDisc)
            {
                MixDiscCollection.Add(track);
            }

            var statusMessage = string.Concat("Mix disc match is found");
            ProgressBarMessage = statusMessage;
            MessageBox.Show(string.Concat(statusMessage, "."));
        }
    }
}
