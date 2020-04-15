using MixDiscInterface;
using SongInterface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MixDiscImplementation
{
    public class MixDisc : IMixDisc
    {
        public List<ISong> PlaylistTracks { get; set; }
        public int MinPlaytime { get; set; }
        public string IntensityStyle { get; set; }
        public int MixLength { get; set; }
        public List<List<ISong>> MatchingTrackCombinationList { get; set; }
        public List<ISong> LongestTrackCombinationList { get; set; }
        public List<ISong> BaseTrackList { get; set; }

        public MixDisc()
        {
            MatchingTrackCombinationList = new List<List<ISong>>();
            LongestTrackCombinationList = new List<ISong>();
        }

        public List<ISong> GetBestMatch()
        {
            var bestMatch = new List<ISong>();

            if (IsPlaytimeReached(BaseTrackList, MinPlaytime))
            {
                MatchingTrackCombinationList.Add(BaseTrackList);
            }
            else
            {
                CombineTracks(BaseTrackList, PlaylistTracks, MinPlaytime);
            }

            bestMatch = GetFinalBestMatch();

            return bestMatch;
        }

        public List<ISong> GetFinalBestMatch()
        {
            var bestMatch = new List<ISong>();

            if (MatchingTrackCombinationList.Count == 1)
            {
                bestMatch = MatchingTrackCombinationList[0];
            }
            else if (MatchingTrackCombinationList.Count > 1)
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
                            MatchingTrackCombinationList.Add(newTrackCombination);
                        }
                        else
                        {
                            UpdateLongestTrackCombinationList(newTrackCombination);
                            CombineTracks(newTrackCombination, playlistTracks, minPlaytime);
                        }
                    }
                }
            }
        }

        internal void UpdateLongestTrackCombinationList(List<ISong> newTrackCombination)
        {
            var newTrackCombinationPlaytime = GetTotalCombinationPlayTime(newTrackCombination);
            var longestTrackCombinationPlaytime = GetTotalCombinationPlayTime(LongestTrackCombinationList);

            if (newTrackCombinationPlaytime > longestTrackCombinationPlaytime)
            {
                LongestTrackCombinationList = newTrackCombination;
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

        public bool IsPlaytimeReached(List<ISong> trackCombination, int minPlaytime)
        {
            var isPlaytimeReached = false;
            var totalCombinationPlaytime = GetTotalCombinationPlayTime(trackCombination);

            if (totalCombinationPlaytime >= minPlaytime)
            {
                isPlaytimeReached = true;
            }

            return isPlaytimeReached;
        }

        public List<ISong> GetMixableTracks(ISong trailingTrack, List<ISong> playlistTracks, List<ISong> mixableTrackCombination)
        {
            return playlistTracks.Where(t =>
                t != trailingTrack
                && (mixableTrackCombination == null
                    || !mixableTrackCombination.Contains(t))
                && t.IsInTempoRange(trailingTrack.TrailingTempo)
                && t.IsInHarmonicKeyRange(trailingTrack.TrailingHarmonicKey)
                && (string.IsNullOrEmpty(IntensityStyle)
                    || t.IsInIntensityRange(trailingTrack.Intensity))
                ).ToList();
        }

        internal List<ISong> GetBestIntensityMatch()
        {
            var intensityMatch = new List<ISong>();
            var bestIntensityCombinationMatches = GetBestIntensityCombinationMatches(MatchingTrackCombinationList);

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

            var randomIndex = GetRandomIndex(MatchingTrackCombinationList);
            randomMatch = MatchingTrackCombinationList[randomIndex];

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
