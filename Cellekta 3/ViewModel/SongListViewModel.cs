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

        public ObservableCollection<ISong> SongCollection
        {
            get { return _songListModel.SongCollection; }
            set
            {
                _songListModel.SongCollection = value;
                NotifyPropertyChanged("SongCollection");
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

        public bool ProgressBarIsIndeterminate
        {
            get { return _songListModel.ProgressBarIsIndeterminate; }
            set
            {
                if (_songListModel.ProgressBarIsIndeterminate != value)
                {
                    _songListModel.ProgressBarIsIndeterminate = value;
                    NotifyPropertyChanged("ProgressBarIsIndeterminate");
                }
            }
        }

        public string ProgressMessage
        {
            get { return _songListModel.ProgressMessage; }
            set
            {
                _songListModel.ProgressMessage = value;
                NotifyPropertyChanged("ProgressMessage");
            }
        }

        public SongListViewModel(ISongListModel songListModel, IXmlWrapper xmlWrapper)
        {
            _songListModel = songListModel;
            _xmlWrapper = xmlWrapper;
            NewMenuCommand = new RelayCommand(OnNewMenuCommand);
            ImportMenuCommand = new RelayCommand(OnImportMenuCommand);
            ExitMenuCommand = new RelayCommand(OnExitMenuCommand);
            ResetProgressBar();
            ProgressMessage = "Ready to import";
        }

        internal void OnNewMenuCommand(object param)
        {
            _songListModel.SongCollection.Clear();
        }

        internal async void OnImportMenuCommand(object param)
        {
            if (_songListModel.TraktorLibrary.IsCollectionFound())
            {
                _songListModel.ProgressBarIsIndeterminate = true;
                await Task.Run(() => _songListModel.TraktorLibrary.DeleteWorkingCollection());
                await Task.Run(() => _songListModel.TraktorLibrary.CreateWorkingCollection());
                await Task.Run(() => _songListModel.TraktorLibrary.LoadWorkingCollection());
                ProgressBarMax = await Task.Run(() => _songListModel.TraktorLibrary.GetSongCount());
                ProgressBarIsIndeterminate = false;

                if (!string.IsNullOrEmpty(_songListModel.TraktorLibrary.WorkingCollection))
                {
                    _xmlWrapper.XmlPath = _songListModel.TraktorLibrary.WorkingCollectionPath;
                    _xmlWrapper.Load();

                    foreach (XmlNode collectionNode in _xmlWrapper.XmlDocument.DocumentElement.SelectNodes("/NML/COLLECTION"))
                    {
                        foreach (XmlNode entryNode in collectionNode.SelectNodes("ENTRY"))
                        {
                            ISong song = await Task.Run(() => GetSong(entryNode));
                            SongCollection.Add(song);
                            ProgressBarValue++;
                        }
                    }
                }

                ProgressMessage = "Song collection imported";
            }
            else
            {
                ProgressMessage = "No Traktor library found";
            }

            MessageBox.Show("Song collection imported.");
            ResetProgressBar();
        }

        internal void OnExitMenuCommand(object param)
        {
            Application.Current.Shutdown();
        }

        internal void ResetProgressBar()
        {
            ProgressBarValue = InitialProgressBarValue;
            ProgressBarMax = InitialProgressBarMax;
            ProgressMessage = "";
        }

        internal ISong GetSong(XmlNode entryNode)
        {
            ISong song = _songListModel.TraktorLibrary.GetSong(entryNode);
            ProgressMessage = song.Path;

            return song;
        }
    }
}
