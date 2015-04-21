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
    public partial class TransHistory : Lib.UI.BasePage
    {
        protected int _page;
        protected long _countTotal;
        protected DateTime? _fromDate;
        protected DateTime? _toDate;
        protected string _type;
        protected string _server;
        protected string _pinNumber;
        protected string _account;
        protected string _payPurpose;

        public TransHistory()
            : base(Lib.AppFunctions.TRANSHISTORY)
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
            if (AppManager.GameID == "")
            {
                Response.Redirect("Index.aspx", false);
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    this.cmbServer.DataValueField = Lib.Meta.SERVER_NAME;
                    this.cmbServer.DataTextField = Lib.Meta.SERVER_FULLNAME;
                    this.cmbServer.DataSource = WebDB.Server_Select(true);
                    this.cmbServer.DataBind();

                    _page = Converter.ToInt(GetParamter("page"));
                    _fromDate = Converter.ToNullableDateTime(GetParamter("fromdate"));
                    _toDate = Converter.ToNullableDateTime(GetParamter("todate"));
                    _type = Converter.ToString(GetParamter("type"));
                    _server = Converter.ToString(GetParamter("server"));
                    _pinNumber = Converter.ToString(GetParamter("pin"));
                    _account = Converter.ToString(GetParamter("account"));
                    _payPurpose = Converter.ToString(GetParamter("paypurpose"));

                    //Nếu thời gian xem không được chỉ định thì chỉ lấy ngày hiện tại
                    if (_fromDate == null) _fromDate = DateTime.Today;
                    if (_toDate == null) _toDate = DateTime.Today;

                    txtFromDate.Text = Converter.ToShortDateString(_fromDate);
                    txtToDate.Text = Converter.ToShortDateString(_toDate);
                    cmbType.SelectedValue = _type;
                    cmbServer.SelectedValue = _server;
                    txtPin.Text = _pinNumber;
                    txtAccount.Text = _account;
                    txtPayPurpose.Text = _payPurpose;

                    if (_page <= 0) _page = 1;

                    ViewHistory();

                }
                else
                {
                    _page = 1;
                    _fromDate = Converter.ToNullableDateTime(txtFromDate.Text);
                    _toDate = Converter.ToNullableDateTime(txtToDate.Text);
                    _type = cmbType.SelectedValue;
                    _server = cmbServer.SelectedValue;
                    _pinNumber = txtPin.Text.Trim();
                    _account = txtAccount.Text.Trim();
                    _payPurpose = txtPayPurpose.Text.Trim();

                    ViewHistory();
                }
            }
        }

        protected void ViewHistory()
        {
            try
            {
                string linkFormat = "TransHistory.aspx?page={0}&fromdate={1:dd/MM/yyyy}&todate={2:dd/MM/yyyy}&type={3}&server={4}&pin={5}&account={6}&paypurpose={7}";

                int startPos = (_page - 1) * _PageSize + 1;

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
                        UIHelpers.CreateTableCell("Ngày/giờ nạp",Unit.Percentage(15),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Mã g.dịch",Unit.Percentage(7),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Serial - Mã thẻ",Unit.Percentage(20),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Loại thẻ",Unit.Percentage(5),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Server",Unit.Percentage(5),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("TT",Unit.Percentage(3),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Mã lỗi",Unit.Percentage(5),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Số tiền",Unit.Percentage(6),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Mục đích",Unit.Percentage(7),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Ghi chú",Unit.Percentage(10),HorizontalAlign.Left,"cellHeader")
                    }
                );
                table.Rows.Add(rowHeader);

                using (DataTable dt = WebDB.CardLog_Select(_fromDate, _toDate, _type, _server, _pinNumber, _account, _payPurpose, startPos, _PageSize))
                {
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        TableRow rowEmpty = new TableRow();
                        rowEmpty.Cells.Add(UIHelpers.CreateTableCell("<p>Không có dữ liệu</p>", HorizontalAlign.Center, "cell1", 11));
                        table.Rows.Add(rowEmpty);
                        this.txtTotal.Text = "0";
                    }
                    else
                    {
                        string css;
                        int stt = 0;
                        string ghichu;
                        bool allowViewDetails = CheckRight(Lib.AppFunctions.TRANSHISTORY_DETAILS);
                        _countTotal = Converter.ToLong(dt.Rows[0]["CountTotal"]);

                        string returnURL = Server.UrlEncode(string.Format(linkFormat, _page, _fromDate, _toDate, _type, _server, _pinNumber, _account, _payPurpose));
                        foreach (DataRow dr in dt.Rows)
                        {
                            stt += 1;
                            css = stt % 2 == 0 ? "cell1" : "cell2";
                            TableRow row = new TableRow();
                            if (dr[Lib.Meta.CARDLOG_STATUS].ToString().Trim() == "1" && dr[Lib.Meta.CARDLOG_ERRORCODE].ToString().Trim() == "0")
                            {
                                ghichu = "Thành công";
                            }
                            else
                            {
                                ghichu = "";
                            }
                            row.Cells.AddRange
                            (
                                new TableCell[]
                                {
                                    UIHelpers.CreateTableCell(string.Format("<a href='TransHistoryDetails.aspx?id={0}&returnURL={1}'><b>{2}</b></a>", dr[Lib.Meta.CARDLOG_ID], returnURL, dr[Lib.Meta.CARDLOG_CUSTOMERID]),HorizontalAlign.Left,css),                                    
                                    UIHelpers.CreateTableCell(dr[Lib.Meta.CARDLOG_ACCOUNT].ToString(),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(string.Format("{0:dd/MM/yyyy HH:mm:ss}",dr[Lib.Meta.CARDLOG_CREATED]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(dr["ExchangeID"].ToString(),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(allowViewDetails? string.Format("{0} - {1}",dr["Serial"], dr[Lib.Meta.CARDLOG_PINNUMBER]) : "**************",
                                                              HorizontalAlign.Left, css),
                                    UIHelpers.CreateTableCell(dr[Lib.Meta.CARDLOG_TYPE].ToString(),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(dr[Lib.Meta.CARDLOG_SERVER].ToString(),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(string.Format("<b>{0}</b>",dr[Lib.Meta.CARDLOG_STATUS]),HorizontalAlign.Center,css),
                                    UIHelpers.CreateTableCell(string.Format("<b>{0}</b>",dr[Lib.Meta.CARDLOG_ERRORCODE]), HorizontalAlign.Center, css),
                                    UIHelpers.CreateTableCell((!allowViewDetails && Converter.ToInt(dr[Lib.Meta.CARDLOG_AMOUNT]) > 0) ? "**********" : string.Format("{0:N0}",dr[Lib.Meta.CARDLOG_AMOUNT]),
                                                              HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(string.Format("{0}",dr[Lib.Meta.CARDLOG_PAYPURPOSE]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(ghichu,HorizontalAlign.Left,css)
                                }
                            );
                            table.Rows.Add(row);
                            this.txtTotal.Text = _countTotal.ToString();
                        }
                    }
                }
                this.panelList.Controls.Clear();
                this.panelList.Controls.Add(table);


                this.linkFirst.NavigateUrl = string.Format(linkFormat, 1, _fromDate, _toDate, _type, _server, _pinNumber, _account, _payPurpose);
                this.linkPrev.NavigateUrl = string.Format(linkFormat, _page > 0 ? _page - 1 : 1, _fromDate, _toDate, _type, _server, _pinNumber, _account, _payPurpose);
                this.linkNext.NavigateUrl = string.Format(linkFormat, _page + 1, _fromDate, _toDate, _type, _server, _pinNumber, _account, _payPurpose);
                this.linkLast.NavigateUrl = string.Format(linkFormat, _countTotal / _PageSize, _fromDate, _toDate, _type, _server, _pinNumber, _account, _payPurpose);
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

        private Table GetToExcel()
        {
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
                    UIHelpers.CreateTableCell("Ngày/giờ nạp",Unit.Percentage(15),HorizontalAlign.Left,"cellHeader"),
                    UIHelpers.CreateTableCell("Mã g.dịch",Unit.Percentage(7),HorizontalAlign.Left,"cellHeader"),
                    UIHelpers.CreateTableCell("Serial - Mã thẻ",Unit.Percentage(20),HorizontalAlign.Left,"cellHeader"),
                    UIHelpers.CreateTableCell("Loại thẻ",Unit.Percentage(5),HorizontalAlign.Left,"cellHeader"),
                    UIHelpers.CreateTableCell("Server",Unit.Percentage(5),HorizontalAlign.Left,"cellHeader"),
                    UIHelpers.CreateTableCell("TT",Unit.Percentage(3),HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Mã lỗi",Unit.Percentage(5),HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Số tiền",Unit.Percentage(6),HorizontalAlign.Left,"cellHeader"),
                    UIHelpers.CreateTableCell("Mục đích",Unit.Percentage(7),HorizontalAlign.Left,"cellHeader"),
                    UIHelpers.CreateTableCell("Ghi chú",Unit.Percentage(10),HorizontalAlign.Left,"cellHeader")
                }
            );
            table.Rows.Add(rowHeader);
            try
            {
                using (DataTable dt = WebDB.CardLog_Select(_fromDate, _toDate, _type, _server, _pinNumber, _account, _payPurpose, 0, Converter.ToInt(_countTotal)+1))
                {
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        TableRow rowEmpty = new TableRow();
                        rowEmpty.Cells.Add(UIHelpers.CreateTableCell("<p>Không có dữ liệu</p>", HorizontalAlign.Center, "cell1", 11));
                        table.Rows.Add(rowEmpty);
                        this.txtTotal.Text = "0";
                    }
                    else
                    {
                        string css;
                        int stt = 0;
                        string ghichu;
                        bool allowViewDetails = CheckRight(Lib.AppFunctions.TRANSHISTORY_DETAILS);
                        _countTotal = Converter.ToLong(dt.Rows[0]["CountTotal"]);

                        foreach (DataRow dr in dt.Rows)
                        {
                            stt += 1;
                            css = stt % 2 == 0 ? "cell1" : "cell2";
                            TableRow row = new TableRow();
                            if (dr[Lib.Meta.CARDLOG_STATUS].ToString().Trim() == "1" && dr[Lib.Meta.CARDLOG_ERRORCODE].ToString().Trim() == "0")
                            {
                                ghichu = "Thành công";
                            }
                            else
                            {
                                ghichu = "";
                            }
                            row.Cells.AddRange
                            (
                                new TableCell[]
                                {
                                    UIHelpers.CreateTableCell(string.Format("{0}", dr[Lib.Meta.CARDLOG_CUSTOMERID]),HorizontalAlign.Left,css),                                    
                                    UIHelpers.CreateTableCell(dr[Lib.Meta.CARDLOG_ACCOUNT].ToString(),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(string.Format("{0:dd/MM/yyyy HH:mm:ss}",dr[Lib.Meta.CARDLOG_CREATED]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(dr["ExchangeID"].ToString(),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(allowViewDetails? string.Format("{0} - {1}",dr["Serial"], dr[Lib.Meta.CARDLOG_PINNUMBER]) : "**************",
                                                              HorizontalAlign.Left, css),
                                    UIHelpers.CreateTableCell(dr[Lib.Meta.CARDLOG_TYPE].ToString(),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(dr[Lib.Meta.CARDLOG_SERVER].ToString(),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(string.Format("<b>{0}</b>",dr[Lib.Meta.CARDLOG_STATUS]),HorizontalAlign.Center,css),
                                    UIHelpers.CreateTableCell(string.Format("<b>{0}</b>",dr[Lib.Meta.CARDLOG_ERRORCODE]), HorizontalAlign.Center, css),
                                    UIHelpers.CreateTableCell((!allowViewDetails && Converter.ToInt(dr[Lib.Meta.CARDLOG_AMOUNT]) > 0) ? "**********" : string.Format("{0:N0}",dr[Lib.Meta.CARDLOG_AMOUNT]),
                                                              HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(string.Format("{0}",dr[Lib.Meta.CARDLOG_PAYPURPOSE]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(ghichu,HorizontalAlign.Left,css)
                                }
                            );
                            table.Rows.Add(row);
                        }
                    }
                }
            }
            catch
            { }
            return table;
        }

        protected void buttonExportToExcel_Click(object sender, EventArgs e)
        {
            Lib.DataExporter.ExportTable(GetToExcel(),
                                        IDAdmin.Lib.ExportFormat.Excel,
                                        string.Format("{0}_TransHistory_{1:dd/MM/yyyy}.xls", AppManager.GameID, DateTime.Today));
        }
    }
}
