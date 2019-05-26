using Cellekta_3.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cellekta_3.Model
{
    public interface ISongListModel
    {
        ObservableCollection<ISong> Songs { get; set; }
    }
}
