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
        public string LeadingHarmonicKey { get; set; }
        public string TrailingHarmonicKey { get; set; }
        public string FullName { get; set; }
        public string Playlist { get; set; }
        public string TempoText { get; private set; }
        public int PlayTime { get; set; }
        public int Intensity { get; set; }

        private IXmlWrapper _xmlWrapper;
        private XmlNode _entryNode;

        public Song()
        {

        }

        public Song(IXmlWrapper xmlWrapper, XmlNode entryNode)
        {
            _xmlWrapper = xmlWrapper;
            _entryNode = entryNode;
        }

        public void Load()
        {
            Artist = _xmlWrapper.GetAttribute(_entryNode.Attributes["ARTIST"]);
            Title = _xmlWrapper.GetAttribute(_entryNode.Attributes["TITLE"]);
            var locationNode = _entryNode.SelectSingleNode("LOCATION");
            Playlist = GetPlayList(locationNode);
            FullName = GetFullName(locationNode);
            var infoNode = _entryNode.SelectSingleNode("INFO");
            PlayTime = GetPlayTime(_xmlWrapper.GetAttribute(infoNode.Attributes["PLAYTIME"]));
            LeadingTempo = GetTempo(_entryNode.SelectSingleNode("TEMPO"), FullName);
            TrailingTempo = GetTempo(_entryNode.SelectSingleNode("TEMPO"), FullName, false);
            TempoText = GetTempoText(LeadingTempo, TrailingTempo);
            var comment = _xmlWrapper.GetAttribute(infoNode.Attributes["COMMENT"]);
            Intensity = GetIntensity(comment);
            LeadingHarmonicKey = GetLeadingHarmonicKey(comment);
            TrailingHarmonicKey = GetTrailingHarmonicKey(comment);
        }

        internal double GetTempo(XmlNode tempoNode, string fullName, bool isLeadingTempo = true)
        {
            var tempo = 0.0;

            try
            {
                if (IsTransitionPlaylist(fullName))
                {
                    ITag tag = new Tag(fullName);
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

            return tempo;
        }

        internal string GetPlayList(XmlNode locationNode)
        {
            if (locationNode == null) throw new ArgumentNullException("locationNode is null");

            var directory = GetDirectory(locationNode);

            return new DirectoryInfo(directory).Name;
        }

        internal string GetFullName(XmlNode locationNode)
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

        internal bool IsTransitionPlaylist(string fullName)
        {
            // should this explicitly check a playlist path?
            return (fullName.Contains(@"Tranny") || fullName.Contains(@"Transition"));
        }

        internal string GetTempoText(double leadingTempo, double trailingTempo)
        {
            if (leadingTempo == 0.0) throw new ArgumentOutOfRangeException("playTimeAttribute is 0.0");

            var tempoText = string.Empty;

            if (leadingTempo != trailingTempo)
            {
                tempoText = string.Concat(leadingTempo, "-", trailingTempo);
            }
            else
            {
                tempoText = leadingTempo.ToString();
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

        internal string GetTrailingHarmonicKey(string comment)
        {
            var trailingHarmonicKey = string.Empty;

            if (IsRegexMatch(comment, @"^\d\d?[AB]/\d\d?[AB]\s-\sEnergy\s\d"))
            {
                trailingHarmonicKey = GetRegexMatchValue(comment, @"/\d\d?[AB]").Replace("/", "");
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
    }
}
