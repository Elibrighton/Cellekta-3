using SongInterface;

namespace SongImplementation
{
    public class Song : ISong
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public double Tempo { get; set; }
        public string HarmonicKey { get; set; }
    }
}
