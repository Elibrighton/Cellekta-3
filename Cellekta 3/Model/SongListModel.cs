using Cellekta_3.Services;
using System.Collections.ObjectModel;

namespace Cellekta_3.Model
{
    public class SongListModel : ISongListModel
    {
        public ObservableCollection<ISong> Songs { get; set; }

        public SongListModel()
        {
            Songs = new ObservableCollection<ISong>();

            Songs.Add(new Song { Artist = "Acusmouse", Title = "Little Helper 344-1 (Original Mix)", Tempo = 125.0, HarmonicKey = "10A" });
            Songs.Add(new Song { Artist = "Block & Crown", Title = "Betty Never Sleeps (Original Mix)", Tempo = 124.0, HarmonicKey = "10A" });
            Songs.Add(new Song { Artist = "Chicks Luv Us", Title = "Qu'est Ce Qu'il Veut Lui-! (Original Mix)", Tempo = 125.0, HarmonicKey = "10A" });
            Songs.Add(new Song { Artist = "Mambo Brothers", Title = "Slow [Original Mix] 10A 124", Tempo = 124.0, HarmonicKey = "10A" });
        }
    }
}
