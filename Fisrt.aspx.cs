using System;
using System.Collections.Generic;
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


           string sImgBinaryCodes = Convert.ToBase64String(File.ReadAllBytes(Server.MapPath("WCF_C2.PNG")), Base64FormattingOptions.None);

        }
    }
}