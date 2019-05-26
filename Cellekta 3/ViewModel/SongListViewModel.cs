using Cellekta_3.Base;
using Cellekta_3.Model;
using Cellekta_3.Services;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
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

        public ObservableCollection<ISong> Songs
        {
            get { return _songListModel.Songs; }
            set
            {
                _songListModel.Songs = value;
                NotifyPropertyChanged("Songs");
            }
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
            Songs.Clear();
        }

        internal void OnOpenMenuCommand(object param)
        {
            _songListModel.SetTraktorLibraryPath();

            if (_songListModel.IsTraktorLibraryFound())
            {

            }
            else
            {
                StatusMessage = "No Traktor library found.";
            }
        }

        internal void OnExitMenuCommand(object param)
        {
            Application.Current.Shutdown();
        }
    }
}
