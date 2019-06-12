namespace MixableRangeInterface
{
    public interface ITempoRange
    {
        double SlowestTempo { get; set; }
        double FastestTempo { get; set; }
        double SlowestHalfTempo { get; set; }
        double FastestHalfTempo { get; set; }
        double SlowestDoubleTempo { get; set; }
        double FastestDoubleTempo { get; set; }

        void Load(double tempo, int tempoRange);
    }
}
