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
    public partial class CardLog_CheckError : Lib.UI.BasePage
    {
        protected DateTime? _fromDate;
        protected DateTime? _toDate;
        protected string _type;
        protected int _status;
        protected int _errorCode;

        public CardLog_CheckError():base(Lib.AppFunctions.CARDLOG_CHECKERROR)
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
                    _fromDate = Converter.ToNullableDateTime(GetParamter("fromdate"));
                    _toDate = Converter.ToNullableDateTime(GetParamter("todate"));
                    _type = Converter.ToString(GetParamter("type"));
                    _status = Converter.ToInt(GetParamter("status"));
                    _errorCode = Converter.ToInt(GetParamter("errorCode"), -199);

                    if (_errorCode == 0)
                        _errorCode = -199;

                    //Nếu thời gian xem không được chỉ định thì chỉ lấy ngày hiện tại
                    if (_fromDate == null) _fromDate = DateTime.Today;
                    if (_toDate == null) _toDate = DateTime.Today;

                    txtFromDate.Text = Converter.ToShortDateString(_fromDate);
                    txtToDate.Text = Converter.ToShortDateString(_toDate);
                    cmbType.SelectedValue = _type;

                    ViewError();
                }
                else
                {
                    _fromDate = Converter.ToNullableDateTime(txtFromDate.Text);
                    _toDate = Converter.ToNullableDateTime(txtToDate.Text);
                    _type = cmbType.SelectedValue;
                    _status = Converter.ToInt(txtStatus.Text, -2);
                    _errorCode = Converter.ToInt(txtErrorCode.Text, -199);
                    if (_errorCode == 0)
                        _errorCode = -199;

                    ViewError();
                }
            }
        }

        private void ViewError()
        {
            try
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
                        UIHelpers.CreateTableCell("Số tiền",Unit.Percentage(8),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Ghi chú",Unit.Percentage(15),HorizontalAlign.Left,"cellHeader")
                    }
                );
                table.Rows.Add(rowHeader);

                using (DataTable dt = WebDB.CardLog_SelectError(_fromDate, _toDate, _type,_status, _errorCode))
                {
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        TableRow rowEmpty = new TableRow();
                        rowEmpty.Cells.Add(UIHelpers.CreateTableCell("<p>Không có dữ liệu</p>", HorizontalAlign.Center, "cell1", 11));
                        table.Rows.Add(rowEmpty);
                    }
                    else
                    {
                        string css;
                        int stt = 0;
                        string ghichu;

                        string returnURL = Server.UrlEncode(string.Format("CardLog_CheckError.aspx?fromdate={0}&todate={1}&type={2}&errorcode={3}",
                                                                        _fromDate, _toDate, _type, _errorCode));
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
                                    UIHelpers.CreateTableCell(string.Format("{0} - {1}",dr["Serial"], dr[Lib.Meta.CARDLOG_PINNUMBER]),
                                                              HorizontalAlign.Left, css),
                                    UIHelpers.CreateTableCell(dr[Lib.Meta.CARDLOG_TYPE].ToString(),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(dr[Lib.Meta.CARDLOG_SERVER].ToString(),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(dr[Lib.Meta.CARDLOG_STATUS].ToString(),HorizontalAlign.Center,css),
                                    UIHelpers.CreateTableCell(dr[Lib.Meta.CARDLOG_ERRORCODE].ToString(), HorizontalAlign.Center, css),
                                    UIHelpers.CreateTableCell(string.Format("{0:N0}",dr[Lib.Meta.CARDLOG_AMOUNT]),
                                                              HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(ghichu,HorizontalAlign.Left,css)
                                }
                            );
                            table.Rows.Add(row);
                        }
                    }
                }
                this.panelList.Controls.Clear();
                this.panelList.Controls.Add(table);
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
