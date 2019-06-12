using MixableRangeInterface;
using System;

namespace MixableRangeImplementation
{
    public class TempoRange : ITempoRange
    {
        public double SlowestTempo { get; set; }
        public double FastestTempo { get; set; }
        public double SlowestHalfTempo { get; set; }
        public double FastestHalfTempo { get; set; }
        public double SlowestDoubleTempo { get; set; }
        public double FastestDoubleTempo { get; set; }

        public void Load(double tempo, int tempoRange)
        {
            if (tempo == 0.0) throw new ArgumentOutOfRangeException("tempo is 0.0");
            if (tempoRange == 0) throw new ArgumentOutOfRangeException("tempoRange is 0");

            FastestTempo = GetFastestTempo(tempo, tempoRange);
            SlowestTempo = GetSlowestTempo(tempo, tempoRange);
            FastestDoubleTempo = GetFastestDoubleTempo(FastestTempo);
            SlowestDoubleTempo = GetSlowestDoubleTempo(SlowestTempo);
            FastestHalfTempo = GetFastestHalfTempo(FastestTempo);
            SlowestHalfTempo = GetSlowestHalfTempo(SlowestTempo);
        }

        internal double GetFastestTempo(double tempo, int tempoRange)
        {
            return (tempo + tempoRange);
        }

        internal double GetSlowestTempo(double tempo, int tempoRange)
        {
            return (tempo - tempoRange);
        }

        internal double GetFastestDoubleTempo(double fastestTempo)
        {
            return (fastestTempo * 2);
        }

        internal double GetSlowestDoubleTempo(double slowestTempo)
        {
            return (slowestTempo * 2);
        }

        internal double GetFastestHalfTempo(double fastestTempo)
        {
            return (fastestTempo / 2);
        }

        internal double GetSlowestHalfTempo(double slowestTempo)
        {
            return (slowestTempo / 2);
        }
    }
}
