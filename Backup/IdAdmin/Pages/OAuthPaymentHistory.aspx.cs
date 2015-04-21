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
    public partial class OAuthPaymentHistory : Lib.UI.BasePage
    {
        protected int _page;
        protected DateTime? _fromDate;
        protected DateTime? _toDate;
        protected string _serviceID;
        protected string _searchValue;

        public OAuthPaymentHistory()
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
                if (!Page.IsPostBack)
                {
                    _page = Converter.ToInt(GetParamter("page"));
                    _fromDate = Converter.ToNullableDateTime(GetParamter("fromdate"));
                    _toDate = Converter.ToNullableDateTime(GetParamter("todate"));
                    
                    _serviceID = Converter.ToString(GetParamter("serviceid"));
                    _searchValue = Converter.ToString(GetParamter("searchvalue"));

                    //Nếu thời gian xem không được chỉ định thì chỉ lấy ngày hiện tại
                    if (_fromDate == null) _fromDate = DateTime.Today;
                    if (_toDate == null) _toDate = DateTime.Today;

                    txtFromDate.Text = Converter.ToShortDateString(_fromDate);
                    txtToDate.Text = Converter.ToShortDateString(_toDate);
                    txtServiceID.Text = _serviceID;                    
                    txtSearchValue.Text = _searchValue;

                    if (_page <= 0) _page = 1;

                    ViewHistory();
                }
                else
                {
                    _page = 1;
                    _fromDate = Converter.ToNullableDateTime(txtFromDate.Text);
                    _toDate = Converter.ToNullableDateTime(txtToDate.Text);
                    _serviceID = txtServiceID.Text; 
                    _searchValue = txtSearchValue.Text.Trim();

                    ViewHistory();
                }
            }
        }

        protected void ViewHistory()
        {
            try
            {
                string linkFormat = "OAuthPaymentHistory.aspx?page={0}&fromdate={1:dd/MM/yyyy}&todate={2:dd/MM/yyyy}&serviceid={3}&searchvalue={4}";

                Table table = new Table();
                table.CssClass = "table1";
                table.CellSpacing = 1;

                TableHeaderRow rowHeader = new TableHeaderRow();
                rowHeader.Cells.AddRange
                (
                    new TableCell[]
                    {   
                        UIHelpers.CreateTableCell("Mã đơn hàng",Unit.Percentage(10),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Thời điểm",Unit.Percentage(13),HorizontalAlign.Center, "cellHeader"),                                                                        
                        UIHelpers.CreateTableCell("Nội dung",Unit.Percentage(12),HorizontalAlign.Center,"cellHeader"),                        
                        UIHelpers.CreateTableCell("Số GOSU",Unit.Percentage(7),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Hình thức",Unit.Percentage(8),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Mã dịch vụ",Unit.Percentage(25),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Khách hàng",Unit.Percentage(10),HorizontalAlign.Center,"cellHeader"),                        
                        UIHelpers.CreateTableCell("Kết quả giao dịch",Unit.Percentage(15),HorizontalAlign.Center,"cellHeader")
                    }
                );
                table.Rows.Add(rowHeader);

                using (DataTable dt = WebDB.OAuthPaymentLog_Select(_fromDate, _toDate, _serviceID, _searchValue , _page, _PageSize))
                {
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        TableRow rowEmpty = new TableRow();
                        rowEmpty.Cells.Add(UIHelpers.CreateTableCell("<p>Không có dữ liệu</p>", HorizontalAlign.Center, "cell1", 8));
                        table.Rows.Add(rowEmpty);
                    }
                    else
                    {
                        string css = "cell1";
                        string returnURL = Server.UrlEncode(string.Format(linkFormat, _page, _fromDate, _toDate, _serviceID, _searchValue));
                        foreach (DataRow dr in dt.Rows)
                        {
                            css = css == "cell2" ? "cell1" : "cell2";
                            TableRow row = new TableRow();
                            row.Cells.AddRange
                            (
                                new TableCell[]
                                {
                                    UIHelpers.CreateTableCell(string.Format("<a href='OAuthPaymentHistory_Details.aspx?id={0}&returnURL={1}'><b>{2}</b></a>", dr["LogID"], returnURL, dr["OrderID"]),HorizontalAlign.Left,css),                                    
                                    UIHelpers.CreateTableCell(string.Format("{0:dd/MM/yyyy HH:mm:ss}",dr["Created"]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(dr["OrderInfo"].ToString(),HorizontalAlign.Left,css),                                    
                                    UIHelpers.CreateTableCell(string.Format("{0:N0}", dr["Amount"]), HorizontalAlign.Left, css),
                                    UIHelpers.CreateTableCell(dr["PayMethod"].ToString(),HorizontalAlign.Left,css),                     
                                    UIHelpers.CreateTableCell(dr["ServiceID"].ToString(),HorizontalAlign.Left,css),      
                                    UIHelpers.CreateTableCell(dr["UserName"].ToString(),HorizontalAlign.Left,css),                                          
                                    UIHelpers.CreateTableCell(dr["ResponseMsg"].ToString(),HorizontalAlign.Left,css)
                                }
                            );
                            table.Rows.Add(row);
                        }
                    }
                }
                this.panelList.Controls.Clear();
                this.panelList.Controls.Add(table);


                this.linkFirst.NavigateUrl = string.Format(linkFormat, 1, _fromDate, _toDate, _serviceID, _searchValue);
                this.linkPrev.NavigateUrl = string.Format(linkFormat, _page > 0 ? _page - 1 : 1, _fromDate, _toDate, _serviceID, _searchValue);
                this.linkNext.NavigateUrl = string.Format(linkFormat, _page + 1, _fromDate, _toDate, _serviceID, _searchValue);
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
