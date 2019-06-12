using MixableRangeInterface;
using SongInterface;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using TagImplementation;
using TagInterface;
using XmlWrapperInterface;

namespace SongImplementation
{
    public class Song : ISong
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public double LeadingTempo { get; set; }
        public double TrailingTempo { get; set; }
        public string TempoText { get; private set; }
        public string LeadingHarmonicKey { get; set; }
        public string TrailingHarmonicKey { get; set; }
        public string HarmonicKeyText { get; private set; }
        public string Path { get; set; }
        public string Playlist { get; set; }
        public int PlayTime { get; set; }
        public int Intensity { get; set; }
        public XmlNode EntryNode { get; set; }
        public bool IsCharting { get; set; }
        public string IsChartingText { get; private set; }
        public string FullNameText { get; private set; }
        public ITempoRange MixableRange { get; set; }
        public int RoundedTrailingTempo { get; private set; }

        private IXmlWrapper _xmlWrapper;

        public Song(IXmlWrapper xmlWrapper, ITempoRange mixableRange)
        {
            _xmlWrapper = xmlWrapper;
            MixableRange = mixableRange;
        }

        public void Load()
        {
            Artist = _xmlWrapper.GetAttribute(EntryNode.Attributes["ARTIST"]);
            Title = _xmlWrapper.GetAttribute(EntryNode.Attributes["TITLE"]);
            var locationNode = EntryNode.SelectSingleNode("LOCATION");
            Playlist = GetPlayList(locationNode);
            Path = GetPath(locationNode);
            var infoNode = EntryNode.SelectSingleNode("INFO");
            PlayTime = GetPlayTime(_xmlWrapper.GetAttribute(infoNode.Attributes["PLAYTIME"]));
            LeadingTempo = GetTempo(EntryNode.SelectSingleNode("TEMPO"), Path);
            TrailingTempo = GetTempo(EntryNode.SelectSingleNode("TEMPO"), Path, false);
            TempoText = GetTempoText(LeadingTempo, TrailingTempo);
            RoundedTrailingTempo = GetRoundedTrailingTempo(TrailingTempo);
            var comment = _xmlWrapper.GetAttribute(infoNode.Attributes["COMMENT"]);
            Intensity = GetIntensity(comment);
            LeadingHarmonicKey = GetLeadingHarmonicKey(comment);
            TrailingHarmonicKey = GetTrailingHarmonicKey(comment, LeadingHarmonicKey);
            HarmonicKeyText = GetHarmonicKeyText(LeadingHarmonicKey, TrailingHarmonicKey);
            IsCharting = GetIsCharting();
            IsChartingText = GetIsChartingText(IsCharting);
            FullNameText = GetFullNameText(Artist, Title, TempoText, HarmonicKeyText, Intensity, Playlist);
            MixableRange.Load(TrailingTempo, 3); // menu item control the range value to be added later
        }

        internal double GetTempo(XmlNode tempoNode, string path, bool isLeadingTempo = true)
        {
            var tempo = 0.0;

            try
            {
                if (IsTransitionPlaylist(path))
                {
                    ITag tag = new Tag(path);
                    tempo = isLeadingTempo ? tag.LeadingTempo : tag.TrailingTempo;
                }

                if (tempo == 0.0)
                {
                    if (tempoNode != null)
                    {
                        decimal.TryParse(_xmlWrapper.GetAttribute(tempoNode.Attributes["BPM"]), out decimal parsedTempo);
                        tempo = Convert.ToDouble(parsedTempo);
                    }
                }
            }
            catch (ArgumentNullException)
            {
                tempo = 0.0;
            }
            catch (Exception)
            {
                throw;
            }

            // perform calculation to ensure tempo is in range of 65 to 165 BPM
            if (tempo > 165.0)
            {
                tempo /= 2;
            }
            else if (tempo < 65.0)
            {
                tempo *= 2;
            }

            return tempo;
        }

        internal string GetPlayList(XmlNode locationNode)
        {
            if (locationNode == null) throw new ArgumentNullException("locationNode is null");

            var directory = GetDirectory(locationNode);

            return new DirectoryInfo(directory).Name;
        }

        internal string GetPath(XmlNode locationNode)
        {
            if (locationNode == null) throw new ArgumentNullException("locationNode is null");

            var directory = GetDirectory(locationNode);
            var file = _xmlWrapper.GetAttribute(locationNode.Attributes["FILE"]);
            var volume = _xmlWrapper.GetAttribute(locationNode.Attributes["VOLUME"]);

            return string.Concat(volume, directory, file);
        }

        internal string GetDirectory(XmlNode locationNode)
        {
            if (locationNode == null) throw new ArgumentNullException("locationNode is null");

            return _xmlWrapper.GetAttribute(locationNode.Attributes["DIR"]).Replace("/:", "\\");
        }

        internal bool IsTransitionPlaylist(string path)
        {
            // should this explicitly check a playlist path?
            return (path.Contains(@"Tranny") || path.Contains(@"Transition"));
        }

