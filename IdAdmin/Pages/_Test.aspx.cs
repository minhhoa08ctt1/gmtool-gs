using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace IDAdmin.Pages
{
    public partial class _Test : Lib.UI.BasePage
    {
        DataTable dt = new DataTable("Test");
        
        public _Test():base("WebForm1")
        {                        
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            Response.Write(AppManager.GameID);
        }



    }
}
