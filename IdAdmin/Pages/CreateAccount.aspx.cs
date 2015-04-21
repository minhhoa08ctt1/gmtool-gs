using IDAdmin.Lib.DataLayer;
using IDAdmin.Lib.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IDAdmin.Pages
{
    public partial class CreateAccount : BasePage
    {
        public CreateAccount()
            : base(Lib.AppFunctions.CREATEACCOUNT)
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
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            messageLabel.Text = "";
            labelCheckMessage.Text = "";
            if (checkAccept.Checked==false)
            {
                labelCheckMessage.Text = "[Chưa xác nhận]";
                return;
            }
            int unique=WebDB.checkUniqueUserName(txtUserName.Text);
            if (unique == 1)
            {
                messageLabel.Text = "Username đã tồn tại";
                return;
            }
            if(string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtReTypePassword.Text))
            {
                messageLabel.Text = "Nhập thiếu";
            }

            if (txtPassword.Text.CompareTo(txtReTypePassword.Text) != 0)
            {
                messageLabel.Text = "Nhập lại mật khẩu không giống với mật khẩu";
                return;
            }

            if(IsValidEmailId(txtEmail.Text)==false)
            {
                messageLabel.Text = "Email không hợp lệ";
                return;
            }

           int affectedRow= WebDB.createAccount(txtUserName.Text,txtName.Text,txtPassword.Text,txtEmail.Text);
           if(affectedRow>0)
           {
               messageLabel.Text = "Tạo tài khoản thành công";
           }
        }

        private bool IsValidEmailId(string InputEmail)
        {
            //Regex To validate Email Address
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(InputEmail);
            if (match.Success)
                return true;
            else
                return false;
        }
    }
}