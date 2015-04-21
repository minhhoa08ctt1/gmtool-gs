using IDAdmin.Lib;
using IDAdmin.Lib.DataLayer;
using IDAdmin.Lib.UI;
using IDAdmin.Lib.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IDAdmin.Pages
{
    public partial class EditAccount : BasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataRow data = WebDB.getUserInfo(this._User.ID);
                //txtPassword.Text = Converter.ToString(data["Password"]);
                //txtReTypePassword.Text = Converter.ToString(data["Password"]);
                txtEmail.Text = Converter.ToString(data["Email"]);
            }
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            labelCheckMessage.Text="";
            messageLabel.Text="";
            if(checkAccept.Checked==false)
            {
                labelCheckMessage.Text = "[Chưa xác nhận]";
                return;
            }
            if (string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtReTypePassword.Text) || string.IsNullOrEmpty(txtEmail.Text))
            {
                messageLabel.Text = "Nhập thiếu";
                return;
            }

            if (txtPassword.Text.CompareTo(txtReTypePassword.Text)!=0)
            {
                messageLabel.Text = "Nhập lại mật khẩu không giống với mật khẩu";
                return;
            }
            int affectedRow=WebDB.updateUserInfo(txtPassword.Text, txtEmail.Text, this._User.ID);
            if (affectedRow > 0)
            {
                messageLabel.Text = "Chỉnh sửa thành công";
            }
            else
            {
                messageLabel.Text = "Chỉnh sửa thất bại";
            }
        }
    }
}