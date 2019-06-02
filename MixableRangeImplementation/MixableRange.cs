using MixableRangeInterface;
using System;

namespace MixableRangeImplementation
{
    public class MixableRange : IMixableRange
    {
        public double SlowestTempo { get; set; }
        public double FastestTempo { get; set; }
        public double SlowestHalfTempo { get; set; }
        public double FastestHalfTempo { get; set; }
        public double SlowestDoubleTempo { get; set; }
        public double FastestDoubleTempo { get; set; }
        public string InnerCircleHarmonicKey { get; set; }
        public string OuterCircleHarmonicKey { get; set; }
        public string PlusOneHarmonicKey { get; set; }
        public string MinusOneHarmonicKey { get; set; }

        public void Load(double tempo, int tempoRange, string harmonicKey)
        {
            if (tempo == 0.0) throw new ArgumentOutOfRangeException("tempo is 0.0");
            if (tempoRange == 0) throw new ArgumentOutOfRangeException("tempoRange is 0");
            if (harmonicKey == null) throw new ArgumentNullException("harmonicKey is null");

            FastestTempo = GetFastestTempo(tempo, tempoRange);
            SlowestTempo = GetSlowestTempo(tempo, tempoRange);
            FastestDoubleTempo = GetFastestDoubleTempo(FastestTempo);
            SlowestDoubleTempo = GetSlowestDoubleTempo(SlowestTempo);
            FastestHalfTempo = GetFastestHalfTempo(FastestTempo);
            SlowestHalfTempo = GetSlowestHalfTempo(SlowestTempo);
 
            var keyNumber = Convert.ToInt32(harmonicKey.Replace("A", "").Replace("B", ""));
            var keyLetter = harmonicKey.Contains("A") ? "A" : "B";

            InnerCircleHarmonicKey = GetInnerCircleHarmonicKey(harmonicKey, keyNumber, keyLetter);
            OuterCircleHarmonicKey = GetOuterCircleHarmonicKey(harmonicKey, keyNumber, keyLetter);
            PlusOneHarmonicKey = GetPlusOneHarmonicKey(keyNumber, keyLetter);
            MinusOneHarmonicKey = GetMinusOneHarmonicKey(keyNumber, keyLetter);
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

        internal string GetInnerCircleHarmonicKey(string harmonicKey, int keyNumber, string keyLetter)
        {
            return keyLetter == "A" ? harmonicKey : string.Concat(keyNumber, "A");
        }

        internal string GetOuterCircleHarmonicKey(string harmonicKey, int keyNumber, string keyLetter)
        {
            return keyLetter == "B" ? harmonicKey : string.Concat(keyNumber, "B");
        }

        internal string GetPlusOneHarmonicKey(int keyNumber, string keyLetter)
        {
            return string.Concat(keyNumber == 12 ? 1 : (keyNumber + 1), keyLetter);
        }

        internal string GetMinusOneHarmonicKey(int keyNumber, string keyLetter)
        {
            return string.Concat(keyNumber == 1 ? 12 : (keyNumber - 1), keyLetter);
        }
    }
}
