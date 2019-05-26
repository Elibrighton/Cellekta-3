namespace SongInterface
{
    public interface ISong
    {
        string Artist { get; set; }
        string Title { get; set; }
        double Tempo { get; set; }
        string HarmonicKey { get; set; }
    }
}
