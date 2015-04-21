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
    public partial class Statistic_TopRecharge : Lib.UI.BasePage
    {
        protected int _page;
        protected DateTime? _fromDate;
        protected DateTime? _toDate;
        protected string _server;
        protected string _account;
        
        public Statistic_TopRecharge()
            : base(Lib.AppFunctions.STATISTIC_TOPRECHARGE)
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
                    this.cmbServer.DataValueField = Lib.Meta.SERVER_NAME;
                    this.cmbServer.DataTextField = Lib.Meta.SERVER_FULLNAME;
                    this.cmbServer.DataSource = WebDB.Server_Select(true);
                    this.cmbServer.DataBind();

                    _page = Converter.ToInt(GetParamter("page"));
                    _fromDate = Converter.ToNullableDateTime(GetParamter("fromdate"));
                    _toDate = Converter.ToNullableDateTime(GetParamter("todate"));
                    _server = Converter.ToString(GetParamter("server"));
                    _account = Converter.ToString(GetParamter("account"));

                    //Nếu thời gian xem không được chỉ định thì chỉ lấy ngày hiện tại
                    if (_fromDate == null) _fromDate = DateTime.Today;
                    if (_toDate == null) _toDate = DateTime.Today;

                    txtFromDate.Text = Converter.ToShortDateString(_fromDate);
                    txtToDate.Text = Converter.ToShortDateString(_toDate);
                    cmbServer.SelectedValue = _server;
                    txtAccount.Text = _account;

                    if (_page <= 0) _page = 1;

                    ViewSumary();

                }
                else
                {
                    _page = 1;
                    _fromDate = Converter.ToNullableDateTime(txtFromDate.Text);
                    _toDate = Converter.ToNullableDateTime(txtToDate.Text);
                    _server = cmbServer.SelectedValue;
                    _account = txtAccount.Text.Trim();

                    ViewSumary();
                }
            }
        }

        private void ViewSumary()
        {
            try
            {
                string linkFormat = "Statistic_TopRecharge.aspx?page={0}&fromdate={1:dd/MM/yyyy}&todate={2:dd/MM/yyyy}&server={3}&account={4}";

                Table table = new Table();
                table.CssClass = "table1";
                table.CellSpacing = 1;

                TableHeaderRow rowHeader = new TableHeaderRow();
                rowHeader.Cells.AddRange
                (
                    new TableCell[]
                    {   
                        UIHelpers.CreateTableCell("STT",Unit.Percentage(5),HorizontalAlign.Center, "cellHeader"),                                                
                        UIHelpers.CreateTableCell("Tài khoản nạp tiền",Unit.Percentage(15),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Họ tên",Unit.Percentage(20),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Điện thoại",Unit.Percentage(15),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Email",Unit.Percentage(20),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Số lần nạp tiền",Unit.Percentage(10),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Tổng số tiền nạp",Unit.Percentage(15),HorizontalAlign.Left,"cellHeader")
                    }
                );
                table.Rows.Add(rowHeader);

                using (DataTable dt = WebDB.Statistic_TopRecharge(_fromDate, _toDate, _server, _account, _page, 50))
                {
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        TableRow rowEmpty = new TableRow();
                        rowEmpty.Cells.Add(UIHelpers.CreateTableCell("<p>Không có dữ liệu</p>", HorizontalAlign.Center, "cell1", 7));
                        table.Rows.Add(rowEmpty);
                    }
                    else
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            TableRow row = new TableRow();
                            row.Cells.AddRange
                            (
                                new TableCell[]
                                {
                                    UIHelpers.CreateTableCell(dr["RowNumber"].ToString(),HorizontalAlign.Center,"cell1"),                                    
                                    UIHelpers.CreateTableCell(dr["Account"].ToString(),HorizontalAlign.Left,"cell1"),
                                    UIHelpers.CreateTableCell(dr["Name"].ToString(),HorizontalAlign.Left,"cell1"),
                                    UIHelpers.CreateTableCell(dr["PhoneNumber"].ToString(),HorizontalAlign.Left,"cell1"),
                                    UIHelpers.CreateTableCell(dr["Email"].ToString(),HorizontalAlign.Left,"cell1"),
                                    UIHelpers.CreateTableCell(dr["CountOfLog"].ToString(),HorizontalAlign.Center,"cell1"),
                                    UIHelpers.CreateTableCell(string.Format("{0:N0}", dr["SumOfAmount"]),HorizontalAlign.Left,"cell1")    
                                }
                            );
                            table.Rows.Add(row);
                        }
                    }
                }
                this.panelList.Controls.Clear();
                this.panelList.Controls.Add(table);

                this.linkPrev.NavigateUrl = string.Format(linkFormat, _page > 0 ? _page - 1 : 1, _fromDate, _toDate, _server, _account);
                this.linkNext.NavigateUrl = string.Format(linkFormat, _page + 1, _fromDate, _toDate, _server, _account);
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
