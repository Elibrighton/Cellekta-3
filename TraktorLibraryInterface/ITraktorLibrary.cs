using SongInterface;
using System.Collections.ObjectModel;
using System.Xml;

namespace TraktorLibraryInterface
{
    public interface ITraktorLibrary
    {
        string CollectionPath { get; set; }
        string WorkingCollectionPath { get; set; }
        string WorkingCollection { get; set; }

        bool IsCollectionFound();
        void DeleteWorkingCollection();
        void CreateWorkingCollection();
        void LoadWorkingCollection();
        int GetSongCount();
        ISong GetSong(XmlNode entryNode);
        void SetCollectionPath();
    }
}
