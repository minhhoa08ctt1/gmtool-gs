using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IDAdmin.Lib;
using System.Drawing;
using System.Drawing.Imaging;

namespace IDAdmin.Pages
{
    public partial class Captcha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CaptchaImage ci = new CaptchaImage(this.Session[SessionName.CAPTCHA].ToString(), 300, 40, "Verdana");
            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";
            ci.Image.Save(this.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            ci.Dispose();
        }
    }
}
