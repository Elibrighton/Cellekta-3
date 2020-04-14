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
            FastestDoubleTempo = GetFastestDoubleTempo(tempo, tempoRange);
            SlowestDoubleTempo = GetSlowestDoubleTempo(tempo, tempoRange);
            FastestHalfTempo = GetFastestHalfTempo(tempo, tempoRange);
            SlowestHalfTempo = GetSlowestHalfTempo(tempo, tempoRange);
        }

        internal double GetFastestTempo(double tempo, int tempoRange)
        {
            return Math.Round(tempo + tempoRange, 3);
        }

        internal double GetSlowestTempo(double tempo, int tempoRange)
        {
            return Math.Round(tempo - tempoRange, 3);
        }

        internal double GetFastestDoubleTempo(double tempo, int tempoRange)
        {
            return Math.Round((tempo * 2) + tempoRange, 3);
        }

        internal double GetSlowestDoubleTempo(double tempo, int tempoRange)
        {
            return Math.Round((tempo * 2) - tempoRange, 3);
        }

        internal double GetFastestHalfTempo(double tempo, int tempoRange)
        {
            return Math.Round((tempo / 2) + tempoRange, 3);
        }

        internal double GetSlowestHalfTempo(double tempo, int tempoRange)
        {
            return Math.Round((tempo / 2) - tempoRange, 3);
        }
    }
}
