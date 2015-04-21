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
    public partial class WalletSpendingCode : Lib.UI.BasePage
    {
        protected int _page;
        protected long _countTotal;
        protected long _countTotalCode;
        protected DateTime? _fromDate;
        protected DateTime? _toDate;
        protected string _kind;
        protected int _amount;
        protected string _account;
        protected int _status;

        public WalletSpendingCode()
            : base(Lib.AppFunctions.WALLET_BUYCODE_HISTORY)
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
            if (AppManager.GameID == "")
            {
                Response.Redirect("Index.aspx", false);
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    _page = Converter.ToInt(GetParamter("page"));
                    _fromDate = Converter.ToNullableDateTime(GetParamter("fromdate"));
                    _toDate = Converter.ToNullableDateTime(GetParamter("todate"));
                    _kind = Converter.ToString(GetParamter("type"));
                    _amount = Converter.ToInt(GetParamter("amount"));
                    _account = Converter.ToString(GetParamter("account"));
                    _status = Converter.ToInt(GetParamter("status"));

                    //Nếu thời gian xem không được chỉ định thì chỉ lấy ngày hiện tại
                    if (_fromDate == null) _fromDate = DateTime.Today;
                    if (_toDate == null) _toDate = DateTime.Today;

                    txtFromDate.Text = Converter.ToShortDateString(_fromDate);
                    txtToDate.Text = Converter.ToShortDateString(_toDate);
                    ddlKind.SelectedValue = _kind;
                    ddlAmount.SelectedValue = _amount.ToString();
                    txtAccount.Text = _account;
                    ddlStatus.SelectedValue = _status.ToString();

                    if (_page <= 0) _page = 1;

                    ViewHistory();
                }
                else
                {
                    _page = 1;
                    _fromDate = Converter.ToNullableDateTime(txtFromDate.Text);
                    _toDate = Converter.ToNullableDateTime(txtToDate.Text);
                    _kind = ddlKind.SelectedValue;
                    _amount = Converter.ToInt(ddlAmount.SelectedValue);
                    _account = txtAccount.Text.Trim();
                    _status = Converter.ToInt(ddlStatus.SelectedValue);

                    ViewHistory();
                }
            }
        }

        protected void ViewHistory()
        {
            try
            {
                string linkFormat = "WalletSpendingCode.aspx?page={0}&fromdate={1:dd/MM/yyyy}&todate={2:dd/MM/yyyy}&type={3}&amount={4}&account={5}&status={6}";

                Table table = new Table();
                table.CssClass = "table1";
                table.CellSpacing = 1;

                TableHeaderRow rowHeader = new TableHeaderRow();
                rowHeader.Cells.AddRange
                (
                    new TableCell[]
                    {   
                        UIHelpers.CreateTableCell("Người mua",Unit.Percentage(10),HorizontalAlign.Left, "cellHeader"),                                                
                        UIHelpers.CreateTableCell("Ngày/giờ",Unit.Percentage(12),HorizontalAlign.Left,"cellHeader"),                        
                        UIHelpers.CreateTableCell("Mệnh giá hóa đơn",Unit.Percentage(10),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Loại thẻ",Unit.Percentage(10),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Số Code",Unit.Percentage(5),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Bonus Code",Unit.Percentage(5),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("TT",Unit.Percentage(3),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Mã lỗi",Unit.Percentage(5),HorizontalAlign.Center,"cellHeader"),                                                
                        UIHelpers.CreateTableCell("Ghi chú",Unit.Percentage(20),HorizontalAlign.Left,"cellHeader")
                    }
                );
                table.Rows.Add(rowHeader);

                using (DataTable dt = WebDB.WalletSpendingLog_Select(_fromDate, _toDate, _kind, _amount, _account, _status, _page, _PageSize))
                {
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        TableRow rowEmpty = new TableRow();
                        rowEmpty.Cells.Add(UIHelpers.CreateTableCell("<p>Không có dữ liệu</p>", HorizontalAlign.Center, "cell1", 11));
                        table.Rows.Add(rowEmpty);
                        this.txtTotal.Text = "0";
                        this.txtTotalCode.Text = "0";
                    }
                    else
                    {
                        string css = "cell1";
                        _countTotal = Converter.ToLong(dt.Rows[0]["CountTotal"]);
                        _countTotalCode = Converter.ToLong(dt.Rows[0]["CountTotalCode"]);
                        string returnURL = Server.UrlEncode(string.Format(linkFormat, _page, _fromDate, _toDate, _kind, _amount, _account, _status));
                        foreach (DataRow dr in dt.Rows)
                        {
                            css = css == "cell2" ? "cell1" : "cell2";
                            TableRow row = new TableRow();
                            row.Cells.AddRange
                            (
                                new TableCell[]
                                {
                                    UIHelpers.CreateTableCell(dr["Account"].ToString(),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(string.Format("{0:dd/MM/yyyy HH:mm:ss}",dr["Created"]),HorizontalAlign.Left,css),                                    
                                    UIHelpers.CreateTableCell(string.Format("{0:N0}",dr["VNDAmount"]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(dr["CodeType"].ToString(),HorizontalAlign.Left,css),                                    
                                    UIHelpers.CreateTableCell(string.Format("{0:N0}",dr["CodeAmount"]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(string.Format("{0:N0}",dr["CodeBonus"]),HorizontalAlign.Left,css),     
                                    UIHelpers.CreateTableCell(string.Format("<b>{0}</b>",dr["Status"]),HorizontalAlign.Center,css),
                                    UIHelpers.CreateTableCell(string.Format("<b>{0}</b>",dr["ErrorCode"]), HorizontalAlign.Center, css),
                                    UIHelpers.CreateTableCell(dr["Msg"].ToString(),HorizontalAlign.Left,css)
                                }
                            );
                            table.Rows.Add(row);
                            this.txtTotal.Text = _countTotal.ToString();
                            this.txtTotalCode.Text = _countTotalCode.ToString();
                        }
                    }
                }
                this.panelList.Controls.Clear();
                this.panelList.Controls.Add(table);


                this.linkFirst.NavigateUrl = string.Format(linkFormat, 1, _fromDate, _toDate, _kind, _amount, _account, _status);
                this.linkPrev.NavigateUrl = string.Format(linkFormat, _page > 0 ? _page - 1 : 1, _fromDate, _toDate, _kind, _amount, _account, _status);
                this.linkNext.NavigateUrl = string.Format(linkFormat, _page + 1, _fromDate, _toDate, _kind, _amount, _account, _status);
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
