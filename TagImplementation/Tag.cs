using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TagInterface;

namespace TagImplementation
{
    public class Tag : ITag
    {
        public double LeadingTempo { get; set; }
        public double TrailingTempo { get; set; }

        private string _fullName;
        private TagLib.File _file;

        public Tag(string fullName)
        {
            if (string.IsNullOrEmpty(fullName)) throw new ArgumentNullException("fullName is null or empty");

            _fullName = fullName;
        }

        public void Load()
        {
            _file = TagLib.File.Create(_fullName);
            LeadingTempo = GetTempo(_file.Tag.Title);
            TrailingTempo = GetTempo(_file.Tag.FirstArtist);
        }

        internal double GetTempo(string tag)
        {
            var tempo = 0.0;

            if (!IsNumberedArtist(tag))
            {
                var match = Regex.Match(tag, @"^[0-9][0-9][0-9]?", RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    tempo = Convert.ToDouble(match.Value);
                }
            }

            return tempo;
        }

        internal bool IsNumberedArtist(string tag)
        {
            var isNumberedArtist = false;

            if (!string.IsNullOrEmpty(tag))
            {
                string[] numberedArtist = { @"^50\scent",
                                        @"^5\sSeconds\sOf\sSummer",
                                        @"^360",
                                        @"^50\sWays\sTo\sSay\sGoodbye",
                                        @"^20 fingers" };
                // where is the rest of this list?

                foreach (string artist in numberedArtist)
                {
                    if (Regex.IsMatch(tag, artist, RegexOptions.IgnoreCase))
                    {
                        isNumberedArtist = true;
                        break;
                    }
                }
            }
            return isNumberedArtist;
        }
    }
}
