using HarmonicKeyInterface;
using MixableRangeInterface;

namespace HarmonicKeyImplementation
{
    public class HarmonicKey : IHarmonicKey
    {
        public string Name { get; set; }
        public IHarmonicKeyRange HarmonicKeyRange { get; set; }

        public HarmonicKey(IHarmonicKeyRange harmonicKeyRange)
        {
            HarmonicKeyRange = harmonicKeyRange;
        }
    }
}
