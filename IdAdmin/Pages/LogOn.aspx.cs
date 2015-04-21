using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace IDAdmin.Pages
{
    public partial class LogOn : Lib.UI.BasePage
    {
        public LogOn()
            : base("LogOn")
        { }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (_User.IsAuthenticated)
            {
                Response.Redirect("~/Pages/Index.aspx", false);
            }
            else
            {
                buttonLogOn.Click += new EventHandler(buttonLogOn_Click);
            }
        }

        protected void buttonLogOn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    string userName = txtUserName.Text;
                    string password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "MD5");

                    DataRow drUser = Lib.DataLayer.WebDB.User_Validate(userName, password, Request.UserHostAddress);

                    if (drUser != null)
                    {
                        Lib.WebUser user = new Lib.WebUser();
                        user.LogIn(drUser[Lib.Meta.USER_ID].ToString(),
                                   userName,
                                   Lib.Utils.Converter.ToInt(drUser[Lib.Meta.USER_LEVEL]));

                        Lib.DataLayer.WebDB.WriteLog(userName, Request.UserHostAddress, "Login");
                        Response.Redirect("~/Pages/Index.aspx", false);
                    }
                    else
                    {
                        Lib.DataLayer.WebDB.WriteLog(userName, Request.UserHostAddress, "Login Failed");
                        labelMessage.Text = "Đăng nhập thất bại!";
                    }
                }
            }
            catch(Exception ex)
            {
                labelMessage.Text = "Đăng nhập thất bại";
            }
        }


    }
}
