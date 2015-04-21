using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using IDAdmin.Lib.DataLayer;
using IDAdmin.Lib.UI;
using IDAdmin.Lib.Utils;


namespace IDAdmin.Pages
{
    public partial class Statistic_SumaryWalletRecharge : Lib.UI.BasePage
    {
        public Statistic_SumaryWalletRecharge()
            : base(Lib.AppFunctions.STATISTIC_WALLETRECHARGE)
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
                }
                this.buttonExecute.Click += new EventHandler(buttonExecute_Click);
                this.buttonExportToExcel.Click += new EventHandler(buttonExportToExcel_Click);
            }
        }

        protected void buttonExportToExcel_Click(object sender, EventArgs e)
        {
            Lib.DataExporter.ExportTable(GetSumaryTable("excel"),
                                        IDAdmin.Lib.ExportFormat.Excel,
                                        string.Format("TK_{0:dd/MM/yyyy}.xls", DateTime.Today));
        }

        protected void buttonExecute_Click(object sender, EventArgs e)
        {
            try
            {
                this.panelList.Controls.Clear();
                this.panelList.Controls.Add(GetSumaryTable("html"));
            }
            catch
            { }
        }

        private Table GetSumaryTable(string displayType)
        {
            string numberFormatString = "";
            if (displayType == "excel")
            {
                numberFormatString = "{0}";
            }
            else
            {
                numberFormatString = "{0:N0}";
            }

            DateTime _startDate = Converter.ToDateTime(txtFromDate.Text, DateTime.Today);
            DateTime _endDate = Converter.ToDateTime(txtToDate.Text, DateTime.Today);

            Table table = new Table();
            table.CssClass = "table1";
            table.CellSpacing = 1;

            TableHeaderRow rowHeader = new TableHeaderRow();
            rowHeader.Cells.AddRange(new TableCell[]
            {
                UIHelpers.CreateTableCell("Phân loại",HorizontalAlign.Left,"cellHeader"),
                UIHelpers.CreateTableCell("Tổng số tiền", HorizontalAlign.Left, "cellHeader"),
                UIHelpers.CreateTableCell("Tổng GOSU", HorizontalAlign.Left, "cellHeader"),
                UIHelpers.CreateTableCell("Tổng GOSU tặng", HorizontalAlign.Left, "cellHeader")
            });
            table.Rows.Add(rowHeader);

            using (DataTable dt = WebDB.Statistic_SumaryWalletRecharge(_startDate, _endDate))
            {
                if (dt != null)
                {
                    long sumAmount = 0;
                    long sumGOSU = 0;
                    long sumPromotion = 0;
                    string css = "";
                    foreach (DataRow dr in dt.Rows)
                    {
                        css = (css == "cell2") ? "cell1" : "cell2";
                        TableRow row = new TableRow();
                        row.Cells.AddRange(new TableCell[]
                        {
                            UIHelpers.CreateTableCell(Converter.ToString(dr["Type"]), HorizontalAlign.Left, css),
                            UIHelpers.CreateTableCell(string.Format(numberFormatString, dr["SumOfAmount"]), HorizontalAlign.Left, css),
                            UIHelpers.CreateTableCell(string.Format(numberFormatString, dr["SumOfGOSU"]), HorizontalAlign.Left, css),
                            UIHelpers.CreateTableCell(string.Format(numberFormatString, dr["SumOfPromotion"]), HorizontalAlign.Left,css)
                        });
                        sumAmount += Converter.ToLong(dr["SumOfAmount"]);
                        sumGOSU += Converter.ToLong(dr["SumOfGOSU"]);
                        sumPromotion += Converter.ToLong(dr["SumOfPromotion"]);

                        table.Rows.Add(row);
                    }

                    TableRow rowSum = new TableRow();
                    rowSum.Cells.AddRange(new TableCell[]
                    {
                        UIHelpers.CreateTableCell("<b>TỔNG:</b>", HorizontalAlign.Left, "cellTitle"),
                        UIHelpers.CreateTableCell(string.Format(numberFormatString, sumAmount), HorizontalAlign.Left, "cellTitle"),
                        UIHelpers.CreateTableCell(string.Format(numberFormatString, sumGOSU), HorizontalAlign.Left, "cellTitle"),
                        UIHelpers.CreateTableCell(string.Format(numberFormatString, sumPromotion), HorizontalAlign.Left, "cellTitle")                        
                    });
                    table.Rows.Add(rowSum);
                }
            }

            return table;
        }
    }
}
