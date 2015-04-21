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
    public partial class ChanellingPartner : Lib.UI.BasePage
    {
        public ChanellingPartner()
            : base(Lib.AppFunctions.CHANELLINGGAMSERVER)
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
            else if (AppManager.GameID == "")
            {
                Response.Redirect("Index.aspx", false);
            }
            else
            {
                Table table = new Table();
                table.Width = Unit.Percentage(100);
                TableRow rowHeader = new TableRow();
                rowHeader.Cells.AddRange(
                    new TableCell[]
                    {
                        UIHelpers.CreateTableCell("STT",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Đối tác chanelling",Unit.Percentage(30), HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Ghi chú",Unit.Percentage(55), HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("&nbsp;",Unit.Percentage(10), HorizontalAlign.Center,"cellHeader"),
                    }
                );
                table.Rows.Add(rowHeader);

                DataTable dt = WebDB.ChanellingPartner_SelectByGame(AppManager.GameID);
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    i = i + 1;
                    TableRow row = new TableRow();
                    row.Cells.AddRange(
                        new TableCell[]
                        {
                            UIHelpers.CreateTableCell(i.ToString(), HorizontalAlign.Center, "cell1"),
                            UIHelpers.CreateTableCell(dr["PartnerID"].ToString(), HorizontalAlign.Left, "cell1"),
                            UIHelpers.CreateTableCell(dr["Description"].ToString(), HorizontalAlign.Left, "cell1"),
                            UIHelpers.CreateTableCell(string.Format("<a href='ChanellingGameServer.aspx?partner={0}'>[Tiếp tục]</a>",
                                                                dr["PartnerID"]), 
                                                    HorizontalAlign.Left, "cell1"),
                        }
                    );
                    table.Rows.Add(row);
                }

                this.panelList.Controls.Add(table);
            }

        }
    }
}
