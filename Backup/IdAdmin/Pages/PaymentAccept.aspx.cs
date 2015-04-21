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
    public partial class PaymentAccept : Lib.UI.BasePage
    {
        long _BillID;
        string _GameID;
        string _GameName;

        public PaymentAccept()
            : base(Lib.AppFunctions.PAYMENT_ACCEPT)
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
                try
                {
                    string _action = GetParamter("action");
                    _BillID = Converter.ToLong(GetParamter("id"));
                    if (_BillID == 0)
                    {
                        Response.Redirect("PaymentList.aspx", false);
                    }
                    else
                    {                        
                        if (_action == "delete")
                        {
                            WebDB.Bill_Delete(_User.UserName, _BillID);
                            Response.Redirect("PaymentList.aspx", false);
                        }
                        else
                        {
                            if (!Page.IsPostBack)
                            {
                                DataRow drBill = WebDB.Bill_DetailsForAccept(_BillID);
                                if (drBill == null)
                                {
                                    Response.Redirect("PaymentList.aspx", false);
                                }
                                else
                                {
                                    txtBillID.Text = drBill[Lib.Meta.BILL_BILLID].ToString();
                                    txtCreatedTime.Text = string.Format("{0:dd/MM/yyyy HH:mm:ss}", drBill[Lib.Meta.BILL_CREATEDTIME]);
                                    txtCreatedUserID.Text = drBill[Lib.Meta.BILL_CREATEDUSERID].ToString();
                                    txtAccount.Text = drBill[Lib.Meta.BILL_ACCOUNT].ToString();
                                    txtAmount.Text = string.Format("{0:N0}", drBill[Lib.Meta.BILL_AMOUNT]);
                                    labelAmount.Text = Lib.Utils.NumberReader.CurencyToString(drBill[Lib.Meta.BILL_AMOUNT].ToString());
                                    txtCardLogAmount.Text = string.Format("{0:N0}", drBill[Lib.Meta.BILL_CARDLOGAMOUNT]);
                                    labelCardLogAmount.Text = Lib.Utils.NumberReader.CurencyToString(drBill[Lib.Meta.BILL_CARDLOGAMOUNT].ToString());
                                    txtGameName.Text = drBill["GameName"].ToString();
                                    txtGameServer.Text = drBill[Lib.Meta.BILL_GAMESERVER].ToString();

                                    this.checkAccept.Checked = false;


                                    if (_User.UserName.Trim() == drBill[Lib.Meta.BILL_CREATEDUSERID].ToString().Trim())
                                    {
                                        this.checkAccept.Visible = false;
                                        this.buttonAccept.Visible = false;
                                        this.labelMessage.Text = "Lưu ý: Bạn không được phép xử lý hóa đơn do chính mình lập";
                                    }
                                }
                            } 
                            this.buttonAccept.Click+=new EventHandler(buttonAccept_Click);
                            this.buttonCancel.Click +=new EventHandler(buttonCancel_Click);
                        }
                    }
                }
                catch
                {
                }
            }
        }

        protected void buttonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("PaymentList.aspx", false);
        }

        protected void buttonAccept_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkAccept.Checked)
                {
                    labelMessage.Text = "[Chưa xác nhận]";
                    return;
                }

                DataRow drBill = WebDB.Bill_DetailsForAccept(_BillID);
                _GameID = drBill["GameID"].ToString();
                _GameName = drBill["GameName"].ToString();

                if (drBill == null)
                {
                    Response.Redirect("PaymentList.aspx", false);
                }

                WebDB.Bill_Accept(_User.UserName, _BillID, Request.UserHostAddress);

                try
                {
                    string to = System.Configuration.ConfigurationManager.AppSettings["MailAddresses"];
                    string subject = string.Format("[{0:ddMMyyyy}]{1}_{2}_{3}_{4}",
                                                   DateTime.Today,
                                                   _GameID,
                                                   txtAccount.Text,
                                                   txtGameServer.Text,
                                                   txtAmount.Text);                    
                    System.Text.StringBuilder bodyBuilder = new System.Text.StringBuilder();
                    bodyBuilder.Append(@"<html>");
                    bodyBuilder.Append(string.Format(@"<b>Nạp tiền cho Game thủ: {0}</b><br />", _GameName));
                    bodyBuilder.Append(@"<table width='90%' style='border:0px'>");
                    bodyBuilder.Append(string.Format(@"<tr><td>Tài khoản nạp tiền:</td><td>{0}</td></tr>", txtAccount.Text));
                    bodyBuilder.Append(string.Format(@"<tr><td>Game Server:</td><td>{0}</td></tr>", txtGameServer.Text));
                    bodyBuilder.Append(string.Format(@"<tr><td>Số tiền thu của khách hàng:</td><td>{0}</td></tr>", txtAmount.Text));
                    bodyBuilder.Append(string.Format(@"<tr><td>Số tiền nạp vào game:</td><td>{0}</td></tr>", txtCardLogAmount.Text));
                    bodyBuilder.Append(string.Format(@"<tr><td>Người lập hóa đơn:</td><td>{0}</td></tr>", txtCreatedUserID.Text));
                    bodyBuilder.Append(string.Format(@"<tr><td>Thời điểm lập hóa đơn:</td><td>{0}</td></tr>", txtCreatedTime.Text));
                    bodyBuilder.Append(string.Format(@"<tr><td>Người duyệt hóa đơn:</td><td>{0}</td></tr>", _User.UserName));
                    bodyBuilder.Append(@"</table>");
                    bodyBuilder.Append(@"</html>");

                    Lib.Utils.MailHelper.SendMail(to, subject, bodyBuilder.ToString());
                }
                catch (Exception ex)
                {
                    Response.Write(string.Format("{0}<br />{1}",ex.Message, ex.StackTrace));
                    return;
                    
                }

                Response.Redirect("PaymentList.aspx", false);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message +": " + ex.StackTrace);
            }
            
        }


    }
}
