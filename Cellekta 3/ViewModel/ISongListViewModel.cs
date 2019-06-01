using Cellekta_3.Model;
using SongInterface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cellekta_3.ViewModel
{
    public interface ISongListViewModel
    {
        int ProgressBarMax { get; set; }
        int ProgressBarValue { get; set; }
        bool ProgressBarIsIndeterminate { get; set; }
        string ProgressMessage { get; set; }
    }
}
