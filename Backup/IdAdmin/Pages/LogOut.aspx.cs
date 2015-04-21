using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IDAdmin.Pages
{
    public partial class LogOut : Lib.UI.BasePage
    {
        public LogOut()
            : base("LogOut")
        { }

        protected override void Page_Load(object sender, EventArgs e)
        {
            _User.LogOut();
            RedirectToLogOn();
        }
    }
}
