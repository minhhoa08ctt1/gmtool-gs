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
    public partial class Statistic_Sumary : Lib.UI.BasePage
    {
        public Statistic_Sumary()
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
            catch
            { }
        }

        private Table GetSumaryTable()
        {
            DateTime _startDate = Converter.ToDateTime(txtFromDate.Text, DateTime.Today);
            DateTime _endDate = Converter.ToDateTime(txtToDate.Text, DateTime.Today);
            int _timeLine = Converter.ToInt(cmbTimeLine.SelectedValue);
            string _server = cmbServer.SelectedValue;
            string _type = cmbType.SelectedValue;

            Table table = new Table();
            table.CssClass = "table1";
            table.CellSpacing = 1;

            TableHeaderRow rowHeader = new TableHeaderRow();
            rowHeader.Cells.AddRange
            (
                new TableCell[]
                {     
                    UIHelpers.CreateTableCell("STT",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Ngày thống kê",Unit.Percentage(15),HorizontalAlign.Left, "cellHeader"),  
                    UIHelpers.CreateTableCell("Tg tính bắt đầu",Unit.Percentage(20),HorizontalAlign.Left, "cellHeader"),  
                    UIHelpers.CreateTableCell("Tg tính kết thúc",Unit.Percentage(20),HorizontalAlign.Left, "cellHeader"),  
                    UIHelpers.CreateTableCell("Số lượt nạp thẻ",Unit.Percentage(15),HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Tổng số tiền",Unit.Percentage(25),HorizontalAlign.Center,"cellHeader")
                }
            );
            table.Rows.Add(rowHeader);

            try
            {
                using (DataTable dt = Lib.DataLayer.WebDB.Statistics_SumaryByDate(_startDate, _endDate, _timeLine, _server, _type))
                {
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        TableRow rowEmpty = new TableRow();
                        rowEmpty.Cells.Add(UIHelpers.CreateTableCell("<p>Không có dữ liệu!</p>", HorizontalAlign.Center, "cell1", 5));
                        table.Rows.Add(rowEmpty);
                    }
                    else
                    {
                        string css;
                        int stt = 0;
                        long tonggiaodich = 0;
                        long tongtien = 0;

                        foreach (DataRow dr in dt.Rows)
                        {
                            stt += 1;
                            css = stt % 2 == 0 ? "cell1" : "cell2";
                            tonggiaodich += Converter.ToLong(dr["CountOfTrans"]);
                            tongtien += Converter.ToLong(dr["SumOfAmount"]);
                            TableRow row = new TableRow();
                            row.Cells.AddRange
                            (
                                new TableCell[]
                                {
                                    UIHelpers.CreateTableCell(stt.ToString(),HorizontalAlign.Center,css),
                                    UIHelpers.CreateTableCell(string.Format("{0:dd/MM/yyyy}",dr["SumaryDate"]), HorizontalAlign.Left, css),
                                    UIHelpers.CreateTableCell(string.Format("{0:dd/MM/yyyy HH:mm:ss}",dr["FromTime"]), HorizontalAlign.Left, css),
                                    UIHelpers.CreateTableCell(string.Format("{0:dd/MM/yyyy HH:mm:ss}",dr["ToTime"]), HorizontalAlign.Left, css),
                                    UIHelpers.CreateTableCell(string.Format("{0:N0}",dr["CountOfTrans"]),HorizontalAlign.Center,css),
                                    UIHelpers.CreateTableCell(string.Format("{0:N0}",dr["SumOfAmount"]), HorizontalAlign.Right,css)                                    
                                }
                            );
                            table.Rows.Add(row);
                        }
                        TableRow rowSum = new TableRow();
                        rowSum.Cells.AddRange
                        (
                            new TableCell[]
                            {
                                UIHelpers.CreateTableCell("TỔNG CỘNG:",HorizontalAlign.Right,"cellTitle",4),
                                UIHelpers.CreateTableCell(string.Format("{0:N0}",tonggiaodich),HorizontalAlign.Center,"cellTitle"),
                                UIHelpers.CreateTableCell(string.Format("{0:N0}",tongtien), HorizontalAlign.Right, "cellTitle")
                            }
                        );
                        table.Rows.Add(rowSum);
                    }
                }
            }
            catch
            {
            }
            return table;
        }
    }
}
