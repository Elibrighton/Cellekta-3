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
        public ObservableCollection<ISong> SongCollection { get; set; }
        public int ProgressBarMax { get; set; }
        public int ProgressBarValue { get; set; }
        public bool ProgressBarIsIndeterminate { get; set; }
        public string ProgressMessage { get; set; }

        public SongListModel(ITraktorLibrary traktorLibrary, IXmlWrapper xmlWrapper)
        {
            TraktorLibrary = traktorLibrary;
            _xmlWrapper = xmlWrapper;
            SongCollection = new ObservableCollection<ISong>();
        }
    }
}
