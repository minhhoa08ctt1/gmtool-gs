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
    public partial class GiveGiftCode : Lib.UI.BasePage
    {
        string _ReturnURL = "";

        public GiveGiftCode()
            : base(Lib.AppFunctions.EVENT_GIVEGIFTCODE)
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
                _ReturnURL = GetParamter("returnURL");
                if (_ReturnURL == "")
                    _ReturnURL = "Index.aspx";

                string username = GetParamter("username");
                if (username.Trim() == "")
                {
                    GoBack();
                }
                else
                {
                    bool succeed = WebDB.GiveGiftCode(AppManager.GameID, "", username, 1);
                    if (succeed)
                    {
                        WebDB.WriteLog(_User.UserName, Request.UserHostAddress, "GiveGiftCode: " + username);
                        GoBack();
                    }
                    else
                    {
                        lblMessage.Text = "Số lượng GiftCode đã hết hoặc không thể cấp GiftCode cho tài khoản này!";
                        linkBack.NavigateUrl = _ReturnURL;
                    }
                }
            }
        }

        private void GoBack()
        {
            if (_ReturnURL == "")
            {
                Response.Redirect("Index.aspx", false);
            }
            else
            {
                Response.Redirect(_ReturnURL, false);
            }
        }
    }
}
