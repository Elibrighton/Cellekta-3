using MixDiscInterface;
using SongInterface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MixDiscImplementation
{
    public class MixDisc : IMixDisc
    {
        public int MinPlaytime { get; set; }
        public List<List<ISong>> Matches { get; set; }


        public void Find(ISong firstTrack, List<ISong> playlistTracks)
        {
            var initialTrackCombinations = GetInitialTrackCombinations(firstTrack, playlistTracks);
            Matches = GetCombinationMatches(initialTrackCombinations, playlistTracks);
        }

        internal List<List<ISong>> GetCombinationMatches(List<List<ISong>> initialTrackCombinations, List<ISong> playlistTracks)
        {
            var newTrackAdded = true;
            var iterator = 1; // start at second track of initial combination
            var combinationMatches = new List<List<ISong>>();

            while (newTrackAdded || iterator < initialTrackCombinations.Count)
            {
                newTrackAdded = false;

                foreach (var trackCombination in initialTrackCombinations)
                {
                    if (GetCombinationPlayTime(trackCombination) > MinPlaytime)
                    {
                        combinationMatches.Add(trackCombination);
                    }
                    else if (iterator < trackCombination.Count)
                    {
                        var trailingTrack = trackCombination[iterator];
                        var nextTrack = GetTracksInMixableRange(trailingTrack, playlistTracks, trackCombination).FirstOrDefault();

                        if (nextTrack != null)
                        {
                            trackCombination.Add(nextTrack);
                            newTrackAdded = true;

                            break;
                        }
                    }
                }

                iterator++;
            }

            return combinationMatches;
        }

        internal List<List<ISong>> GetInitialTrackCombinations(ISong firstTrack, List<ISong> playlistTracks)
        {
            var tracksInMixableRange = GetTracksInMixableRange(firstTrack, playlistTracks, null);
            var initialTrackCombinations = new List<List<ISong>>();

            foreach (var nextTrack in tracksInMixableRange)
            {
                var mixableTrackCombination = new List<ISong>
                {
                        firstTrack,
                        nextTrack
                };
                initialTrackCombinations.Add(mixableTrackCombination);
            }

            return initialTrackCombinations;
        }

        internal List<ISong> GetTracksInMixableRange(ISong track1, List<ISong> playlistTracks, List<ISong> mixableTrackCombination)
        {
            return playlistTracks.Where(t =>
                (mixableTrackCombination == null
                || !mixableTrackCombination.Contains(t))
                && (((t.LeadingTempo <= track1.TempoRange.FastestTempo
                && t.LeadingTempo >= track1.TempoRange.SlowestTempo)
                || (t.LeadingTempo <= track1.TempoRange.FastestHalfTempo
                && t.LeadingTempo >= track1.TempoRange.SlowestHalfTempo)
                || (t.LeadingTempo <= track1.TempoRange.FastestDoubleTempo
                && t.LeadingTempo >= track1.TempoRange.SlowestDoubleTempo))
                && (t.LeadingHarmonicKey == track1.HarmonicKeyRange.InnerCircleHarmonicKey
                || t.LeadingHarmonicKey == track1.HarmonicKeyRange.OuterCircleHarmonicKey
                || t.LeadingHarmonicKey == track1.HarmonicKeyRange.PlusOneHarmonicKey
                || t.LeadingHarmonicKey == track1.HarmonicKeyRange.MinusOneHarmonicKey))).ToList();
        }

        internal int GetCombinationPlayTime(List<ISong> mixableTrackCombination)
        {
            var combinationPlayTime = 0;

            foreach (var track in mixableTrackCombination)
            {
                combinationPlayTime += track.PlayTime;
            }

            return combinationPlayTime;
        }
    }
}
