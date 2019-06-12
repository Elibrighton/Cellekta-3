using MixableRangeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixableRangeImplementation
{
    public class HarmonicKeyRange : IHarmonicKeyRange
    {
        public string InnerCircleHarmonicKey { get; set; }
        public string OuterCircleHarmonicKey { get; set; }
        public string PlusOneHarmonicKey { get; set; }
        public string MinusOneHarmonicKey { get; set; }

        public void Load(string harmonicKey)
        {
            if (!string.IsNullOrEmpty(harmonicKey))
            {
                var keyNumber = Convert.ToInt32(harmonicKey.Replace("A", "").Replace("B", ""));
                var keyLetter = harmonicKey.Contains("A") ? "A" : "B";

                InnerCircleHarmonicKey = GetInnerCircleHarmonicKey(harmonicKey, keyNumber, keyLetter);
                OuterCircleHarmonicKey = GetOuterCircleHarmonicKey(harmonicKey, keyNumber, keyLetter);
                PlusOneHarmonicKey = GetPlusOneHarmonicKey(keyNumber, keyLetter);
                MinusOneHarmonicKey = GetMinusOneHarmonicKey(keyNumber, keyLetter);
            }
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
