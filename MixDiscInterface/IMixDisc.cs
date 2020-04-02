using SongInterface;
using System.Collections.Generic;

namespace MixDiscInterface
{
    public interface IMixDisc
    {
        List<ISong> PlaylistTracks { get; set; }
        string IntensityStyle { get; set; }
        int MinPlaytime { get; set; }
        int MixLength { get; set; }
        List<List<ISong>> MatchingTrackCombinationList { get; set; }
        List<ISong> BaseTrackList { get; set; }

        List<ISong> GetBestMatch();
        List<ISong> GetFinalBestMatch();

    }
}
