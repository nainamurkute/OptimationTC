using System.Xml.Serialization;

namespace GSTCalculator.Model
{
    [XmlRoot(ElementName = "calculated")]
    public class Calculated
    {
        [XmlElement("GST")]
        public decimal GST { get; set; }
        [XmlElement("total_excluding_GST")]
        public decimal Total_Excluding_GST { get; set; }
    }
}
