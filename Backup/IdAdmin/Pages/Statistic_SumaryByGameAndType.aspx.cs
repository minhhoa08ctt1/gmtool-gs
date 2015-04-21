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
    public partial class Statistic_SumaryByGameAndType : Lib.UI.BasePage
    {
        private List<string> chanel = new List<string>();
        public Statistic_SumaryByGameAndType()
            : base(Lib.AppFunctions.STATISTIC_SUMARYBYGAMEANDTYPE)
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
                chanel.Add("fpt");
                chanel.Add("gata");
                chanel.Add("nct");
                chanel.Add("soha");
                chanel.Add("tcv");
                chanel.Add("tik");
                chanel.Add("vtc");
                chanel.Add("zing");
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
            
            DateTime? _startDate = Converter.ToNullableDateTime(txtFromDate.Text);
            DateTime? _endDate = Converter.ToNullableDateTime(txtToDate.Text);
            if (_startDate == null || _endDate == null)
            {
                panelList.Controls.Clear();
                Label lblMessage = new Label();
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Thời gian thống kê không hợp lệ!";
                panelList.Controls.Add(lblMessage);
                return new Table();
            }

            Table table = new Table();
            table.CssClass = "table1";
            table.CellSpacing = 1;
                        
            using (DataTable dt = WebDB.Statistic_SumaryByGameAndType(_startDate, _endDate))
            {
                if (dt != null)
                {
                    int columnCount = dt.Columns.Count;
                    List<long> sumArr = new List<long>();
                    List<long> sumArrChanel = new List<long>();
                    long sumChanel = 0;
                    TableHeaderRow rowHeader = new TableHeaderRow();
                    for (int i = 0; i < columnCount; i++)
                    {
                        rowHeader.Cells.Add(UIHelpers.CreateTableCell(dt.Columns[i].ColumnName, HorizontalAlign.Center, "cellHeader"));
                        sumArr.Add(0);
                        sumArrChanel.Add(0);
                    }
                    table.Rows.Add(rowHeader);

                    string css = "";
                    foreach (DataRow dr in dt.Rows)
                    {
                        css = (css == "cell2") ? "cell1" : "cell2";
                        TableRow row = new TableRow();
                        for (int i = 0; i < columnCount; i++)
                        { 
                            if (i == 0)
                            {
                                row.Cells.Add(UIHelpers.CreateTableCell(Converter.ToString(dr[i]),HorizontalAlign.Left,css));
                            }
                            else
                            {
                                row.Cells.Add(UIHelpers.CreateTableCell(string.Format(numberFormatString, dr[i]), HorizontalAlign.Left, css));
                                sumArr[i] += Converter.ToLong(dr[i]);
                                if (chanel.Contains(dr[0].ToString().ToLower()))
                                {
                                    sumArrChanel[i] += Converter.ToLong(dr[i]);
                                    if (i < columnCount - 1)
                                    {
                                        sumChanel += Converter.ToLong(dr[i]);
                                    }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }

                    TableRow rowSumChanelling = new TableRow();
                    rowSumChanelling.Cells.Add(UIHelpers.CreateTableCell("<b>TỔNG CHANNELING:</b>", HorizontalAlign.Left, "cellTitle"));
                    for (int i = 1; i < sumArrChanel.Count; i++)
                    {
                        rowSumChanelling.Cells.Add(UIHelpers.CreateTableCell(string.Format(numberFormatString, sumArrChanel[i]), HorizontalAlign.Left, "cellTitle"));
                    }
                    table.Rows.Add(rowSumChanelling);

                    TableRow rowSum = new TableRow();
                    rowSum.Cells.Add(UIHelpers.CreateTableCell("<b>TỔNG:</b>", HorizontalAlign.Left, "cellTitle"));
                    for (int i = 1; i < sumArr.Count; i++)
                    {
                        rowSum.Cells.Add(UIHelpers.CreateTableCell(string.Format(numberFormatString, sumArr[i]), HorizontalAlign.Left, "cellTitle"));
                    }
                    table.Rows.Add(rowSum);
                    lblChanel.Text = "Tổng Channeling: " + string.Format("{0:N0}", sumChanel);
                }            
            }

            return table;
        }

    }
}
