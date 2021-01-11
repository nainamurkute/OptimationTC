using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using GSTCalculator.Domain;
using GSTCalculator.Model;
using GSTCalculator.Common;
using Microsoft.AspNetCore.Mvc;

namespace GSTCalculator.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GSTCalculatorController : ControllerBase
    {
        private const string openTags = @"(<([^/])([^>]+)*>)";
        private const string closeTags = @"(</([^>]+)*>)";
        [HttpGet]
        public IActionResult Get(string content)
        {
            ProcessXML processXML = new ProcessXML();

            MatchCollection matchOpenTags = Regex.Matches(content, openTags);

            MatchCollection matchCloseTags = Regex.Matches(content, closeTags);
            bool flagClosingTag, flagTotal, flagCostCentre;
            InputValidation inputValidation = new InputValidation();
            inputValidation.matchAndValidate(matchOpenTags, matchCloseTags, out flagClosingTag, out flagTotal, out flagCostCentre);

            if (flagClosingTag)
            {
                return BadRequest("rejected");
            }

            if (!flagTotal)
            {
                return BadRequest("rejected");
            }

            Expense expense = new Expense();
            Response response = new Response();
            Received received = new Received();
            Calculated calculated = new Calculated();
            received.Expense = expense;
            response.Received = received;
            response.Calculated = calculated;
            response.Received.Expense.Cost_Centre = (string)processXML.GetXmlData(content, "cost_centre");

            string totalValueString = (string)processXML.GetXmlData(content, "total");

            decimal totalNumber;
            
            response.Received.Expense.Total = 0;
            if (decimal.TryParse(totalValueString, out totalNumber))
            {
                response.Received.Expense.Total = totalNumber;
            }
            else
            {
                response.Received.Expense.Total = 0;
            }

            GST gst = new GST();
            gst.calculateGST(response);

            response.Received.Expense.Payment_Method = (string) processXML.GetXmlData(content, "payment_method");

            response.Received.Vendor = (string)processXML.GetXmlData(content, "vendor");

            response.Received.Description = (string)processXML.GetXmlData(content, "description");

            response.Received.Date = (string)processXML.GetXmlData(content, "date");

            if (!flagCostCentre)
            {
                received.Expense.Cost_Centre = "UNKNOWN";
            }

            var xmlResponseSerializer = new XmlSerializer(typeof(Response));
            string responseString = "";
            using (var stringWriter = new StringWriter())
            {
                xmlResponseSerializer.Serialize(stringWriter, response);
                responseString = stringWriter.ToString();
            }
            return Ok(responseString);
        }
    }
}