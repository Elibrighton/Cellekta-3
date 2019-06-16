using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixableRangeInterface
{
    public interface IIntensityRange
    {
        int PlusOneIntensity { get; set; }
        int MinusOneIntensity { get; set; }

        void Load(int intensity);
    }
}
