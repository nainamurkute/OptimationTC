using GSTCalculator.Model;
using System;

namespace GSTCalculator.Domain
{
    public class GST
    {
        /// <summary>
        /// Calculate GST where GST is 18 percent
        /// </summary>
        /// <param name="response"></param>
        public void calculateGST(Response response)
        {
            if (response.Received.Expense.Total > 0 && response.Received.Expense.Total.HasValue)
            {
                decimal Total = response.Received.Expense.Total.Value;
                response.Calculated.GST = Math.Round((Total / 118) * 18, 2);
                response.Calculated.Total_Excluding_GST = Math.Round(Total - response.Calculated.GST, 2); ;
            }
        }
    }
}
