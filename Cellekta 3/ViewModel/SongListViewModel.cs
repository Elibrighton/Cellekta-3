using Cellekta_3.Base;
using Cellekta_3.Model;
using Cellekta_3.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cellekta_3.ViewModel
{
    public class SongListViewModel : ObservableObject, ISongListViewModel 
    {
        private ISongListModel _songListModel;

        public SongListViewModel(ISongListModel songListModel)
        {
            _songListModel = songListModel;
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
    }
}
