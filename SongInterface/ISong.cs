using System.Xml;

namespace SongInterface
{
    public interface ISong
    {
        string Artist { get; set; }
        string Title { get; set; }
        double LeadingTempo { get; set; }
        double TrailingTempo { get; set; }
        string LeadingHarmonicKey { get; set; }
        string TrailingHarmonicKey { get; set; }
        string Path { get; set; }
        string Playlist { get; set; }
        string TempoText { get; }
        int PlayTime { get; set; }
        int Intensity { get; set; }
        XmlNode EntryNode { get; set; }
        bool IsCharting { get; set; }

        void Load();
    }
}
