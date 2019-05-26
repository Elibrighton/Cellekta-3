using Cellekta_3.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cellekta_3.Model
{
    public class SongListModel : ISongListModel
    {
        public ObservableCollection<ISong> Songs { get; set; }

        public SongListModel()
        {
            Songs = new ObservableCollection<ISong>();
        }
    }
}
