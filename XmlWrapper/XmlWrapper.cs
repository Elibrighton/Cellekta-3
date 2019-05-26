using System;
using System.Xml;
using SongImplementation;
using SongInterface;
using XmlWrapperInterface;

namespace XmlWrapperImplementation
{
    public class XmlWrapper : IXmlWrapper
    {
        private string _xmlPath;
        private XmlDocument _xmlDocument;
        private ISong _song;

        public XmlWrapper(string xmlPath, ISong song)
        {
            _xmlPath = xmlPath;
            _song = song;
            _xmlDocument = new XmlDocument();
        }

        public void Load()
        {
            try
            {
                _xmlDocument.Load(_xmlPath);
            }
            catch (Exception) { }

            foreach (XmlNode node in _xmlDocument.DocumentElement.SelectNodes("/NML/COLLECTION"))
            {
                foreach (XmlNode entryNode in node.SelectNodes("ENTRY"))
                {
                    LoadSong(entryNode);
                }
            }
        }

        internal void LoadSong(XmlNode entryNode)
        {
            _song = new Song();
            //song.Populate(entryNode);
            //song.GetRating();

            //if (song.Artist != "Loopmasters"
            //    && song.Artist != "Native Instruments"
            //    && song.Artist != "Subb-an")
            //{
            //    _music.Add(song);
            //}
        }
    }
}
