using SongInterface;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Cellekta_3.ViewModel
{
    public interface ISongListViewModel
    {
        ICommand NewMenuCommand { get; set; }
        ICommand ImportMenuCommand { get; set; }
        ICommand ExitMenuCommand { get; set; }
        ObservableCollection<ISong> SongCollection { get; set; }
        int ProgressBarMax { get; set; }
        int ProgressBarValue { get; set; }
        bool ProgressBarIsIndeterminate { get; set; }
        string ProgressMessage { get; set; }
    }
}
