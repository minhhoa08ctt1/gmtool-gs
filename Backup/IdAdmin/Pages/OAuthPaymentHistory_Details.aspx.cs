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
    public partial class OAuthPaymentHistory_Details : Lib.UI.BasePage
    {
        public OAuthPaymentHistory_Details()
            : base(Lib.AppFunctions.OAUTH_PAYMENT_HISTORY)
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
                long _id = Converter.ToLong(GetParamter("id"));
                string _returnURL = GetParamter("returnURL");
                if (_id <= 0)
                {
                    Response.Redirect(_returnURL, false);
                }
                else
                {
                    DataRow dr = WebDB.OAuthPaymentLog_Details(_id);
                    if (dr == null)
                    {
                        Response.Redirect(_returnURL, false);
                    }
                    else
                    {
                        linkBack.NavigateUrl = _returnURL;

                        labelServiceID.Text = dr["ServiceID"].ToString();
                        labelOrderID.Text = dr["OrderID"].ToString();
                        labelCreated.Text = string.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["Created"]);
                        labelOrderInfo.Text = dr["OrderInfo"].ToString();
                        labelAmount.Text = string.Format("{0:N0}", dr["Amount"]);
                        labelPayMethod.Text = dr["PayMethod"].ToString();
                        labelRedirectURL.Text = dr["RedirectURL"].ToString();
                        labelSignature.Text = dr["Signature"].ToString();
                        labelIP.Text = dr["IP"].ToString();
                        labelStatus.Text = dr["Status"].ToString();
                        labelUserName.Text = dr["UserName"].ToString();
                        labelFinishTime.Text = string.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["FinishTime"]);
                        labelResponseCode.Text = dr["ResponseCode"].ToString();
                        labelResponseMsg.Text = dr["ResponseMsg"].ToString();
                    }
                }
            }
        }
    }
}
