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
    public partial class TransHistoryDetails : Lib.UI.BasePage
    {
        private long _ID;
        string _returnURL = "";

        private const string XGATE_PARTNER_ID = "idgosu";
        private const string XGATE_PARTNER_KEY = "gosu@id!s#d%d&v(c@s$s^vn*";

        public TransHistoryDetails()
            : base(Lib.AppFunctions.TRANSHISTORY_DETAILS)
        {
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsLogin())
            {
                RedirectToLogOn();
            }
            if (!CheckRight())
            {
                RedirectToDeniedMessage();
            }
            else
            {
                _ID = Converter.ToLong(GetParamter("ID"));
                _returnURL = GetParamter("returnURL");

                if (_ID == 0)
                {
                    GoBack();
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        ViewCardLogDetails();
                    }
                }
            }
        }

        private void GoBack()
        {

            if (_returnURL != "")
            {
                Response.Redirect(_returnURL, false);
            }
            else
            {
                Response.Redirect("TransHistory.aspx", false);
            }
        }

        private void ViewCardLogDetails()
        {
            try
            {
                DataRow drDetails = WebDB.CardLog_Details(_ID);
                if ((drDetails != null) && (drDetails["GameID"].ToString() == AppManager.GameID))
                {
                    txtExchangeID.Text = Converter.ToString(drDetails["ExchangeID"]);
                    txtCreated.Text = string.Format("{0:dd/MM/yyyy HH:mm:ss}", drDetails["Created"]);
                    txtAccount.Text = Converter.ToString(drDetails["Account"]);
                    txtCustomerID.Text = Converter.ToString(drDetails["CustomerID"]);
                    txtAmount.Text = string.Format("{0:N0}", drDetails["Amount"]);
                    txtGold.Text = string.Format("{0:N0}", Converter.ToInt(drDetails["Gold"]));
                    txtServer.Text = Converter.ToString(drDetails["ServerName"]);
                    txtType.Text = Converter.ToString(drDetails["Type"]);
                    txtSerial.Text = Converter.ToString(drDetails["Serial"]);
                    txtPinNumber.Text = Converter.ToString(drDetails["PinNumber"]);
                    txtStatus.Text = Converter.ToString(drDetails["Status"]);
                    txtErrorCode.Text = Converter.ToString(drDetails["ErrorCode"]);
                    txtIP.Text = Converter.ToString(drDetails["IP"]);

                    //PaymentGateLog
                    Table tableGateLog = new Table();
                    tableGateLog.Width = Unit.Percentage(100);

                    TableRow rowH = new TableRow();
                    rowH.Cells.AddRange
                    (
                        new TableCell[]
                            {
                                UIHelpers.CreateTableCell("<b>Created Time</b>",Unit.Percentage(30), HorizontalAlign.Left,""),
                                UIHelpers.CreateTableCell("<b>Response Code</b>",Unit.Percentage(10), HorizontalAlign.Center,""),
                                UIHelpers.CreateTableCell("<b>Result Message</b>",Unit.Percentage(60), HorizontalAlign.Left,"")                                
                            }
                    );
                    tableGateLog.Rows.Add(rowH);

                    using (DataTable dt = WebDB.PaymentGateLog_Select(Converter.ToString(drDetails["ExchangeID"])))
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            TableRow row = new TableRow();
                            row.Cells.AddRange
                            (
                                new TableCell[]
                                {
                                    UIHelpers.CreateTableCell(string.Format("{0:dd/MM/yyyy HH:mm:ss}",dr["Created"]),Unit.Percentage(30), HorizontalAlign.Left,""),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["ResponseCode"]),Unit.Percentage(10), HorizontalAlign.Center,""),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["Message"]),Unit.Percentage(60), HorizontalAlign.Left,"") 
                                }
                            );
                            tableGateLog.Rows.Add(row);
                        }
                    }

                    panelGateLog.Controls.Clear();
                    panelGateLog.Controls.Add(tableGateLog);

                    //Truy vấn lại kết quả giao dịch thẻ và để xử lý khi lỗi
                    //int _Status = Converter.ToInt(drDetails["ErrorCode"]);
                    //if ((_Status != 0) || (_Status == 0 && Converter.ToInt(drDetails["Amount"]) == 0))
                    //{
                        if (CheckRight(Lib.AppFunctions.TRANSHISTORY_EDIT))
                        {
                            string trace = Converter.ToString(drDetails["ExchangeID"]).Trim();
                            XGateCard.XGO cardService = new IDAdmin.XGateCard.XGO();
                            string signature = Lib.Utils.CryptHelper.EncodeMD5(string.Format("{0}|{1}|{2}",
                                                                                  XGATE_PARTNER_ID,
                                                                                  trace,
                                                                                  XGATE_PARTNER_KEY));
                            string _result = "";
                            try
                            {
                                _result = cardService.GetTransaction(XGATE_PARTNER_ID, trace, signature);
                            }
                            catch
                            {
                            }

                            panelTransEdit.Visible = true;

                            txtQueryResult.Text = _result;
                            txtEditServer.Text = drDetails["ServerName"].ToString();
                            txtEditAmount.Text = drDetails["Amount"].ToString();
                        }
                        else
                        {
                            panelTransEdit.Visible = false;
                        }
                //    }
                }
                else
                {
                    GoBack();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void buttonEditTrans_Click(object sender, EventArgs e)
        {
            try
            {
                string _serverName = txtEditServer.Text.Trim().ToUpper();
                if (_serverName == "") //|| !_serverName.StartsWith("S"))
                {
                    labelEditMessage.Text = "Server không hợp lệ";
                    return;
                }

                int _amount = Converter.ToInt(txtEditAmount.Text);

                int _status = 1;
                int _errorcode = 0;

                string _editStatus = cmbEditSatus.SelectedValue;
                if (_editStatus == "")
                {
                    labelEditMessage.Text = "Chưa chọn cách xử lý";
                    return;
                }
                else if (_editStatus == "Succeed")  //Đã kết thúc => Thành công
                {
                    _status = 1;
                    _errorcode = 0;
                }
                else if (_editStatus == "NotSucceed") //Đã kết thúc => Thẻ không hợp lệ
                {
                    _status = -2;
                    _errorcode = 1;
                }
                else if (_editStatus == "Pending") //Chờ nạp vàng
                {
                    if (_amount == 0)
                    {
                        labelEditMessage.Text = "Số tiền không hợp lệ";
                        return;
                    }
                    _status = 0;
                    _errorcode = 0;
                }
                else
                {
                    labelEditMessage.Text = "Chưa chọn cách xử lý";
                    return;
                }

                if (!chkAccept.Checked)
                {
                    labelEditMessage.Text = "Vui lòng xác nhận đồng ý xử lý";
                    return;
                }

                WebDB.CardLog_Update(_ID, _serverName, _amount, _status, _errorcode);
                WebDB.WriteLog(_User.UserName, Request.UserHostAddress, string.Format("CardLog_Edit: {0}, {1}, {2}, {3}, {4}, {5}", AppManager.GameID, _ID, _serverName, _amount, _status, _errorcode));

                GoBack();

            }
            catch (Exception ex)
            {
                labelEditMessage.Text = "Lỗi: " + ex.Message;
            }
        }
    }
}
