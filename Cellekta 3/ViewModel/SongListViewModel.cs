using Cellekta_3.Base;
using Cellekta_3.Model;
using SongInterface;
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
                    NotifyPropertyChanged("TrackCollectionListViewHeight");
                    NotifyPropertyChanged("PreparationListViewHeight");
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
            ResetProgressBar();
            ProgressBarMessage = "Ready to import";
            SelectedHarmonicKeyComboBoxItem = HarmonicKeyComboBoxCollection[0];
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
                            ProgressBarValue++;
                        }
                    }
                }

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
            ClearFilter();
            EnableControls();
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

            EnableControls();
        }

        internal void ClearFilter()
        {
            TempoSliderValue = 0;
            IsMixableRangeCheckboxChecked = false;
            SelectedHarmonicKeyComboBoxItem = HarmonicKeyComboBoxCollection[0];
            Filter();
            ProgressBarMessage = "Filters cleared";
        }

        internal void EnableControls()
        {
            IsLoadButtonEnabled = FilteredTrackCollection.Count > 0;
            IsAddNextButtonEnabled = FilteredTrackCollection.Count > 0;
            IsDeleteButtonEnabled = PreparationCollection.Count > 0;
            IsAddNextButtonEnabled = PreparationCollection.Count > 0;
            IsClearButtonEnabled = TempoSliderValue != 0 || IsMixableRangeCheckboxChecked;
        }

        internal void SelectRandomTrackCollectionItem()
        {
            var filteredTrackCollectionCount = FilteredTrackCollection.Count();

            if (filteredTrackCollectionCount > 0)
            {
                var randomRowIndex = _songListModel.GetRandomRowIndex();
                SelectedTrackCollectionItem = FilteredTrackCollection[randomRowIndex];

                if (SelectedPreparationItem != null)
                {
                    ProgressBarMessage = string.Concat("Find mixable track for ", SelectedPreparationItem.FullNameText, " from ", filteredTrackCollectionCount.ToString(), " tracks");
                }
            }
            else
            {
                MessageBox.Show("No tracks are mixable with the loaded track.");
                ProgressBarMessage = "No tracks are mixable with the loaded track";
            }
        }
    }
}
