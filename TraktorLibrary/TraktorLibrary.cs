using Microsoft.Win32;
using MixableRangeImplementation;
using MixableRangeInterface;
using SongImplementation;
using SongInterface;
using System.Configuration;
using System.IO;
using System.Xml;
using TraktorLibraryInterface;
using XmlWrapperInterface;

namespace TraktorLibraryImplementation
{
    public class TraktorLibrary : ITraktorLibrary
    {
        const string CollectionFileName = "collection.nml";

        public string CollectionPath { get; set; }
        public string WorkingCollectionPath { get; set; }
        public string WorkingCollection { get; set; }

        private IXmlWrapper _xmlWrapper;

        public TraktorLibrary(IXmlWrapper xmlWrapper)
        {
            _xmlWrapper = xmlWrapper;

            CollectionPath = GetDefaultCollectionPath();
            WorkingCollectionPath = GetWorkingPath();
        }

        internal string GetWorkingPath()
        {
            return Path.Combine(Path.GetTempPath(), CollectionFileName);
        }

        public void SetCollectionPath()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = ConfigurationManager.AppSettings["LibraryPath"];

            if (openFileDialog.ShowDialog() == true)
            {
                CollectionPath = openFileDialog.FileName;
            }
        }

        public void DeleteWorkingCollection()
        {
            if (File.Exists(WorkingCollectionPath))
            {
                File.Delete(WorkingCollectionPath);
            }
        }

        public void CreateWorkingCollection()
        {
            var traktorLibraryPath = Path.Combine(ConfigurationManager.AppSettings["LibraryPath"], CollectionFileName);
            File.Copy(traktorLibraryPath, WorkingCollectionPath, true);
        }

        public void LoadWorkingCollection()
        {
            if (File.Exists(WorkingCollectionPath))
            {
                WorkingCollection = File.ReadAllText(WorkingCollectionPath);
            }
        }

        public bool IsCollectionFound()
        {
            return File.Exists(CollectionPath);
        }

        public int GetSongCount()
        {
            var songCount = 0;

            if (!string.IsNullOrEmpty(WorkingCollection))
            {
                _xmlWrapper.XmlPath = WorkingCollectionPath;
                _xmlWrapper.Load();

                foreach (XmlNode collectionNode in _xmlWrapper.XmlDocument.DocumentElement.SelectNodes("/NML/COLLECTION"))
                {
                    foreach (XmlNode entryNode in collectionNode.SelectNodes("ENTRY"))
                    {
                        songCount++;
                    }
                }
            }
            return songCount;
        }

        public ISong GetSong(XmlNode entryNode)
        {
            ITempoRange mixableRange = new TempoRange();
            ISong song = new Song(_xmlWrapper, mixableRange)
            {
                EntryNode = entryNode
            };
            song.Load();

            return song;
        }

        internal string GetDefaultCollectionPath()
        {
            string[] paths = { ConfigurationManager.AppSettings["LibraryPath"].ToString(), CollectionFileName };
            return Path.Combine(paths);
        }
    }
}
