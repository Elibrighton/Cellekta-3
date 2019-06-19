using MixDiscInterface;
using SongInterface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MixDiscImplementation
{
    public class MixDisc : IMixDisc
    {
        public ISong BaseTrack { get; set; }
        public List<ISong> PlaylistTracks { get; set; }
        public int MinPlaytime { get; set; }
        public string IntensityStyle { get; set; }
        public int MixLength { get; set; }
        public List<List<ISong>> MixDiscTracksList { get; set; }

        private List<List<ISong>> _matchingCombinations;

        public MixDisc()
        {
            _matchingCombinations = new List<List<ISong>>();
            MixDiscTracksList = new List<List<ISong>>();
        }

        public List<ISong> GetBestMatch()
        {
            var bestMatch = new List<ISong>();

            if (BaseTrack != null)
            {
                var baseTrackList = new List<ISong>
                {
                    BaseTrack
                };

                CombineTracks(baseTrackList, PlaylistTracks, MinPlaytime);
                bestMatch = GetFinalBestMatch();
            }

            return bestMatch;
        }

        public List<ISong> GetFinalBestMatch()
        {
            var bestMatch = new List<ISong>();

            if (MixDiscTracksList.Count > 0)
            {
                _matchingCombinations = MixDiscTracksList;
            }

            if (_matchingCombinations.Count == 1)
            {
                bestMatch = _matchingCombinations[0];
            }
            else if (_matchingCombinations.Count > 1)
            {
                switch (IntensityStyle)
                {
                    case "Lowest":
                    case "Highest":
                        bestMatch = GetBestIntensityMatch();
                        break;
                    case "Random":
                    default:
                        bestMatch = GetRandomMatch();
                        break;
                }
            }

            return bestMatch;
        }

        internal void CombineTracks(List<ISong> trackCombination, List<ISong> playlistTracks, int minPlaytime)
        {
            var trailingTrack = GetTrailingTrack(trackCombination);
            var mixableTracks = GetMixableTracks(trailingTrack, playlistTracks, trackCombination);

            if (mixableTracks.Count > 0)
            {
                var newTrackCombinations = new List<List<ISong>>();

                foreach (var mixableTrack in mixableTracks)
                {
                    var newTrackCombination = GetNewTrackCombination(mixableTrack, trackCombination);
                    newTrackCombinations.Add(newTrackCombination);
                }

                if (newTrackCombinations.Count() > 0)
                {
                    foreach (var newTrackCombination in newTrackCombinations)
                    {
                        if (IsPlaytimeReached(newTrackCombination, minPlaytime))
                        {
                            _matchingCombinations.Add(newTrackCombination);
                        }
                        else
                        {
                            CombineTracks(newTrackCombination, playlistTracks, minPlaytime);
                        }
                    }
                }
            }
        }

        internal List<ISong> GetNewTrackCombination(ISong mixableTrack, List<ISong> trackCombination)
        {
            var newTrackCombination = new List<ISong>();

            foreach (var track in trackCombination)
            {
                newTrackCombination.Add(track);
            }

            newTrackCombination.Add(mixableTrack);

            return newTrackCombination;
        }

        internal ISong GetTrailingTrack(List<ISong> trackCombination)
        {
            return trackCombination[trackCombination.Count() - 1];
        }

        internal bool IsPlaytimeReached(List<ISong> trackCombination, int minPlaytime)
        {
            var isPlaytimeReached = false;
            var totalCombinationPlaytime = GetTotalCombinationPlayTime(trackCombination);

            if (totalCombinationPlaytime >= minPlaytime)
            {
                isPlaytimeReached = true;
            }

            return isPlaytimeReached;
        }

        internal List<ISong> GetMixableTracks(ISong trailingTrack, List<ISong> playlistTracks, List<ISong> mixableTrackCombination)
        {
            return playlistTracks.Where(t =>
                t != trailingTrack
                && (mixableTrackCombination == null
                || !mixableTrackCombination.Contains(t))
                && (((Math.Round(t.LeadingTempo, 3) <= trailingTrack.TempoRange.FastestTempo
                && Math.Round(t.LeadingTempo, 3) >= trailingTrack.TempoRange.SlowestTempo)
                || (Math.Round(t.LeadingTempo, 3) <= trailingTrack.TempoRange.FastestHalfTempo
                && Math.Round(t.LeadingTempo, 3) >= trailingTrack.TempoRange.SlowestHalfTempo)
                || (Math.Round(t.LeadingTempo, 3) <= trailingTrack.TempoRange.FastestDoubleTempo
                && Math.Round(t.LeadingTempo, 3) >= trailingTrack.TempoRange.SlowestDoubleTempo))
                && (t.LeadingHarmonicKey == trailingTrack.HarmonicKeyRange.InnerCircleHarmonicKey
                || t.LeadingHarmonicKey == trailingTrack.HarmonicKeyRange.OuterCircleHarmonicKey
                || t.LeadingHarmonicKey == trailingTrack.HarmonicKeyRange.PlusOneHarmonicKey
                || t.LeadingHarmonicKey == trailingTrack.HarmonicKeyRange.MinusOneHarmonicKey)
                && (string.IsNullOrEmpty(IntensityStyle)
                || (t.Intensity <= trailingTrack.IntensityRange.PlusOneIntensity
                && t.Intensity >= trailingTrack.IntensityRange.MinusOneIntensity)))).ToList();
        }

        internal List<ISong> GetBestIntensityMatch()
        {
            var intensityMatch = new List<ISong>();
            var bestIntensityCombinationMatches = GetBestIntensityCombinationMatches(_matchingCombinations);

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

            var randomIndex = GetRandomIndex(_matchingCombinations);
            randomMatch = _matchingCombinations[randomIndex];

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

        internal int GetTotalCombinationPlayTime(List<ISong> mixableTrackCombination)
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
