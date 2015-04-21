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
    public partial class WalletRechargeHistory_Edit : Lib.UI.BasePage
    {
        long _rechargeID;
        string _returnURL;

        public WalletRechargeHistory_Edit()
            : base(Lib.AppFunctions.WALLET_RECHARGE_HISTORY)
        { }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsLogin())
            {
                Response.Redirect("LogOn.aspx", false);
            }
            if (!CheckRight())
            {
                RedirectToDeniedMessage();
            }
            else
            {
                _rechargeID = Converter.ToLong(GetParamter("id"));
                _returnURL = GetParamter("returnURL");

                if (!Page.IsPostBack)
                {
                    Session[Lib.SessionName.CAPTCHA] = Lib.CaptchaImage.GetCaptchaString();
                    ViewData();
                }

                linkBack.NavigateUrl = _returnURL;
            }
        }

        private void ViewData()
        {
            try
            {
                DataRow dr = WebDB.WalletRechargeLog_Details(_rechargeID);
                if (dr != null)
                {
                    labelRechargeID.Text = dr["RechargeID"].ToString();
                    labelRechargeUser.Text = dr["RechargeUser"].ToString();
                    labelUserName.Text = dr["UserName"].ToString();
                    labelCreated.Text = string.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["Created"]);
                    labelType.Text = dr["Type"].ToString();
                    labelPinNumber.Text = string.Format("{0} - {1}", dr["Serial"], dr["PinNumber"]);
                    labelAmount.Text = string.Format("{0:N0}", dr["Amount"]);
                    labelWalletAmount.Text = string.Format("{0:N0}", dr["WalletAmount"]);
                    labelPromotionAmount.Text = string.Format("{0:N0}", dr["PromotionAmount"]);
                    labelStatus.Text = dr["Status"].ToString();
                    labelErrorCode.Text = dr["ErrorCode"].ToString();
                    labelMsg.Text = dr["Msg"].ToString();

                    if (Converter.ToInt(dr["Status"]) == 1 && Converter.ToInt(dr["ErrorCode"]) == 0)
                    {
                        panelEdit.Visible = false;
                    }
                }
                else
                {
                    Response.Redirect(_returnURL, false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void buttonUpdateLog_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsValid)
                    return;

                if (txtCaptcha.Text != Session[Lib.SessionName.CAPTCHA].ToString())
                {                    
                    labelMessage.Text = "Mã bảo vệ không đúng!";
                    txtCaptcha.Text = "";
                    Session[Lib.SessionName.CAPTCHA] = Lib.CaptchaImage.GetCaptchaString();
                }
                else
                {
                    Session[Lib.SessionName.CAPTCHA] = Lib.CaptchaImage.GetCaptchaString();

                    int _EditAmount = Converter.ToInt(txtEditAmount.Text, 0);
                    if (_EditAmount <= 0)
                    {
                        labelMessage.Text = "Số tiền không hợp lệ";
                    }
                    else
                    {
                        string logAction = string.Format("WalletRechargeLog_Update: {0}, ({1},{2})=>({3},{4}), {5:N0} VNĐ", _rechargeID, labelStatus.Text, labelErrorCode.Text, 1, 0, _EditAmount);
                        int updateResult = WebDB.WalletRechargeLog_Update(_rechargeID, _EditAmount, 1, 0, string.Format("Nạp tiền thành công ({0})", _User.UserName), _User.UserName, logAction, Request.UserHostAddress);
                        switch (updateResult)
                        {
                            case -99:
                                labelMessage.Text = "Lỗi hệ thống. Vui lòng thử lại";
                                break;
                            case -1:
                                labelMessage.Text = "Giao dịch không tồn tại hoặc đã thực hiện thành công. Không được sửa đổi";
                                break;
                            case -0:
                                labelMessage.Text = "Cập nhật giao dịch không thành công";
                                break;
                            default:
                                Response.Redirect(_returnURL);
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
