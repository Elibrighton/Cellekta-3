using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackSearchInterface;

namespace TrackSearchImplementation
{
    public class TrackSearch : ITrackSearch
    {
        public string Text { get; set; }
        public string Artist { get; private set; }
        public string Title { get; private set; }

        public void Reset()
        {
            Text = string.Empty;
            Artist = string.Empty;
            Title = string.Empty;
        }

        public void Load()
        {

            if (!string.IsNullOrEmpty(Text) && Text.Contains(" -"))
            {
                var index = Text.IndexOf('-');

                if (Text.Trim().Count() == index + 1)
                {
                    Text = Text.Replace('-', ' ').Trim();
                }
                else
                {
                    Artist = Text.Substring(0, index).Trim();
                    Title = Text.Substring(index + 1).Trim();
                }
            }
        }
    }
}
