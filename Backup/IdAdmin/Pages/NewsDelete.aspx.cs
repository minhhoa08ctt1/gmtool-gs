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
    public partial class NewsDelete : Lib.UI.BasePage
    {
        string _returnURL;

        public NewsDelete()
            : base(Lib.AppFunctions.NEWS_DELETE)
        { 
        }

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
                _returnURL = Server.UrlDecode(GetParamter("returnURL"));
                this.panelMessage.Visible = false;
                long id = Converter.ToLong(GetParamter("id"));
                WebDB.News_Delete(id, _User.UserName);
                WebDB.WriteLog(_User.UserName, Request.UserHostAddress, "News_Delete: " + id.ToString());
                Response.Redirect(_returnURL, false);                
            }
        }
    }
}
