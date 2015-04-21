using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IDAdmin.Lib.UI;
using IDAdmin.Lib.Utils;
using System.Text;

namespace IDAdmin.Pages
{
    public partial class Statistic_ActiveCode : Lib.UI.BasePage
    {
        public Statistic_ActiveCode()
            : base(Lib.AppFunctions.VIEWACTIVECODE)
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
            else if (AppManager.GameID == "")
            {
                Response.Redirect("Index.aspx", false);
            }
            else
            {
                buttonExecute.Click += new EventHandler(buttonExecute_Click);
            }
        }

        private void ListActiveCode()
        {
            try
            {
                string strSerial = txtSerial.Text.Trim();
                string strAccount = txtAccount.Text.Trim();
                if ((strSerial != "") || (strAccount != ""))
                {
                    Table table = new Table();
                    TableRow rowHeader = new TableRow();
                    table.CssClass = "table1";
                    table.CellSpacing = 1;
                    rowHeader.Cells.AddRange
                    (
                        new TableCell[]
                    {
                        UIHelpers.CreateTableCell("Serial", Unit.Percentage(15), HorizontalAlign.Left, "cellHeader"),
                        UIHelpers.CreateTableCell("Type", Unit.Percentage(10), HorizontalAlign.Left, "cellHeader"),
                        UIHelpers.CreateTableCell("Kind", Unit.Percentage(10),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Turn", Unit.Percentage(10),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Price", Unit.Percentage(10),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Status", Unit.Percentage(10), HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Time", Unit.Percentage(10), HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Account", Unit.Percentage(15),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("UsedAccount", Unit.Percentage(15),HorizontalAlign.Left,"cellHeader")
                    }
                    );
                    table.Rows.Add(rowHeader);
                    using (DataTable dt = Lib.DataLayer.WebDB.GetActiveCodeBySerial(strSerial, strAccount))
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            TableRow row = new TableRow();
                            string strStatus = "";
                            switch (Converter.ToInt(dr["Status"]))
                            {
                                case 1:
                                    strStatus = "Chưa bán";
                                    break;
                                case 2:
                                    strStatus = "Đã bán";
                                    break;
                                case 3:
                                    strStatus = "Đã sử dụng";
                                    break;
                            }
                            row.Cells.AddRange
                            (
                                new TableCell[]
                            {
                                UIHelpers.CreateTableCell(Converter.ToString(dr["Serial"]),HorizontalAlign.Left,"cell1"),
                                UIHelpers.CreateTableCell(Converter.ToString(dr["Type"]),HorizontalAlign.Left,"cell1"),
                                UIHelpers.CreateTableCell(Converter.ToString(dr["Kind"]),HorizontalAlign.Left,"cell1"),
                                UIHelpers.CreateTableCell(Converter.ToString(dr["Turn"]),HorizontalAlign.Left,"cell1"),
                                UIHelpers.CreateTableCell(string.Format("{0:N0}",dr["Price"]),HorizontalAlign.Right,"cell1"),
                                UIHelpers.CreateTableCell(strStatus,HorizontalAlign.Left,"cell1"),
                                UIHelpers.CreateTableCell(Converter.ToString(dr["Updated"]),HorizontalAlign.Left,"cell1"),
                                UIHelpers.CreateTableCell(Converter.ToString(dr["Account"]),HorizontalAlign.Left,"cell1"),
                                UIHelpers.CreateTableCell(Converter.ToString(dr["UsedAccount"]),HorizontalAlign.Left,"cell1")
                            }
                            );
                            table.Rows.Add(row);
                        }
                    }
                    this.panelList.Controls.Clear();
                    if (table.Rows.Count > 1)
                    {
                        this.panelList.Controls.Add(table);
                    }
                    else
                    {
                        this.panelList.Controls.Add(new LiteralControl("<b>Không tìm thấy</b>"));
                    }
                }
            }
            catch (Exception ex)
            {
                Label labelMessage = new Label();
                labelMessage.Text = ex.Message;
                this.panelList.Controls.Add(labelMessage);
            }
        }

        protected void buttonExecute_Click(object sender, EventArgs e)
        {
            ListActiveCode();
        }
    }
}
