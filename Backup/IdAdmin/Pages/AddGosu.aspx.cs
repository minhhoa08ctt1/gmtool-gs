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
    public partial class AddGosu : Lib.UI.BasePage
    {
        public AddGosu()
            : base(Lib.AppFunctions.ADD_GOSU)
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
                if (!Page.IsPostBack)
                {
                    txtCaptcha.Text = "";
                    Session[Lib.SessionName.CAPTCHA] = Lib.CaptchaImage.GetCaptchaString();
                }

                buttonAccept.Click += new EventHandler(buttonAccept_Click);
                buttonCancel.Click += new EventHandler(buttonCancel_Click);
                buttonRechargeList.Click += new EventHandler(buttonRechargeList_Click);
            }
        }

        protected void buttonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx", false);
        }

        protected void buttonAccept_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCaptcha.Text != Session[Lib.SessionName.CAPTCHA].ToString())
                {
                    labelMessage.ForeColor = System.Drawing.Color.Red;
                    labelMessage.Text = "Mã bảo vệ không đúng!";
                    txtCaptcha.Text = "";
                    Session[Lib.SessionName.CAPTCHA] = Lib.CaptchaImage.GetCaptchaString();
                }
                else
                {
                    string username = txtAccount.Text.Trim();
                    if (username == "")
                    {
                        labelMessage.Text = "Chưa nhập tên tài khoản nạp GOSU";
                        Session[Lib.SessionName.CAPTCHA] = Lib.CaptchaImage.GetCaptchaString();
                        return;
                    }
                    if (!WebDB.User_Exists(username))
                    {
                        labelMessage.Text = "Tài khoản nạp GOSU không tồn tại";
                        Session[Lib.SessionName.CAPTCHA] = Lib.CaptchaImage.GetCaptchaString();
                        return;
                    }

                    string rechargeUser = _User.UserName;
                    int amount = Converter.ToInt(txtAmount.Text, 0);
                    if (amount < 1000)
                    {
                        labelMessage.Text = "Số tiền không hợp lệ";
                        Session[Lib.SessionName.CAPTCHA] = Lib.CaptchaImage.GetCaptchaString();
                        return;
                    }
                    string ip = Request.UserHostAddress;
                    string msg = txtMsg.Text.Trim();
                    if (msg == "")
                    {
                        labelMessage.Text = "Phải cho biết lý do nạp tiền";
                        Session[Lib.SessionName.CAPTCHA] = Lib.CaptchaImage.GetCaptchaString();
                        return;
                    }

                    string type = cmbType.SelectedValue;
                    if (type == "")
                    {
                        labelMessage.Text = "Phải chọn hình thức nạp tiền";
                        Session[Lib.SessionName.CAPTCHA] = Lib.CaptchaImage.GetCaptchaString();
                        return;
                    }

                    bool succeed = WebDB.Wallet_AddDirectly(username, rechargeUser, amount, ip, msg, type);
                    if (succeed)
                    {
                        Response.Redirect("WalletRechargeHistory.aspx?account=" + username);
                        return;
                    }
                    else
                    {
                        labelMessage.Text = "Nạp tiền vào ví GOSU lỗi. Không thực hiện được";
                        Session[Lib.SessionName.CAPTCHA] = Lib.CaptchaImage.GetCaptchaString();
                    }
                }
            }
            catch (Exception ex)
            {
                labelMessage.Text = ex.Message + ": " + ex.StackTrace;
                Session[Lib.SessionName.CAPTCHA] = Lib.CaptchaImage.GetCaptchaString();
            }
        }

        protected void buttonRechargeList_Click(object sender, EventArgs e)
        {
            try
            {
                string ip = Request.UserHostAddress;
                string[] lines = System.IO.File.ReadAllLines(Server.MapPath(@"~/gosu.txt"));
                foreach (string line in lines)
                {
                    string[] arr = line.Split('|');
                    if (arr.Length > 0)
                    {
                        WebDB.Wallet_AddDirectly(arr[0], arr[1], Converter.ToInt(arr[2]), ip, arr[3], arr[4]);
                    }
                }
                labelMessage.Text = "Nạp GOSU thành công";
            }
            catch (Exception ex)
            {
                labelMessage.Text = ex.Message + ": " + ex.StackTrace;
            }
        }
    }
}
