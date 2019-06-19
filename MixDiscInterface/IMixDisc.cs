using SongInterface;
using System.Collections.Generic;

namespace MixDiscInterface
{
    public interface IMixDisc
    {
        ISong BaseTrack { get; set; }
        List<ISong> PlaylistTracks { get; set; }
        string IntensityStyle { get; set; }
        int MinPlaytime { get; set; }
        int MixLength { get; set; }
        List<List<ISong>> MixDiscTracksList { get; set; }

        List<ISong> GetBestMatch();
        List<ISong> GetFinalBestMatch();

    }
}
