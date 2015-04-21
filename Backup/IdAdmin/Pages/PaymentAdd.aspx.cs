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
    public partial class PaymentAdd : Lib.UI.BasePage
    {
        public PaymentAdd()
            : base(Lib.AppFunctions.PAYMENT_ADD)
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
                if (!Page.IsPostBack)
                {
                    this.cmbServer.DataValueField = Lib.Meta.SERVER_NAME;
                    this.cmbServer.DataTextField = Lib.Meta.SERVER_FULLNAME;
                    this.cmbServer.DataSource = WebDB.Server_Select(false);
                    this.cmbServer.DataBind();

                    txtCaptcha.Text = "";
                    Session[Lib.SessionName.CAPTCHA] = Lib.CaptchaImage.GetCaptchaString();
                }
                this.buttonAccept.Click += new EventHandler(buttonAccept_Click);
                this.buttonCancel.Click += new EventHandler(buttonCancel_Click);                
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
                if (!Page.IsValid)
                    return;

                if (txtCaptcha.Text != Session[Lib.SessionName.CAPTCHA].ToString())
                {
                    labelMessage.ForeColor = System.Drawing.Color.Red;
                    labelMessage.Text = "Mã bảo vệ không đúng!";
                    txtCaptcha.Text = "";
                    Session[Lib.SessionName.CAPTCHA] = Lib.CaptchaImage.GetCaptchaString();
                }
                else
                {
                    labelMessage.Text = "";
                    string account = txtAccount.Text.Trim();

                    //Kiểm tra tài khoản có tồn tại hay không?
                    if (!WebDB.User_Exists(account))
                    {
                        labelMessage.Text = "Tài khoản không tồn tại";
                        txtCaptcha.Text = "";
                        Session[Lib.SessionName.CAPTCHA] = Lib.CaptchaImage.GetCaptchaString();
                        return;
                    }

                    //Kiểm tra trong server có tồn tại nhân nhật hay không?
                    //
                    //TODO: Add code here
                    //


                    int amount = Converter.ToInt(txtAmount.Text, 0);
                    if (amount <= 0)
                    {
                        labelMessage.Text = "Số tiền khách thanh toán không hợp lệ";
                        txtCaptcha.Text = "";
                        Session[Lib.SessionName.CAPTCHA] = Lib.CaptchaImage.GetCaptchaString();
                        return;
                    }
                    int cardLogAmount = Converter.ToInt(txtCardLogAmount.Text, 0);
                    if (cardLogAmount < amount)
                    {
                        labelMessage.Text = "Số tiền được nạp vào game không hợp lệ";
                        txtCaptcha.Text = "";
                        Session[Lib.SessionName.CAPTCHA] = Lib.CaptchaImage.GetCaptchaString();
                        return;
                    }

                    string _GameID = AppManager.GameID;
                    if (_GameID != "")
                    {
                        WebDB.Bill_Create(_User.UserName, account,_GameID, cmbServer.SelectedValue, amount, cardLogAmount);
                        Response.Redirect("PaymentList.aspx",false);
                    }
                    else
                    {
                        Response.Redirect("Index.aspx", false);
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(string.Format("{0}<br />{1}",ex.Message,ex.StackTrace));
            }
        }
    }
}
