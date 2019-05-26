using Cellekta_3.Base;
using Cellekta_3.Model;
using System.Windows;
using System.Windows.Input;

namespace Cellekta_3.ViewModel
{
    public class SongListViewModel : ObservableObject, ISongListViewModel
    {
        private ISongListModel _songListModel;

        public ICommand NewMenuCommand { get; set; }
        public ICommand OpenMenuCommand { get; set; }
        public ICommand ExitMenuCommand { get; set; }

        public SongListViewModel(ISongListModel songListModel)
        {
            _songListModel = songListModel;
            NewMenuCommand = new RelayCommand(OnNewMenuCommand);
            OpenMenuCommand = new RelayCommand(OnOpenMenuCommand);
            ExitMenuCommand = new RelayCommand(OnExitMenuCommand);
        }

        public string StatusMessage
        {
            get { return _songListModel.StatusMessage; }
            set
            {
                _songListModel.StatusMessage = value;
                NotifyPropertyChanged("StatusMessage");
            }
        }

        internal void OnNewMenuCommand(object param)
        {
            _songListModel.TraktorLibrary.Songs.Clear();
        }

        internal void OnOpenMenuCommand(object param)
        {
            if (_songListModel.TraktorLibrary.IsCollectionFound())
            {
                _songListModel.TraktorLibrary.ImportCollection();
            }
            else
            {
                StatusMessage = "No Traktor library found. Locate the collection.nml file.";
            }
        }

        internal void OnExitMenuCommand(object param)
        {
            Application.Current.Shutdown();
        }
    }
}
