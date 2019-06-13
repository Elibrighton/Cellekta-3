using MixableRangeInterface;
using System.Collections.Generic;

namespace HarmonicKeyInterface
{
    public interface IHarmonicKey
    {
        string Name { get; set; }
        IHarmonicKeyRange HarmonicKeyRange { get; set; }
    }
}
