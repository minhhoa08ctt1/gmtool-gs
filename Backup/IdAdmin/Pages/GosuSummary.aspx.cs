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
    public partial class GosuSummary : Lib.UI.BasePage
    {
        public GosuSummary()
            : base(Lib.AppFunctions.GOSU_SUMMARY)
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
                labelCountOfGosu.Text = string.Format("{0:N0}", Lib.DataLayer.WebDB.Gosu_Total());
                this.buttonExecute.Click += new EventHandler(buttonExecute_Click);
                this.buttonExportToExcel.Click += new EventHandler(buttonExportToExcel_Click);
            }
        }

        protected void buttonExportToExcel_Click(object sender, EventArgs e)
        {
            Lib.DataExporter.ExportTable(ListGosuSummary(),
                                        IDAdmin.Lib.ExportFormat.Excel,
                                        string.Format("Gosu_{0:dd/MM/yyyy}.xls", DateTime.Today));
        }

        protected void buttonExecute_Click(object sender, EventArgs e)
        {
            try
            {
                this.panelList.Controls.Clear();
                this.panelList.Controls.Add(ListGosuSummary());
            }
            catch
            { }
        }

        private Table ListGosuSummary()
        {
            try
            {
                DateTime _startDate = Converter.ToDateTime(txtFromDate.Text, DateTime.Today);
                DateTime _endDate = Converter.ToDateTime(txtToDate.Text, DateTime.Today);
                Table table = new Table();
                table.CssClass = "table1";
                table.CellSpacing = 1;
                TableHeaderRow rowHeader = new TableHeaderRow();
                rowHeader.Cells.AddRange
                (
                    new TableCell[]
                    {     
                        UIHelpers.CreateTableCell("STT",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Ngày thống kê",Unit.Percentage(10),HorizontalAlign.Center, "cellHeader"),                        
                        UIHelpers.CreateTableCell("Số gosu đã nạp",Unit.Percentage(15),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Số gosu đã sử dụng",Unit.Percentage(15),HorizontalAlign.Center,"cellHeader")
                    }
                );
                table.Rows.Add(rowHeader);
                using (DataTable dt = Lib.DataLayer.WebDB.Gosu_Summary(_startDate, _endDate))
                {
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        TableRow rowEmpty = new TableRow();
                        rowEmpty.Cells.Add(UIHelpers.CreateTableCell("<p>Không có dữ liệu</p>", HorizontalAlign.Center, "cell1", 9));
                        table.Rows.Add(rowEmpty);
                    }
                    else
                    {
                        string css;
                        int stt = 0;
                        List<long> sumArr = new List<long>();
                        sumArr.Add(0);
                        sumArr.Add(0);
                        foreach (DataRow dr in dt.Rows)
                        {
                            stt += 1;
                            css = stt % 2 == 0 ? "cell1" : "cell2";
                            sumArr[0] += Converter.ToLong(dr["Recharge"]);
                            if (!string.IsNullOrEmpty(dr["Spend"].ToString()))
                            {
                                sumArr[1] += Converter.ToLong(dr["Spend"]);
                            }
                            TableRow row = new TableRow();
                            row.Cells.AddRange
                            (
                                new TableCell[]
                                {
                                    UIHelpers.CreateTableCell(stt.ToString(),HorizontalAlign.Center,css),
                                    UIHelpers.CreateTableCell(string.Format("{0:dd/MM/yyyy}",dr["Created"]), HorizontalAlign.Left, css),
                                    UIHelpers.CreateTableCell(string.Format("{0:N0}", dr["Recharge"]), HorizontalAlign.Right,css),
                                    UIHelpers.CreateTableCell(string.Format("{0:N0}", dr["Spend"]), HorizontalAlign.Right, css),
                                }
                            );
                            table.Rows.Add(row);
                        }
                        stt += 1;
                        css = stt % 2 == 0 ? "cell1" : "cell2";
                        TableRow rowSum = new TableRow();
                        rowSum.Cells.AddRange
                            (
                                new TableCell[]
                                {
                                    UIHelpers.CreateTableCell("<b>TỔNG:</b>",HorizontalAlign.Center,"cellTitle"),
                                    UIHelpers.CreateTableCell("", HorizontalAlign.Left, "cellTitle"),
                                    UIHelpers.CreateTableCell(string.Format("{0:N0}", sumArr[0]), HorizontalAlign.Right,"cellTitle"),
                                    UIHelpers.CreateTableCell(string.Format("{0:N0}", sumArr[1]), HorizontalAlign.Right, "cellTitle")
                                }
                            );
                        table.Rows.Add(rowSum);
                    }
                }
                return table;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

