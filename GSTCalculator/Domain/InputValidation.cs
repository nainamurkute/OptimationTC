using System.Text.RegularExpressions;

namespace GSTCalculator.Domain
{
    public class InputValidation
    {
        /// <summary>
        /// It will validate XML and Tags
        /// </summary>
        /// <param name="matchOpenTags"></param>
        /// <param name="matchCloseTags"></param>
        /// <param name="flagClosingTag"></param>
        /// <param name="flagTotal"></param>
        /// <param name="flagCostCentre"></param>
        public void matchAndValidate(MatchCollection matchOpenTags, MatchCollection matchCloseTags, out bool flagClosingTag, out bool flagTotal, out bool flagCostCentre)
        {
            flagClosingTag = false;
            flagTotal = false;
            flagCostCentre = false;
            foreach (Match openTagsItem in matchOpenTags)
            {
                //skipping mail ids
                if (openTagsItem.ToString().Contains("@"))
                {
                    break;
                }
                if (openTagsItem.ToString() == "<total>")
                {
                    flagTotal = true;
                }

                if (openTagsItem.ToString() == "<cost_centre>")
                {
                    flagCostCentre = true;
                }
                bool findClosingTag = false;
                foreach (Match closeTagsItem in matchCloseTags)
                {
                    closeTagsItem.ToString().Replace("/", "");

                    if (openTagsItem.ToString() == closeTagsItem.ToString().Replace("/", ""))
                    {
                        findClosingTag = true;
                        break;
                    }
                }

                if (!findClosingTag)
                {
                    flagClosingTag = true;
                    break;
                }
            }
        }
    }
}
