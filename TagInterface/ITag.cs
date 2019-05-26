using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagInterface
{
    public interface ITag
    {
        double LeadingTempo { get; set; }
        double TrailingTempo { get; set; }

        void Load();
    }
}
