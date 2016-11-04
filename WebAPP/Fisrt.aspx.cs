using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAPP
{
    public partial class Fisrt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sSN = string.Format("###,##0.##", 4685.68);

            // we want to see if baseValue is a power of 2
            uint iNum = 14;
            // the result goes here 
            bool f;                 
            // Note that 0 is incorrectly considered a power of 2 here. To remedy this, use:
            f = iNum > 0 && (iNum & (iNum - 1)) != 0;

            // Compute the next highest power of 2 of 32-bit baseValue
            iNum = 14;
            iNum--;
            iNum |= iNum >> 1;
            iNum |= iNum >> 2;
            iNum |= iNum >> 4;
            iNum |= iNum >> 8;
            iNum |= iNum >> 16;
            iNum++;

            RequestNotification oRNF = 0;
            oRNF = (RequestNotification)Enum.Parse(typeof(RequestNotification), 4.ToString());
            Enum.TryParse<RequestNotification>(8.ToString(), out oRNF);

            int iSCR = String.Compare("N", "");

            //NumberFormatInfo.InvariantInfo.GetFormat()
            // NumberFormatInfo.CurrentInfo.NumberDecimalDigits = 29;

            string sRS = string.Format("{0:D6C3}", 52.66);

            string sImgBinaryCodes = Convert.ToBase64String(File.ReadAllBytes(Server.MapPath("WCF_C2.PNG")), Base64FormattingOptions.None);

            string sMSSQLConnStr = ConfigurationManager.ConnectionStrings["mssql-local"].ConnectionString;

            SqlCommand oMyCmd = (new SqlConnection(sMSSQLConnStr)).CreateCommand();
            oMyCmd.CommandText = @"";

            DataSet oMyDs = new DataSet();
            SqlDataAdapter oMyDa = new SqlDataAdapter(oMyCmd);
            oMyDa.Fill(oMyDs);
        }
    }
}