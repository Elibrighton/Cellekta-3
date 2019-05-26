using System.Xml;

namespace SongInterface
{
    public interface ISong
    {
        string Artist { get; set; }
        string Title { get; set; }
        double LeadingTempo { get; set; }
        double TrailingTempo { get; set; }
        string HarmonicKey { get; set; }
        string FullName { get; set; }
        string Playlist { get; set; }
        string TempoText { get; }

        void Load();
    }
}
