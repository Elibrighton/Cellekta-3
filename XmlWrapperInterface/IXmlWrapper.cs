using System.Xml;

namespace XmlWrapperInterface
{
    public interface IXmlWrapper
    {
        XmlDocument XmlDocument { get; set; }
        string XmlPath { get; set; }

        void Load();
        string GetAttribute(XmlAttribute xmlAttributes);
    }
}
