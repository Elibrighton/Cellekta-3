using Cellekta_3.Base;
using Cellekta_3.Model;
using SongInterface;
using System.Collections.ObjectModel;
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

        private ISongListModel _songListModel;
        private IXmlWrapper _xmlWrapper;

        public ICommand NewMenuCommand { get; set; }
        public ICommand ImportMenuCommand { get; set; }
        public ICommand ExitMenuCommand { get; set; }
        public ICommand LoadButtonCommand { get; set; }

        public ObservableCollection<ISong> TrackCollection
        {
            get { return _songListModel.TrackCollection; }
            set
            {
                _songListModel.TrackCollection = value;
                NotifyPropertyChanged("TrackCollection");
            }
        }

        public ObservableCollection<ISong> Preparation
        {
            get { return _songListModel.Preparation; }
            set
            {
                _songListModel.Preparation = value;
                NotifyPropertyChanged("Preparation");
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

                    ListViewHeight = (value - 111);
                    NotifyPropertyChanged("ListViewHeight");
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

        public int ListViewHeight
        {
            get { return _songListModel.ListViewHeight; }
            set
            {
                if (_songListModel.ListViewHeight != value)
                {
                    _songListModel.ListViewHeight = value;
                    NotifyPropertyChanged("ListViewHeight");
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
                    NotifyPropertyChanged("ListViewHeight");
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

        public SongListViewModel(ISongListModel songListModel, IXmlWrapper xmlWrapper)
        {
            _songListModel = songListModel;
            _xmlWrapper = xmlWrapper;
            NewMenuCommand = new RelayCommand(OnNewMenuCommand);
            ImportMenuCommand = new RelayCommand(OnImportMenuCommand);
            ExitMenuCommand = new RelayCommand(OnExitMenuCommand);
            LoadButtonCommand = new RelayCommand(OnLoadButtonCommand);
            ResetProgressBar();
            ProgressBarMessage = "Ready to import";
        }

        internal void OnNewMenuCommand(object param)
        {
            TrackCollection.Clear();
            IsLoadButtonEnabled = TrackCollection.Count > 0;
            ProgressBarMessage = "Ready to import";
        }

        internal async void OnImportMenuCommand(object param)
        {
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
                            TrackCollection.Add(song);
                            ProgressBarValue++;
                            IsLoadButtonEnabled = TrackCollection.Count > 0;
                        }
                    }
                }

                ProgressBarMessage = "Traktor collection imported";
            }
            else
            {
                ProgressBarMessage = "No Traktor collection found";
            }

            MessageBox.Show("Traktor collection imported.");
            ResetProgressBar();
        }

        internal void OnExitMenuCommand(object param)
        {
            Application.Current.Shutdown();
        }

        internal void OnLoadButtonCommand(object param)
        {
            if (SelectedTrackCollectionItem != null)
            {
                Preparation.Add(SelectedTrackCollectionItem);
                ProgressBarMessage = string.Concat("Loaded ", SelectedTrackCollectionItem.Artist, " ", SelectedTrackCollectionItem.Title);
            }
        }


        internal void ResetProgressBar()
        {
            ProgressBarValue = InitialProgressBarValue;
            ProgressBarMax = InitialProgressBarMax;
            ProgressBarMessage = "";
        }

        internal ISong GetSong(XmlNode entryNode)
        {
            ISong song = _songListModel.TraktorLibrary.GetSong(entryNode);
            ProgressBarMessage = song.Path;

            return song;
        }
    }
}
