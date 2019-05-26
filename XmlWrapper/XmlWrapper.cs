using System;
using System.Xml;
using SongImplementation;
using SongInterface;
using XmlWrapperInterface;

namespace XmlWrapperImplementation
{
    public class XmlWrapper : IXmlWrapper
    {
        public XmlDocument XmlDocument { get; set; }

        private string _xmlPath;

        public XmlWrapper()
        {
            XmlDocument = new XmlDocument();
        }

        public XmlWrapper(string xmlPath)
        {
            _xmlPath = xmlPath;
            XmlDocument = new XmlDocument();
        }

        public void Load()
        {
            try
            {
                XmlDocument.Load(_xmlPath);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetAttribute(XmlAttribute xmlAttributes)
        {
            var attribute = string.Empty;

            if (xmlAttributes != null)
            {
                if (xmlAttributes.Value != null)
                {
                    attribute = xmlAttributes.Value;
                }
            }

            return attribute;
        }
    }
}
