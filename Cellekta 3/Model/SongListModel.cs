using SongInterface;
using System.Collections.ObjectModel;
using TraktorLibraryInterface;
using XmlWrapperInterface;

namespace Cellekta_3.Model
{
    public class SongListModel : ISongListModel
    {
        private IXmlWrapper _xmlWrapper;

        public ITraktorLibrary TraktorLibrary { get; set; }
        public ObservableCollection<ISong> TrackCollection { get; set; }
        public ObservableCollection<ISong> Preparation { get; set; }
        public int ProgressBarMax { get; set; }
        public int ProgressBarValue { get; set; }
        public bool IsProgressBarIndeterminate { get; set; }
        public string ProgressBarMessage { get; set; }
        public int WindowHeight { get; set; }
        public int WindowWidth { get; set; }
        public int ListViewHeight { get; set; }
        public int ListViewWidth { get; set; }
        public int ProgressBarWidth { get; set; }
        public bool IsLoadButtonEnabled { get; set; }
        public ISong SelectedTrackCollectionItem { get; set; }
        public bool IsDeleteButtonEnabled { get; set; }
        public ISong SelectedPreparationItem { get; set; }

        public SongListModel(ITraktorLibrary traktorLibrary, IXmlWrapper xmlWrapper)
        {
            TraktorLibrary = traktorLibrary;
            _xmlWrapper = xmlWrapper;
            TrackCollection = new ObservableCollection<ISong>();
            Preparation = new ObservableCollection<ISong>();
            WindowHeight = 412;
            WindowWidth = 1316;
            ListViewHeight = 281; 
            ListViewWidth = 1292;
            ProgressBarWidth = 1294;
            IsLoadButtonEnabled = false;
            IsDeleteButtonEnabled = false;
        }
    }
}
