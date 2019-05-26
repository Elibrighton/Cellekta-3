using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cellekta_3.Services
{
    public class HarmonicKey : IHarmonicKey
    {
        public Dictionary<int, string> HarmonicKeys { get; set; }

        public HarmonicKey()
        {
            HarmonicKeys = new Dictionary<int, string>()
            {
                { 0, "1d" },
                { 1, "8d" },
                { 2, "3d" },
                { 3, "10d" },
                { 4, "5d" },
                { 5, "12d" },
                { 6, "7d" },
                { 7, "2d" },
                { 8, "9d" },
                { 9, "4d" },
                { 10, "11d" },
                { 11, "6d" },
                { 12, "10m" },
                { 13, "5m" },
                { 14, "12m" },
                { 15, "7m" },
                { 16, "2m" },
                { 17, "9m" },
                { 18, "4m" },
                { 19, "11m" },
                { 20, "6m" },
                { 21, "1m" },
                { 22, "8m" },
                { 23, "3m" }
            };
        }
    }
}
