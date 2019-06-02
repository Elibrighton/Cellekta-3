namespace MixableRangeInterface
{
    public interface IMixableRange
    {
        double SlowestTempo { get; set; }
        double FastestTempo { get; set; }
        double SlowestHalfTempo { get; set; }
        double FastestHalfTempo { get; set; }
        double SlowestDoubleTempo { get; set; }
        double FastestDoubleTempo { get; set; }
        string InnerCircleHarmonicKey { get; set; }
        string OuterCircleHarmonicKey { get; set; }
        string PlusOneHarmonicKey { get; set; }
        string MinusOneHarmonicKey { get; set; }

        void Load(double tempo, int tempoRange, string harmonicKey);
    }
}
