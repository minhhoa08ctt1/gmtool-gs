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
    public partial class Statistic_ByCommunity : Lib.UI.BasePage
    {
        public Statistic_ByCommunity()
            : base(Lib.AppFunctions.STATISTIC_SUMARY)
        { }

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

                if (!Page.IsPostBack)
                {
                    this.cmbServer.DataValueField = Lib.Meta.SERVER_NAME;
                    this.cmbServer.DataTextField = Lib.Meta.SERVER_FULLNAME;
                    this.cmbServer.DataSource = WebDB.Server_Select(true);
                    this.cmbServer.DataBind();
                }
                this.buttonExecute.Click += new EventHandler(buttonExecute_Click);
                this.buttonExportToExcel.Click += new EventHandler(buttonExportToExcel_Click);
            }
        }

        protected void buttonExportToExcel_Click(object sender, EventArgs e)
        {
            Lib.DataExporter.ExportTable(GetSumaryTable(),
                                        IDAdmin.Lib.ExportFormat.Excel,
                                        string.Format("ThongKe_{0:dd/MM/yyyy}.xls", DateTime.Today));
        }

        protected void buttonExecute_Click(object sender, EventArgs e)
        {
            try
            {
                this.panelList.Controls.Clear();
                this.panelList.Controls.Add(GetSumaryTable());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Table GetSumaryTable()
        {
            DateTime _startDate = Converter.ToDateTime(txtFromDate.Text, DateTime.Today);
            DateTime _endDate = Converter.ToDateTime(txtToDate.Text, DateTime.Today);            
            string _server = cmbServer.SelectedValue;            

            Table table = new Table();
            table.CssClass = "table1";
            table.CellSpacing = 1;

            TableHeaderRow rowHeader = new TableHeaderRow();
            rowHeader.Cells.AddRange
            (
                new TableCell[]
                {     
                    UIHelpers.CreateTableCell("STT",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Cộng đồng",Unit.Percentage(35),HorizontalAlign.Left, "cellHeader"),  
                    UIHelpers.CreateTableCell("Số lượt nạp tiền",Unit.Percentage(15),HorizontalAlign.Left, "cellHeader"),  
                    UIHelpers.CreateTableCell("Số người nạp tiền",Unit.Percentage(15),HorizontalAlign.Left, "cellHeader"),  
                    UIHelpers.CreateTableCell("Tổng số tiền nạp",Unit.Percentage(15),HorizontalAlign.Left,"cellHeader"),
                    UIHelpers.CreateTableCell("Trung bình/khách hàng", Unit.Percentage(15), HorizontalAlign.Left, "cellHeader")
                }
            );
            table.Rows.Add(rowHeader);

            try
            {
                using (DataTable dt = Lib.DataLayer.WebDB.Statistic_SumaryByCommunity(_startDate, _endDate, _server))
                {
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        TableRow rowEmpty = new TableRow();
                        rowEmpty.Cells.Add(UIHelpers.CreateTableCell("<p>Không có dữ liệu!</p>", HorizontalAlign.Center, "cell1", 6));
                        table.Rows.Add(rowEmpty);
                    }
                    else
                    {
                        string css;
                        int stt = 0;
                        long tonggiaodich = 0;
                        long tongtien = 0;
                        long tongkhachhang = 0;

                        foreach (DataRow dr in dt.Rows)
                        {
                            stt += 1;
                            css = stt % 2 == 0 ? "cell1" : "cell2";

                            tonggiaodich += Converter.ToLong(dr["CountOfExchange"]);
                            tongkhachhang += Converter.ToLong(dr["CountOfUser"]);
                            tongtien += Converter.ToLong(dr["SumOfAmount"]);
                            TableRow row = new TableRow();
                            row.Cells.AddRange
                            (
                                new TableCell[]
                                {
                                    UIHelpers.CreateTableCell(stt.ToString(),HorizontalAlign.Center,css),
                                    UIHelpers.CreateTableCell(dr["Community"].ToString(),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(string.Format("{0:N0}",dr["CountOfExchange"]), HorizontalAlign.Left, css),
                                    UIHelpers.CreateTableCell(string.Format("{0:N0}",dr["CountOfUser"]), HorizontalAlign.Left, css),
                                    UIHelpers.CreateTableCell(string.Format("{0:N0}",dr["SumOfAmount"]), HorizontalAlign.Left, css),
                                    UIHelpers.CreateTableCell(string.Format("{0:N0}",Converter.ToLong(dr["CountOfUser"]) == 0 ? 0 : Converter.ToLong(dr["SumOfAmount"])/Converter.ToLong(dr["CountOfUser"])), 
                                                              HorizontalAlign.Left, css)

                                }
                            );
                            table.Rows.Add(row);
                        }
                        TableRow rowSum = new TableRow();
                        rowSum.Cells.AddRange
                        (
                            new TableCell[]
                            {
                                UIHelpers.CreateTableCell("TỔNG CỘNG:",HorizontalAlign.Right,"cellTitle",2),
                                UIHelpers.CreateTableCell(string.Format("{0:N0}",tonggiaodich),HorizontalAlign.Left,"cellTitle"),
                                UIHelpers.CreateTableCell(string.Format("{0:N0}",tongkhachhang),HorizontalAlign.Left,"cellTitle"),
                                UIHelpers.CreateTableCell(string.Format("{0:N0}",tongtien), HorizontalAlign.Left, "cellTitle"),
                                UIHelpers.CreateTableCell("&nbsp;", HorizontalAlign.Left, "cellTitle")
                            }
                        );
                        table.Rows.Add(rowSum);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return table;
        }
    }
}
