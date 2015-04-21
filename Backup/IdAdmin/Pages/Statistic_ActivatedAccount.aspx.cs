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
    public partial class Statistic_ActivatedAccount : Lib.UI.BasePage
    {
        public Statistic_ActivatedAccount()
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
                                        string.Format("{0}_ActivatedUser_{1:dd/MM/yyyy}.xls", AppManager.GameID, DateTime.Today));
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
            string strKind = ddlKind.SelectedValue;
            Table table = new Table();
            table.CssClass = "table1";
            table.CellSpacing = 1;
            TableRow rowHeader = new TableRow();
            rowHeader.Cells.AddRange
            (
                new TableCell[]
                {     
                    UIHelpers.CreateTableCell("STT",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("UserName",Unit.Percentage(10),HorizontalAlign.Left, "cellHeader"),  
                    UIHelpers.CreateTableCell("ClientIP",Unit.Percentage(6),HorizontalAlign.Center, "cellHeader"),  
                    UIHelpers.CreateTableCell("RegisterType",Unit.Percentage(6),HorizontalAlign.Left, "cellHeader"),
                    UIHelpers.CreateTableCell("Created",Unit.Percentage(8),HorizontalAlign.Left, "cellHeader"),
                    UIHelpers.CreateTableCell("AdditionalData",Unit.Percentage(25),HorizontalAlign.Left, "cellHeader")
                }
            );
            table.Rows.Add(rowHeader);
            try
            {
                using (DataTable dt = Lib.DataLayer.WebDB.Statistics_ActivatedUser(AppManager.GameID, _startDate, _endDate, strKind))
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
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["UserName"]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["ClientIP"]), HorizontalAlign.Left, css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["RegisterType"]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(string.Format("{0:dd/MM/yyyy HH:mm:ss}",dr["Created"]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["AdditionalData"]),HorizontalAlign.Left,css)
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