        internal string GetTempoText(double leadingTempo, double trailingTempo)
        {
            if (leadingTempo == 0.0) throw new ArgumentOutOfRangeException("leadingTempo is 0.0");
            if (trailingTempo == 0.0) throw new ArgumentOutOfRangeException("trailingTempo is 0.0");

            leadingTempo = Math.Round(leadingTempo, 3);
            trailingTempo = Math.Round(trailingTempo, 3);

            var tempoText = string.Empty;

            if (leadingTempo != trailingTempo)
            {
                tempoText = string.Concat(leadingTempo.ToString("0.000"), "/", trailingTempo.ToString("0.000"));
            }
            else
            {
                tempoText = leadingTempo.ToString("0.000");
            }

            return tempoText;
        }

        internal int GetPlayTime(string playTimeAttribute)
        {
            if (playTimeAttribute == null) throw new ArgumentNullException("playTimeAttribute is null");

            int.TryParse(playTimeAttribute, out int playTime);

            return playTime;
        }

        internal int GetIntensity(string comment)
        {
            if (comment == null) throw new ArgumentNullException("comment is null");

            var intensity = 0;

            if (IsRegexMatch(comment, @"^\d\d?[AB](/\d\d?[AB])?\s-\sEnergy\s\d"))
            {
                intensity = Convert.ToInt32(GetRegexMatchValue(comment, @"\d$"));
            }

            return intensity;
        }

        internal string GetLeadingHarmonicKey(string comment)
        {
            if (comment == null) throw new ArgumentNullException("comment is null");

            var leadingHarmonicKey = string.Empty;

            if (IsRegexMatch(comment, @"^\d\d?[AB](/\d\d?[AB])?\s-\sEnergy\s\d"))
            {
                leadingHarmonicKey = GetRegexMatchValue(comment, @"^\d\d?[AB]");
            }

            return leadingHarmonicKey;
        }

        internal string GetTrailingHarmonicKey(string comment, string leadingHarmonicKey)
        {
            var trailingHarmonicKey = string.Empty;

            if (IsRegexMatch(comment, @"^\d\d?[AB]/\d\d?[AB]\s-\sEnergy\s\d"))
            {
                trailingHarmonicKey = GetRegexMatchValue(comment, @"/\d\d?[AB]").Replace("/", "");
            }
            else
            {
                trailingHarmonicKey = leadingHarmonicKey;
            }

            return trailingHarmonicKey;
        }

        // refactor into RegexWrapper project and pass in _regexWrapper interface to ctor
        internal bool IsRegexMatch(string input, string pattern)
        {
            if (input == null) throw new ArgumentNullException("input is null");
            if (pattern == null) throw new ArgumentNullException("pattern is null");

            var isRegexMatch = false;

            if (!string.IsNullOrEmpty(input))
            {
                isRegexMatch = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
            }

            return isRegexMatch;
        }

        // refactor into RegexWrapper project and pass in _regexWrapper interface to ctor
        internal string GetRegexMatchValue(string input, string pattern)
        {
            if (input == null) throw new ArgumentNullException("input is null");
            if (pattern == null) throw new ArgumentNullException("pattern is null");

            var matchValue = string.Empty;
            var regex = new Regex(pattern);
            var match = regex.Match(input);

            if (match.Success)
            {
                matchValue = match.Value;
            }

            return matchValue;
        }

        internal string GetHarmonicKeyText(string leadingHarmonicKey, string trailingHarmonicKey)
        {
            if (leadingHarmonicKey == null) throw new ArgumentNullException("leadingHarmonicKey is null");
            if (trailingHarmonicKey == null) throw new ArgumentNullException("trailingHarmonicKey is null");

            var harmonicKeyText = string.Empty;

            if (leadingHarmonicKey != trailingHarmonicKey)
            {
                harmonicKeyText = string.Concat(leadingHarmonicKey.ToString(), "/", trailingHarmonicKey.ToString());
            }
            else
            {
                harmonicKeyText = leadingHarmonicKey.ToString();
            }

            return harmonicKeyText;
        }

        internal bool GetIsCharting()
        {
            // to be implemented from GetRating();
            return false;
        }

        internal string GetIsChartingText(bool isCharting)
        {
            var isChartingText = string.Empty;

            if (isCharting)
            {
                isChartingText = "Yes";
            }

            return isChartingText;
        }

        internal string GetFullNameText(string artist, string title, string tempoText, string harmonicKeyText, int intensity, string playlist)
        {
            return string.Concat(artist, !string.IsNullOrEmpty(artist) && !string.IsNullOrEmpty(title) ? " - " : "", title, " - ", tempoText, " - ", harmonicKeyText, " - ", intensity, " - ", playlist);
        }

        internal int GetRoundedTrailingTempo(double trailingTempo)
        {
            return Convert.ToInt32(Math.Round(trailingTempo));
        }
    }
}
