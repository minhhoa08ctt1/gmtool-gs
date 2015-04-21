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
    public partial class ViewAdminLog : Lib.UI.BasePage
    {
        protected string _userName = "";
        protected int _page;

        public ViewAdminLog()
            : base(Lib.AppFunctions.VIEWADMINLOG)
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
                _userName = GetParamter("username").Trim();
                _page = Converter.ToInt(GetParamter("page"));
                if (_page == 0)
                    _page = 1;

                ListGrantedUser();
                ViewLog();
            }
        }

        private void ListGrantedUser()
        {
            try
            {
                string s = "";
                using (DataTable dt = WebDB.Function_SelectGrantedUser())
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        s += string.Format("<a href='ViewAdminLog.aspx?username={0}&page=1'>{0}</a> | ", dr["UserName"]);
                    }
                }                
                cellGrantedUser.Text = s;
            }
            catch (Exception ex)
            {
                cellGrantedUser.Text = "Error: " + ex.Message;
            }
        }

        private void ViewLog()
        {
            try
            {
                Table table = new Table();
                table.CssClass = "table1";
                table.CellSpacing = 1;

                TableHeaderRow rowHeader = new TableHeaderRow();
                rowHeader.Cells.AddRange
                (
                    new TableCell[]
                    {   
                        UIHelpers.CreateTableCell("Log Time",Unit.Percentage(15),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("User Name",Unit.Percentage(20),HorizontalAlign.Left, "cellHeader"),                        
                        UIHelpers.CreateTableCell("IP Address",Unit.Percentage(15),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Log Action",Unit.Percentage(50),HorizontalAlign.Left,"cellHeader")
                    }
                );
                table.Rows.Add(rowHeader);

                using (DataTable dt = WebDB.SelectAdminLog(_userName, _page, 100))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        TableRow row = new TableRow();
                        row.Cells.AddRange
                        (
                            new TableCell[]
                            {   
                                UIHelpers.CreateTableCell(string.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["LogTime"]),HorizontalAlign.Left,"cell1"),
                                UIHelpers.CreateTableCell(dr["UserName"].ToString(),HorizontalAlign.Left,"cell1"),
                                UIHelpers.CreateTableCell(dr["IPAddress"].ToString(), HorizontalAlign.Left,"cell1"),
                                UIHelpers.CreateTableCell(dr["LogAction"].ToString(),HorizontalAlign.Left,"cell1"),
                            }
                        );
                        table.Rows.Add(row);
                    }
                }

                panelList.Controls.Clear();
                panelList.Controls.Add(table);

                linkPrev.NavigateUrl = string.Format("ViewAdminLog.aspx?username={0}&page={1}", _userName, _page - 1);
                linkNext.NavigateUrl = string.Format("ViewAdminLog.aspx?username={0}&page={1}", _userName, _page + 1);
            }
            catch (Exception ex)
            {
                Label labelMessage = new Label();
                labelMessage.Text = ex.Message;
                this.panelList.Controls.Clear();
                this.panelList.Controls.Add(labelMessage);
            }
        }
    }
}
