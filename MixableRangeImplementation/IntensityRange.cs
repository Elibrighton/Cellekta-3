using MixableRangeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixableRangeImplementation
{
    public class IntensityRange : IIntensityRange
    {
        public int PlusOneIntensity { get; set; }
        public int MinusOneIntensity { get; set; }

        public void Load(int intensity)
        {
            PlusOneIntensity = (intensity + 1);
            MinusOneIntensity = (intensity - 1);
        }
    }
}
