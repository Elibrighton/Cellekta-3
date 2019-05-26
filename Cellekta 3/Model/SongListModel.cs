using TraktorLibraryInterface;

namespace Cellekta_3.Model
{
    public class SongListModel : ISongListModel
    {
        public string StatusMessage { get; set; }
        public ITraktorLibrary TraktorLibrary { get; set; }

        public SongListModel(ITraktorLibrary traktorLibrary)
        {
            TraktorLibrary = traktorLibrary;
            StatusMessage = "Ready..";
        }
    }
}
