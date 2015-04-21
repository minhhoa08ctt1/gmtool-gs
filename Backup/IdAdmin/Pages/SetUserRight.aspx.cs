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
    public partial class SetUserRight : Lib.UI.BasePage
    {
        public SetUserRight()
            : base(Lib.AppFunctions.MANAGEUSERRIGHTS)
        { }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsLogin())
            {
                RedirectToLogOn();
            }
            else if (!CheckRight())
            {
                RedirectToDeniedMessage();
            }
            else
            {
                string functionName = GetParamter("f").Trim();
                string username = GetParamter("username").Trim();
                bool allow = Converter.ToBoolean(GetParamter("allow"));
                string returnURL = GetParamter("returnURL");

                if (functionName == "" || username == "")
                {
                    Response.Redirect(returnURL, false);
                }
                else
                {
                    WebDB.Function_SetUserRight(functionName, username, allow);
                    Response.Redirect(returnURL);
                }
            }
        }
    }
}
