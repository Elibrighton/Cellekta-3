using SongInterface;
using System;
using System.IO;
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
        public string HarmonicKey { get; set; }
        public string FullName { get; set; }
        public string Playlist { get; set; }
        public string TempoText
        {
            get { return GetTempoText(); }
        }

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
            Playlist = GetPlayList();
            FullName = GetFullName();
            LeadingTempo = GetTempo();
            TrailingTempo = GetTempo(false);
        }

        internal double GetTempo(bool isLeadingTempo = true)
        {
            var tempo = 0.0;

            try
            {
                if (IsTransitionPlaylist())
                {
                    ITag tag = new Tag(FullName);
                    tempo = isLeadingTempo ? tag.LeadingTempo : tag.TrailingTempo;
                }

                if (tempo == 0.0)
                {
                    var tempoNode = _entryNode.SelectSingleNode("TEMPO");

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

        internal string GetPlayList()
        {
            var directory = GetDirectory(_entryNode.SelectSingleNode("LOCATION"));

            return new DirectoryInfo(directory).Name;
        }

        internal string GetFullName()
        {
            var locationNode = _entryNode.SelectSingleNode("LOCATION");
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

        internal bool IsTransitionPlaylist()
        {
            return (FullName.Contains(@"Tranny") || FullName.Contains(@"Transition"));
        }
        
        internal string GetTempoText()
        {
            var tempoText = string.Empty;

            if (LeadingTempo != TrailingTempo)
            {
                tempoText = string.Concat(LeadingTempo, "-", TrailingTempo);
            }
            else
            {
                tempoText = LeadingTempo.ToString();
            }

            return tempoText;
        }
    }
}
