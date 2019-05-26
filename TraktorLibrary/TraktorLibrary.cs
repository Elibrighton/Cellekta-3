using Microsoft.Win32;
using SongImplementation;
using SongInterface;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
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
            _workingCollection = string.Empty;

            Songs = new ObservableCollection<ISong>();

            // Dummy data for testing
            Songs.Add(new Song { Artist = "Acusmouse", Title = "Little Helper 344-1 (Original Mix)", Tempo = 125.0, HarmonicKey = "10A" });
            Songs.Add(new Song { Artist = "Block & Crown", Title = "Betty Never Sleeps (Original Mix)", Tempo = 124.0, HarmonicKey = "10A" });
            Songs.Add(new Song { Artist = "Chicks Luv Us", Title = "Qu'est Ce Qu'il Veut Lui-! (Original Mix)", Tempo = 125.0, HarmonicKey = "10A" });
            Songs.Add(new Song { Artist = "Mambo Brothers", Title = "Slow [Original Mix] 10A 124", Tempo = 124.0, HarmonicKey = "10A" });
        }

        internal string GetWorkingPath()
        {
            return string.Concat(Path.GetTempPath(), CollectionFileName);
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
            var traktorLibraryPath = string.Concat(ConfigurationManager.AppSettings["LibraryPath"], CollectionFileName);
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

            return File.Exists(WorkingCollectionPath);
        }

        public void ImportCollection()
        {
            DeleteWorkingCollection();
            CreateWorkingCollection();
            LoadWorkingCollection();

            if (!string.IsNullOrEmpty(_workingCollection))
            {
                _xmlWrapper.Load();
            }
        }

        public void Dispose()
        {
            DeleteWorkingCollection();
        }
    }
}
