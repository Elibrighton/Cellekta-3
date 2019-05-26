using TraktorLibraryInterface;

namespace Cellekta_3.Model
{
    public interface ISongListModel
    {
        string StatusMessage { get; set; }
        ITraktorLibrary TraktorLibrary { get; set; }
    }
}
