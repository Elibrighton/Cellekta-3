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
        public string XmlPath { get; set; }

        public XmlWrapper()
        {
            XmlDocument = new XmlDocument();
        }

        public void Load()
        {
            try
            {
                XmlDocument.Load(XmlPath);
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
