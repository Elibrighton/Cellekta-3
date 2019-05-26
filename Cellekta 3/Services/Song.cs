using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cellekta_3.Services
{
    public class Song : ISong
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public double Tempo { get; set; }
        public string HarmonicKey { get; set; }
    }
}
