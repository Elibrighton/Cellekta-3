using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixableRangeInterface
{
    public interface IHarmonicKeyRange
    {
        string InnerCircleHarmonicKey { get; set; }
        string OuterCircleHarmonicKey { get; set; }
        string PlusOneHarmonicKey { get; set; }
        string MinusOneHarmonicKey { get; set; }

        void Load(string harmonicKey);
    }
}
