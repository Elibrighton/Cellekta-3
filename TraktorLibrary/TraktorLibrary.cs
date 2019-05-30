using Microsoft.Win32;
using SongImplementation;
using SongInterface;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Xml;
using TraktorLibraryInterface;
using XmlWrapperInterface;

namespace TraktorLibraryImplementation
{
    public class TraktorLibrary : ITraktorLibrary, IDisposable
    {
        const string CollectionFileName = "collection.nml";

        public ObservableCollection<ISong> Songs { get; set; }
        public string CollectionPath { get; set; }
        public string WorkingCollectionPath { get; set; }

        private string _workingCollection;
        private IXmlWrapper _xmlWrapper;

        public TraktorLibrary(IXmlWrapper xmlWrapper)
        {
            _xmlWrapper = xmlWrapper;

            WorkingCollectionPath = GetWorkingPath();
            Songs = new ObservableCollection<ISong>();

            // Dummy data for testing
            Songs.Add(new Song { Artist = "Acusmouse", Title = "Little Helper 344-1 (Original Mix)", LeadingTempo = 125.0, TrailingTempo = 125.0, LeadingHarmonicKey = "10A" });
            Songs.Add(new Song { Artist = "Block & Crown", Title = "Betty Never Sleeps (Original Mix)", LeadingTempo = 124.0, TrailingTempo = 125.0, LeadingHarmonicKey = "10A" });
            Songs.Add(new Song { Artist = "Chicks Luv Us", Title = "Qu'est Ce Qu'il Veut Lui-! (Original Mix)", LeadingTempo = 125.0, TrailingTempo = 125.0, LeadingHarmonicKey = "10A" });
            Songs.Add(new Song { Artist = "Mambo Brothers", Title = "Slow [Original Mix] 10A 124", LeadingTempo = 124.0, TrailingTempo = 125.0, LeadingHarmonicKey = "10A" });
        }

        internal string GetWorkingPath()
        {
            return Path.Combine(Path.GetTempPath(), CollectionFileName);
        }

        internal void SetCollectionPath()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = ConfigurationManager.AppSettings["LibraryPath"];

            if (openFileDialog.ShowDialog() == true)
            {
                CollectionPath = openFileDialog.FileName;
            }
        }

        internal void DeleteWorkingCollection()
        {
            if (File.Exists(WorkingCollectionPath))
            {
                File.Delete(WorkingCollectionPath);
            }
        }

        internal void CreateWorkingCollection()
        {
            var traktorLibraryPath = Path.Combine(ConfigurationManager.AppSettings["LibraryPath"], CollectionFileName);
            File.Copy(traktorLibraryPath, WorkingCollectionPath, true);
        }

        internal void LoadWorkingCollection()
        {
            if (File.Exists(WorkingCollectionPath))
            {
                _workingCollection = File.ReadAllText(WorkingCollectionPath);
            }
        }

        public bool IsCollectionFound()
        {
            SetCollectionPath();

            return File.Exists(CollectionPath);
        }

        public void ImportCollection()
        {
            DeleteWorkingCollection();
            CreateWorkingCollection();
            // call async
            LoadWorkingCollection();

            if (!string.IsNullOrEmpty(_workingCollection))
            {
                _xmlWrapper.Load();

                foreach (XmlNode collectionNode in _xmlWrapper.XmlDocument.DocumentElement.SelectNodes("/NML/COLLECTION"))
                {
                    foreach (XmlNode entryNode in collectionNode.SelectNodes("ENTRY"))
                    {
                        ISong song = new Song(_xmlWrapper, entryNode);
                        song.Load();
                        //song.GetRating();
                        Songs.Add(song);
                    }
                }
            }
        }

        public void Dispose()
        {
            DeleteWorkingCollection();
        }
    }
}
