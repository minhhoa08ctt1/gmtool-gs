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
    public partial class ManageUserRights : Lib.UI.BasePage
    {
        protected string _userName;
   
        public ManageUserRights()
            : base(Lib.AppFunctions.MANAGEUSERRIGHTS)
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
                ListGrantedUser();

                _userName = GetParamter("username").Trim();
                if (_userName != "")
                {
                    ListUserRights(_userName);
                }
                this.buttonExecute.Click += new EventHandler(buttonExecute_Click);                
            }
        }

        protected void buttonExecute_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageUserRights.aspx?username=" + txtUserName.Text.Trim());
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
                        s += string.Format("<a href='ManageUserRights.aspx?username={0}'>{0}</a> | ", dr["UserName"]);
                    }
                }
                cellGrantedUser.Text = s;
            }
            catch (Exception ex)
            {
                cellGrantedUser.Text = "Error: " + ex.Message;
            }
        }

        protected void ListUserRights(string username)
        {
            try
            {
                Table tableRights = new Table();
                tableRights.CssClass = "table1";
                tableRights.CellSpacing = 1;

                TableHeaderRow rowHeader = new TableHeaderRow();
                rowHeader.Cells.AddRange
                (
                    new TableCell[]
                    {   
                        UIHelpers.CreateTableCell("Cấp quyền",Unit.Percentage(10),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Chức năng",Unit.Percentage(20),HorizontalAlign.Left, "cellHeader"),                        
                        UIHelpers.CreateTableCell("Mô tả chức năng",Unit.Percentage(70),HorizontalAlign.Left,"cellHeader")                        
                    }
                );
                tableRights.Rows.Add(rowHeader);

                if (!WebDB.User_Exists(username))
                {
                    TableRow rowEmpty = new TableRow();
                    rowEmpty.Cells.Add(UIHelpers.CreateTableCell(string.Format("<p>Tài khoản {0} không tồn tại</p>",username), HorizontalAlign.Center, "cell1", 3));
                    tableRights.Rows.Add(rowEmpty);
                }
                else
                {
                    using (DataTable dt = Lib.DataLayer.WebDB.Function_SelectUserRight(username))
                    {
                        if (dt == null || dt.Rows.Count == 0)
                        {
                            TableRow rowEmpty = new TableRow();
                            rowEmpty.Cells.Add(UIHelpers.CreateTableCell("<p>Tài khoản không tồn tại</p>", HorizontalAlign.Center, "cell1", 3));
                            tableRights.Rows.Add(rowEmpty);
                        }
                        else
                        {
                            TableRow rowTitle = new TableRow();
                            rowTitle.Cells.Add(UIHelpers.CreateTableCell(string.Format("Tài khoản: {0}",username), HorizontalAlign.Center, "cellTitle", 3));
                            tableRights.Rows.Add(rowTitle);

                            string css = "cell1";
                            string returnURL = Server.UrlEncode(Request.Url.ToString());
                            foreach (DataRow dr in dt.Rows)
                            {
                                css = css == "cell1" ? "cell2" : "cell1";
                                TableRow row = new TableRow();
                                row.Cells.AddRange
                                (
                                    new TableCell[]
                                    {
                                        UIHelpers.CreateTableCell(string.Format("<a href='SetUserRight.aspx?f={0}&username={1}&allow={2}&returnURL={3}' {4}>{5}</a>",
                                                                                dr["FunctionName"].ToString().Trim(), 
                                                                                _userName, 
                                                                                !Converter.ToBoolean(dr["Allow"]),
                                                                                returnURL,
                                                                                UIHelpers.CreateConfirm(string.Format("{0} [{1}] đối với tài khoản: [{2}]?", 
                                                                                                        Converter.ToBoolean(dr["Allow"]) ? "Thu hồi quyền" : "Cấp quyền",
                                                                                                        dr["FunctionDesc"].ToString(), username)),
                                                                                Converter.ToBoolean(dr["Allow"]) ? "Thu hồi quyền" : "Cấp quyền"),
                                                                  HorizontalAlign.Center, css),
                                        UIHelpers.CreateTableCell(dr["FunctionName"].ToString(), HorizontalAlign.Left, css),
                                        UIHelpers.CreateTableCell(dr["FunctionDesc"].ToString(), HorizontalAlign.Left, css)
                                    }
                                );
                                tableRights.Rows.Add(row);
                            }
                        }
                    }
                }

                this.panelList.Controls.Clear();
                this.panelList.Controls.Add(tableRights);
            }
            catch (Exception ex)
            {
                Label labelMessage = new Label();
                labelMessage.Text = ex.Message;
                this.panelList.Controls.Add(labelMessage);
            }
        }
    }
}
