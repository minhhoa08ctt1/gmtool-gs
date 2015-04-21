using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IDAdmin.Lib.DataLayer;
using IDAdmin.Lib.UI;
using IDAdmin.Lib.Utils;
using IDAdmin.Lib;

namespace IDAdmin.Pages
{
    public partial class Statistics02 : Lib.UI.BasePage
    {
        public Statistics02()
            : base(Lib.AppFunctions.STATISTIC_USER)
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
                WebUser _User = new WebUser();
                _User.GetInfo();
                if (_User.Level < 1000)
                {
                    Response.Redirect("Index.aspx", false);
                }
            }
            this.labelCountOfUser.Text = string.Format("{0:N0}", WebDB.Statistics_UserAccount(AppManager.GameID));
            this.buttonExecute.Click += new EventHandler(buttonExecute_Click);
            this.buttonExportToExcel.Click += new EventHandler(buttonExportToExcel_Click);
        }

        protected void buttonExportToExcel_Click(object sender, EventArgs e)
        {
            Lib.DataExporter.ExportTable(GetSumaryTable(),
                                        IDAdmin.Lib.ExportFormat.Excel,
                                        string.Format("NgaoKiem_{0:dd/MM/yyyy}.xls", DateTime.Today));
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

            Table table = new Table();
            table.CssClass = "table1";
            table.CellSpacing = 1;

            TableRow rowHeader = new TableRow();
            rowHeader.Cells.AddRange
            (
                new TableCell[]
                {     
                    UIHelpers.CreateTableCell("STT",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Ngày thống kê",Unit.Percentage(20),HorizontalAlign.Left, "cellHeader"),  
                    UIHelpers.CreateTableCell("Số TK đăng ký trong ngày",Unit.Percentage(20),HorizontalAlign.Center, "cellHeader"),  
                    UIHelpers.CreateTableCell("&nbsp;",Unit.Percentage(55),HorizontalAlign.Left, "cellHeader"),  
                }
            );
            table.Rows.Add(rowHeader);

            try
            {
                using (DataTable dt = Lib.DataLayer.WebDB.Statistics_UserAccount(AppManager.GameID, _startDate, _endDate))
                {
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        TableRow rowEmpty = new TableRow();
                        rowEmpty.Cells.Add(UIHelpers.CreateTableCell("<p>Không có dữ liệu!</p>", HorizontalAlign.Center, "cell1", 4));
                        table.Rows.Add(rowEmpty);
                    }
                    else
                    {
                        string css;
                        int stt = 0;
                        long sum = 0;

                        foreach (DataRow dr in dt.Rows)
                        {
                            stt += 1;
                            css = stt % 2 == 0 ? "cell1" : "cell2";
                            sum += Converter.ToLong(dr["CountOfRegDate"], 0);

                            TableRow row = new TableRow();
                            row.Cells.AddRange
                            (
                                new TableCell[]
                                {
                                    UIHelpers.CreateTableCell(stt.ToString(),HorizontalAlign.Center,css),
                                    UIHelpers.CreateTableCell(string.Format("{0:dd/MM/yyyy}",dr["RegisterDate"]), HorizontalAlign.Left, css),
                                    UIHelpers.CreateTableCell(string.Format("{0:N0}",dr["CountOfRegDate"]),HorizontalAlign.Center,css),
                                    UIHelpers.CreateTableCell("", HorizontalAlign.Left,css)                                    
                                }
                            );
                            table.Rows.Add(row);
                        }

                        TableRow rowSum = new TableRow();
                        rowSum.Cells.AddRange
                        (
                            new TableCell[]
                            {
                                UIHelpers.CreateTableCell("<b>TỔNG CỘNG:</b>", HorizontalAlign.Right,"cellTitle",2),
                                UIHelpers.CreateTableCell(string.Format("{0:N0}",sum),HorizontalAlign.Center,"cellTitle"),
                                UIHelpers.CreateTableCell("", HorizontalAlign.Left,"cellTitle") 
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
