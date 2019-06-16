using SongInterface;
using System.Collections.Generic;

namespace MixDiscInterface
{
    public interface IMixDisc
    {
        int MinPlaytime { get; set; }
        List<List<ISong>> Matches { get; set; }

        void Find(ISong firstTrack, List<ISong> playlistTracks);
    }
}
