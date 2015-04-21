using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IDAdmin.Lib.DataLayer;
using IDAdmin.Lib.UI;
using IDAdmin.Lib.Utils;

namespace IDAdmin.Pages
{
    public partial class Index : Lib.UI.BasePage
    {
        public Index()
            : base("Index")
        { }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsLogin())
            {
                RedirectToLogOn();
            }
            else
            {
                if (!Page.IsPostBack)
                {
                }
            }
        }
    }
}
