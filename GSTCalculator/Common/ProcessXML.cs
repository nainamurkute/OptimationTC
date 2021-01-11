using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace GSTCalculator.Common
{
    public class ProcessXML
    {
        private const string Format = @"<{0}>[\s\S]*?<\/{0}>";

        /// <summary>
        /// get xml string and Deserialize to string
        /// </summary>
        /// <param name="content"></param>
        /// <param name="xmlName"></param>
        /// <returns></returns>
        public object GetXmlData(string content, string xmlName)
        {
            StringBuilder sbFileText = new StringBuilder(content);
            string xmlString = "";
            string xmlPath = string.Format(Format, xmlName);
            Match matchXmlPath = Regex.Match(sbFileText.ToString(), xmlPath);
            if (matchXmlPath.Success)
            {
                xmlString += matchXmlPath.Value.Replace("\r\n", " "); ;

                var xmlNameSerializer = new XmlSerializer(typeof(string), new XmlRootAttribute(xmlName));
                using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlString)))
                {
                    // cost_centre XML string
                    return (string)xmlNameSerializer.Deserialize(memoryStream);
                }
            }
            else
            {
                return "";
            }

        }
    }
}
