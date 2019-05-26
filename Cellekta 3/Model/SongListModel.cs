using Cellekta_3.Services;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Configuration;
using System;

namespace Cellekta_3.Model
{
    public class SongListModel : ISongListModel, IDisposable
    {
        const string TraktorLibraryFileName = "collection.nml";

        public ObservableCollection<ISong> Songs { get; set; }
        public string TraktorLibraryPath { get; set; }
        public string StatusMessage { get; set; }

        #region Constructor

        public SongListModel()
        {
            Songs = new ObservableCollection<ISong>();
            StatusMessage = "Ready..";

            // Dummy data for testing
            Songs.Add(new Song { Artist = "Acusmouse", Title = "Little Helper 344-1 (Original Mix)", Tempo = 125.0, HarmonicKey = "10A" });
            Songs.Add(new Song { Artist = "Block & Crown", Title = "Betty Never Sleeps (Original Mix)", Tempo = 124.0, HarmonicKey = "10A" });
            Songs.Add(new Song { Artist = "Chicks Luv Us", Title = "Qu'est Ce Qu'il Veut Lui-! (Original Mix)", Tempo = 125.0, HarmonicKey = "10A" });
            Songs.Add(new Song { Artist = "Mambo Brothers", Title = "Slow [Original Mix] 10A 124", Tempo = 124.0, HarmonicKey = "10A" });
        } 

        #endregion

        #region Private methods

        internal void SetTraktorLibraryPath()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = ConfigurationManager.AppSettings["LibraryPath"];

            if (openFileDialog.ShowDialog() == true)
            {
                TraktorLibraryPath = File.ReadAllText(openFileDialog.FileName);
            }
        }

        internal void DeleteWorkingTraktorLibrary()
        {
            var workingTraktorLibrary = string.Concat(Path.GetTempPath(), TraktorLibraryFileName);

            if (File.Exists(workingTraktorLibrary))
            {
                File.Delete(workingTraktorLibrary);
            }
        }

        #endregion

        #region Public methods

        public bool IsTraktorLibraryFound()
        {
            SetTraktorLibraryPath();

            return File.Exists(string.Concat(TraktorLibraryPath, TraktorLibraryFileName));
        }

        public void ImportTraktorLibrary()
        {
            DeleteWorkingTraktorLibrary();
        }

        public void Dispose()
        {
            DeleteWorkingTraktorLibrary();
        }

        #endregion
    }
}
