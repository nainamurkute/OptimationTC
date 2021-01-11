using System.Xml.Serialization;

namespace GSTCalculator.Model
{
    [XmlRoot("response")]
    public class Response
    {
        [XmlElement("received")]
        public Received Received { get; set; }

        [XmlElement("calculated")]
        public Calculated Calculated { get; set; }
    }
}
