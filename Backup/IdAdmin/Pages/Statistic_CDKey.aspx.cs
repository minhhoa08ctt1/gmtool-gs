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
    public partial class Statistic_CDKey : Lib.UI.BasePage
    {
        public Statistic_CDKey()
            : base(Lib.AppFunctions.STATISTIC_ACTIVATEDACCOUNT)
        {
        }

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
            else if (AppManager.GameID == "")
            {
                Response.Redirect("Index.aspx", false);
            }
            else
            {
                this.buttonExecute.Click += new EventHandler(buttonExecute_Click);
                this.buttonExportToExcel.Click += new EventHandler(buttonExportToExcel_Click);
            }
        }

        protected void buttonExportToExcel_Click(object sender, EventArgs e)
        {
            Lib.DataExporter.ExportTable(GetSumaryTable(),
                                    IDAdmin.Lib.ExportFormat.Excel,
                                    string.Format("{0}_CDKey_{1:dd/MM/yyyy}.xls", AppManager.GameID, DateTime.Today));
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
            string strKind = ddlKind.SelectedValue == "All" ? "" : ddlKind.SelectedValue;
            Table table = new Table();
            table.CssClass = "table1";
            table.CellSpacing = 1;
            TableRow rowHeader = new TableRow();
            rowHeader.Cells.AddRange
            (
                new TableCell[]
                {     
                    UIHelpers.CreateTableCell("STT",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("UsedAccount",Unit.Percentage(15),HorizontalAlign.Left, "cellHeader"),
                    UIHelpers.CreateTableCell("TransactionNo",Unit.Percentage(15),HorizontalAlign.Left, "cellHeader"),  
                    UIHelpers.CreateTableCell("CDKey",Unit.Percentage(10),HorizontalAlign.Center, "cellHeader"),  
                    UIHelpers.CreateTableCell("Kind",Unit.Percentage(5),HorizontalAlign.Left, "cellHeader"),
                    UIHelpers.CreateTableCell("TeamID",Unit.Percentage(5),HorizontalAlign.Left, "cellHeader"),
                    UIHelpers.CreateTableCell("ServerID",Unit.Percentage(5),HorizontalAlign.Left, "cellHeader"),
                    UIHelpers.CreateTableCell("UsedTime",Unit.Percentage(15),HorizontalAlign.Left, "cellHeader")
                }
            );
            table.Rows.Add(rowHeader);
            try
            {
                using (DataTable dt = Lib.DataLayer.WebDB.Statistics_CDKey(_startDate, _endDate, strKind))
                {
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        TableRow rowEmpty = new TableRow();
                        rowEmpty.Cells.Add(UIHelpers.CreateTableCell("<p>Không có dữ liệu!</p>", HorizontalAlign.Center, "cell1", 4));
                        table.Rows.Add(rowEmpty);
                    }
                    else
                    {
                        labelCountOfUser.Text = string.Format("{0:N0}", dt.Rows.Count);
                        string css;
                        int stt = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            stt += 1;
                            css = stt % 2 == 0 ? "cell1" : "cell2";
                            TableRow row = new TableRow();
                            row.Cells.AddRange
                            (
                                new TableCell[]
                                {
                                    UIHelpers.CreateTableCell(stt.ToString(),HorizontalAlign.Center,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["UsedAccount"]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["TransactionNo"]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["CDKey"]), HorizontalAlign.Left, css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["Kind"]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["TeamID"]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["ServerID"]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(string.Format("{0:dd/MM/yyyy HH:mm:ss}",dr["UsedTime"]),HorizontalAlign.Left,css)
                                }
                            );
                            table.Rows.Add(row);
                        }
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
