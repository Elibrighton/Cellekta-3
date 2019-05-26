using SongInterface;
using System.Collections.ObjectModel;

namespace TraktorLibraryInterface
{
    public interface ITraktorLibrary
    {
        string CollectionPath { get; set; }
        string WorkingCollectionPath { get; set; }
        ObservableCollection<ISong> Songs { get; set; }

        bool IsCollectionFound();
        void ImportCollection();
    }
}
