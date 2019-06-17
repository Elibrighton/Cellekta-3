using SongInterface;
using System.Collections.Generic;

namespace MixDiscInterface
{
    public interface IMixDisc
    {
        int MinPlaytime { get; set; }
        List<List<ISong>> Matches { get; set; }
        string IntensityStyle { get; set; }
        List<ISong> IntensityMatch { get; set; }
        int MixLength { get; set; }

        void SetMatches(ISong firstTrack, List<ISong> playlistTracks);
        void SetIntensityMatch();

    }
}
