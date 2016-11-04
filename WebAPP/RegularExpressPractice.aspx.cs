using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAPP
{
    public partial class RegularExpressPractice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sLen11Str = "abc";
            sLen11Str = sLen11Str.PadLeft(11, 'X');
            
            string sRS = "ABCDSWE_#mmDDyyy_hhmmss##";
            int iFisrtForzenPos = 0;
            int iLastForzenPos = 0;
            iFisrtForzenPos = sRS.IndexOf("#") + 1;
            iLastForzenPos = sRS.LastIndexOf("#", iFisrtForzenPos) - 1;

            StringBuilder sb = new StringBuilder();
            sb.Append(InFunc());

            string sDataValue =
@"<Field>
    <Category>LS001</Category></Field><Field>ABC<><Category>LS009</Category>>>*.</Field><Field>ASFSAF8293742938*&(&(*238&(<Root></Root></Field><Field>ASFSAF8<Field>293742938*&(&(*238&(<</Field>";

            // Verify
            Regex oRegexVerification = new Regex(@"(<Field>((?(?=<Field>)\0|(.|\r|\n))*)</Field>)", RegexOptions.IgnoreCase);
            MatchCollection oMCVerification = oRegexVerification.Matches(sDataValue, 0);

            Regex oRegexField = new Regex(@"(<Field>((?(?=<Field>)\0|(.|\r|\n))*)</Field>)", RegexOptions.IgnoreCase);
            MatchCollection oMCField = oRegexField.Matches(sDataValue, 0);

            List<string[]> oResult = new List<string[]>();
            Regex oRegexCategory = new Regex(@"<Category>((?(?=<Category>)\0|(.|\r|\n))*)</Category>", RegexOptions.IgnoreCase);
            Match oMCategory = null;

            // the start position in theory.
            int iSStartPosition = 0;
            foreach (Match oMField in oMCField)
            {
                if (iSStartPosition < oMField.Index && !string.IsNullOrWhiteSpace(sDataValue.Substring(iSStartPosition, oMField.Index - iSStartPosition)))
                {
                    throw new Exception("");
                }
                oMCategory = oRegexCategory.Match(oMField.Groups[1].Value);
                if (oMCategory.Success)
                {
                    oResult.Add(new string[] { string.Empty, oMCategory.Groups[1].Value });
                }
                else
                {
                    oResult.Add(new string[] { oMField.Groups[1].Value, string.Empty });
                }

                // next start position.
                iSStartPosition = oMField.Index + oMField.Length;
            }
        }

        private string InFunc()
        {
            return null;
        }
    }
}