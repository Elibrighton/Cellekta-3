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
        public string IntensityStyle { get; set; }
        public List<ISong> IntensityMatch { get; set; }
        public int MixLength { get; set; }


        public void SetMatches(ISong firstTrack, List<ISong> playlistTracks)
        {
            var initialTrackCombinations = GetInitialTrackCombinations(firstTrack, playlistTracks);
            Matches = GetCombinationMatches(initialTrackCombinations, playlistTracks);
        }

        public void SetIntensityMatch()
        {
            if (Matches.Count == 1)
            {
                IntensityMatch = Matches[0];
            }
            else if (Matches.Count > 1)
            {
                switch (IntensityStyle)
                {
                    case "Lowest":
                    case "Highest":
                        IntensityMatch = GetBestIntensityMatch();
                        break;
                    case "Random":
                    default:
                        IntensityMatch = GetRandomMatch();
                        break;
                }
            }
        }

        internal List<ISong> GetBestIntensityMatch()
        {
            var intensityMatch = new List<ISong>();
            var bestIntensityCombinationMatches = GetBestIntensityCombinationMatches(Matches);

            if (bestIntensityCombinationMatches.Count == 1)
            {
                intensityMatch = bestIntensityCombinationMatches[0];
            }
            else
            {
                var bestWeightedIntensityCombinationMatches = GetBestIntensityCombinationMatches(bestIntensityCombinationMatches, true);

                if (bestWeightedIntensityCombinationMatches.Count == 1)
                {
                    intensityMatch = bestWeightedIntensityCombinationMatches[0];
                }
                else
                {
                    var randomIndex = GetRandomIndex(bestWeightedIntensityCombinationMatches);
                    intensityMatch = bestWeightedIntensityCombinationMatches[randomIndex];
                }
            }

            return intensityMatch;
        }

        internal int GetRandomIndex(List<List<ISong>> combinationMatches)
        {
            var randomIndex = 0;

            if (combinationMatches.Count > 1)
            {
                var random = new Random();
                randomIndex = random.Next(combinationMatches.Count);
            }

            return randomIndex;
        }

        internal List<ISong> GetRandomMatch()
        {
            var randomMatch = new List<ISong>();

            var randomIndex = GetRandomIndex(Matches);
            randomMatch = Matches[randomIndex];

            return randomMatch;
        }

        internal List<List<ISong>> GetBestIntensityCombinationMatches(List<List<ISong>> matches, bool isWeighted = false)
        {
            var bestIntensityAverage = 0.0;
            var bestIntensityCombinationMatches = new List<List<ISong>>();

            foreach (var combinationMatch in matches)
            {
                var intensityCount = isWeighted ? GetWeightedIntensityCount(combinationMatch) : GetIntensityCount(combinationMatch);
                var intensityAverage = GetIntensityAverage(intensityCount, combinationMatch.Count());

                if (intensityAverage == bestIntensityAverage || bestIntensityAverage == 0.0)
                {
                    if (bestIntensityAverage == 0.0)
                    {
                        bestIntensityAverage = intensityAverage;
                    }

                    bestIntensityCombinationMatches.Add(combinationMatch);
                }
                else if (((IntensityStyle == "Highest" || isWeighted) && intensityAverage > bestIntensityAverage)
                    || (IntensityStyle == "Lowest" && !isWeighted && intensityAverage < bestIntensityAverage))
                {
                    bestIntensityAverage = intensityAverage;
                    bestIntensityCombinationMatches = new List<List<ISong>>
                    {
                        combinationMatch
                    };
                }
            }

            return bestIntensityCombinationMatches;
        }

        internal double GetIntensityAverage(int intensityCount, int combinationMatchCount)
        {
            return ((double)intensityCount / combinationMatchCount);
        }

        internal int GetWeightedIntensityCount(List<ISong> combinationMatch)
        {
            var weightedIntensityCount = 0;

            for (int i = 0; i < combinationMatch.Count; i++)
            {
                var track = combinationMatch[i];
                weightedIntensityCount += ((i + 1) * track.Intensity);
            }

            return weightedIntensityCount;
        }

        internal int GetIntensityCount(List<ISong> combinationMatch)
        {
            var intensityCount = 0;

            foreach (var track in combinationMatch)
            {
                intensityCount += track.Intensity;
            }

            return intensityCount;
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
                    var combinationPlaytime = GetCombinationPlayTime(trackCombination);

                    if (combinationPlaytime >= MinPlaytime)
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

                            continue;
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
                t != track1
                && (mixableTrackCombination == null
                || !mixableTrackCombination.Contains(t))
                && (((Math.Round(t.LeadingTempo, 3) <= track1.TempoRange.FastestTempo
                && Math.Round(t.LeadingTempo, 3) >= track1.TempoRange.SlowestTempo)
                || (Math.Round(t.LeadingTempo, 3) <= track1.TempoRange.FastestHalfTempo
                && Math.Round(t.LeadingTempo, 3) >= track1.TempoRange.SlowestHalfTempo)
                || (Math.Round(t.LeadingTempo, 3) <= track1.TempoRange.FastestDoubleTempo
                && Math.Round(t.LeadingTempo, 3) >= track1.TempoRange.SlowestDoubleTempo))
                && (t.LeadingHarmonicKey == track1.HarmonicKeyRange.InnerCircleHarmonicKey
                || t.LeadingHarmonicKey == track1.HarmonicKeyRange.OuterCircleHarmonicKey
                || t.LeadingHarmonicKey == track1.HarmonicKeyRange.PlusOneHarmonicKey
                || t.LeadingHarmonicKey == track1.HarmonicKeyRange.MinusOneHarmonicKey)
                && (string.IsNullOrEmpty(IntensityStyle)
                || (t.Intensity <= track1.IntensityRange.PlusOneIntensity
                && t.Intensity >= track1.IntensityRange.MinusOneIntensity)))).ToList();
        }

        internal int GetCombinationPlayTime(List<ISong> mixableTrackCombination)
        {
            var combinationPlayTime = 0;

            foreach (var track in mixableTrackCombination)
            {
                combinationPlayTime += track.PlayTime;
            }

            var combinationMixTime = GetCombinationMixTime(mixableTrackCombination.Count() - 1);
            combinationPlayTime -= combinationMixTime;

            return combinationPlayTime;
        }

        internal int GetCombinationMixTime(int mixableTrackCombinationCount)
        {
            return (mixableTrackCombinationCount * MixLength);
        }
    }
}
