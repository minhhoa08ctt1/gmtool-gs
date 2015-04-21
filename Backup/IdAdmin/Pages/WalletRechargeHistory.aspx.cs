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
    public partial class WalletRechargeHistory : Lib.UI.BasePage
    {
        protected int _page;        
        protected DateTime? _fromDate;
        protected DateTime? _toDate;
        protected string _type;        
        protected string _pinNumber;
        protected string _account;

        public WalletRechargeHistory()
            : base(Lib.AppFunctions.WALLET_RECHARGE_HISTORY)
        { 
        }

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
                if (!Page.IsPostBack)
                {
                    _page = Converter.ToInt(GetParamter("page"));
                    _fromDate = Converter.ToNullableDateTime(GetParamter("fromdate"));
                    _toDate = Converter.ToNullableDateTime(GetParamter("todate"));
                    _type = Converter.ToString(GetParamter("type"));                    
                    _pinNumber = Converter.ToString(GetParamter("pin"));
                    _account = Converter.ToString(GetParamter("account"));

                    //Nếu thời gian xem không được chỉ định thì chỉ lấy ngày hiện tại
                    if (_fromDate == null) _fromDate = DateTime.Today;
                    if (_toDate == null) _toDate = DateTime.Today;

                    txtFromDate.Text = Converter.ToShortDateString(_fromDate);
                    txtToDate.Text = Converter.ToShortDateString(_toDate);
                    cmbType.SelectedValue = _type;                    
                    txtPin.Text = _pinNumber;
                    txtAccount.Text = _account;

                    if (_page <= 0) _page = 1;

                    ViewHistory();
                }
                else
                {
                    _page = 1;
                    _fromDate = Converter.ToNullableDateTime(txtFromDate.Text);
                    _toDate = Converter.ToNullableDateTime(txtToDate.Text);
                    _type = cmbType.SelectedValue;                    
                    _pinNumber = txtPin.Text.Trim();
                    _account = txtAccount.Text.Trim();

                    ViewHistory();
                }
            }
        }


        protected void ViewHistory()
        {
            try
            {
                string linkFormat = "WalletRechargeHistory.aspx?page={0}&fromdate={1:dd/MM/yyyy}&todate={2:dd/MM/yyyy}&type={3}&pin={4}&account={5}";

                Table table = new Table();
                table.CssClass = "table1";
                table.CellSpacing = 1;

                TableHeaderRow rowHeader = new TableHeaderRow();
                rowHeader.Cells.AddRange
                (
                    new TableCell[]
                    {   
                        UIHelpers.CreateTableCell("Người nạp",Unit.Percentage(10),HorizontalAlign.Left, "cellHeader"),                                                
                        UIHelpers.CreateTableCell("Tài khoản đích",Unit.Percentage(10),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Ngày/giờ nạp",Unit.Percentage(10),HorizontalAlign.Left,"cellHeader"),                        
                        UIHelpers.CreateTableCell("Serial - Mã thẻ",Unit.Percentage(15),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Loại thẻ",Unit.Percentage(5),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Số tiền",Unit.Percentage(7),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("GOSU",Unit.Percentage(5),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("GOSU tặng",Unit.Percentage(5),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("TT",Unit.Percentage(3),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Mã lỗi",Unit.Percentage(5),HorizontalAlign.Center,"cellHeader"),                                                
                        UIHelpers.CreateTableCell("Ghi chú",Unit.Percentage(15),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Mã giảm giá",Unit.Percentage(10),HorizontalAlign.Left,"cellHeader")
                    }
                );
                table.Rows.Add(rowHeader);

                using (DataTable dt = WebDB.WalletRechargeLog_Select(_fromDate, _toDate, _type, _pinNumber, _account, _page, _PageSize))
                {
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        TableRow rowEmpty = new TableRow();
                        rowEmpty.Cells.Add(UIHelpers.CreateTableCell("<p>Không có dữ liệu</p>", HorizontalAlign.Center, "cell1", 11));
                        table.Rows.Add(rowEmpty);
                    }
                    else
                    {
                        string css = "cell1";                                                             

                        string returnURL = Server.UrlEncode(string.Format(linkFormat, _page, _fromDate, _toDate, _type, _pinNumber, _account));
                        foreach (DataRow dr in dt.Rows)
                        {                            
                            css = css=="cell2" ? "cell1" : "cell2";
                            TableRow row = new TableRow();
                            string Pincode = "";
                            if (dr[Lib.Meta.CARDLOG_PINNUMBER].ToString() != "")
                            {
                                Pincode = "**********" + dr[Lib.Meta.CARDLOG_PINNUMBER].ToString().Substring(dr[Lib.Meta.CARDLOG_PINNUMBER].ToString().Length - 4);
                            }
                            row.Cells.AddRange
                            (
                                new TableCell[]
                                {
                                    UIHelpers.CreateTableCell(string.Format("<a href='WalletRechargeHistory_Edit.aspx?id={0}&returnURL={1}'><b>{2}</b></a>", dr["RechargeID"], returnURL, dr["RechargeUser"]),HorizontalAlign.Left,css),                                    
                                    UIHelpers.CreateTableCell(dr["UserName"].ToString(),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(string.Format("{0:dd/MM/yyyy HH:mm:ss}",dr["Created"]),HorizontalAlign.Left,css),                                    
                                    UIHelpers.CreateTableCell(string.Format("{0} - {1}",dr["Serial"], Pincode), HorizontalAlign.Left, css),
                                    UIHelpers.CreateTableCell(dr["Type"].ToString(),HorizontalAlign.Left,css),                     
                                    UIHelpers.CreateTableCell(string.Format("{0:N0}",dr["Amount"]),HorizontalAlign.Left,css),      
                                    UIHelpers.CreateTableCell(string.Format("{0:N0}",dr["WalletAmount"]),HorizontalAlign.Left,css),      
                                    UIHelpers.CreateTableCell(string.Format("{0:N0}",dr["PromotionAmount"]),HorizontalAlign.Left,css),      
                                    UIHelpers.CreateTableCell(string.Format("<b>{0}</b>",dr["Status"]),HorizontalAlign.Center,css),
                                    UIHelpers.CreateTableCell(string.Format("<b>{0}</b>",dr["ErrorCode"]), HorizontalAlign.Center, css),
                                    UIHelpers.CreateTableCell(dr["Msg"].ToString(),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(dr["AdditionData"].ToString(),HorizontalAlign.Left,css)
                                }
                            );
                            table.Rows.Add(row);
                        }
                    }
                }
                this.panelList.Controls.Clear();
                this.panelList.Controls.Add(table);


                this.linkFirst.NavigateUrl = string.Format(linkFormat, 1, _fromDate, _toDate, _type, _pinNumber, _account);
                this.linkPrev.NavigateUrl = string.Format(linkFormat, _page > 0 ? _page - 1 : 1, _fromDate, _toDate, _type, _pinNumber, _account);
                this.linkNext.NavigateUrl = string.Format(linkFormat, _page + 1, _fromDate, _toDate, _type, _pinNumber, _account);                
            }
            catch (Exception ex)
            {
                Label labelErrorMessage = new Label();
                labelErrorMessage.ForeColor = System.Drawing.Color.Red;
                labelErrorMessage.Text = string.Format("Lỗi: {0}", ex.Message);
                this.panelList.Controls.Clear();
                this.panelList.Controls.Add(labelErrorMessage);
            }
        }
    }
}
