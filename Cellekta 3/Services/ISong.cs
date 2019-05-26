using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Cellekta_3.Services.Song;

namespace Cellekta_3.Services
{
    public interface ISong
    {
        string Artist { get; set; }
        string Title { get; set; }
        double Tempo { get; set; }
        string HarmonicKey { get; set; }
    }
}
