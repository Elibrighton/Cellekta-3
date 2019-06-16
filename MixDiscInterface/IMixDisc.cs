using SongInterface;
using System.Collections.Generic;

namespace MixDiscInterface
{
    public interface IMixDisc
    {
        void Find(ISong firstTrack, List<ISong> playlistTracks);
    }
}
