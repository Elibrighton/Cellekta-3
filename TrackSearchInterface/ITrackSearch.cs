using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSearchInterface
{
    public interface ITrackSearch
    {
        string Text { get; set; }
        string Artist { get; }
        string Title { get; }

        void Reset();
        void Load();
    }
}
