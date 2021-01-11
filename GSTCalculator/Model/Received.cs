using System.Xml.Serialization;

namespace GSTCalculator.Model
{
    [XmlRoot(ElementName = "recieved")]
    public class Received
    {
        [XmlElement("expense")]
        public Expense Expense { get; set; }
        [XmlElement("vendor")]
        public string Vendor { get; set; }
        [XmlElement("description")]
        public string Description { get; set; }
        [XmlElement("date")]
        public string Date { get; set; }
    }
}
