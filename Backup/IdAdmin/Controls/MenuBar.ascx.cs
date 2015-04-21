using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IDAdmin.Controls
{
    public partial class MenuBar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Lib.WebUser _user = new IDAdmin.Lib.WebUser();
            _user.GetInfo();
            if (!_user.IsAuthenticated)
            {

            }
            else
            {
                linkLogOut.Text = string.Format("Thoát [{0}]", _user.UserName);
                if (AppManager.GameID != "")
                {
                    labelHeader.Text = AppManager.GameName;
                }
                else
                {
                    labelHeader.Text = "GOSU Administrative";
                }
            }
        }
    }
}