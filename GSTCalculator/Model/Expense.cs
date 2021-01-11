using System.Xml.Serialization;

namespace GSTCalculator.Model
{
    [XmlRoot(ElementName = "expense")]
    public class Expense
    {
        [XmlElement("cost_centre")]
        public string Cost_Centre { get; set; }
        [XmlElement("total")]
        public decimal? Total { get; set; }
        [XmlElement("payment_method")]
        public string Payment_Method { get; set; }
    }
}
