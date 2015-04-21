using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IDAdmin.Lib.UI;
using IDAdmin.Lib;
using IDAdmin.Lib.Utils;

namespace IDAdmin.Pages
{
    public partial class ServerList : Lib.UI.BasePage
    {
        public ServerList()
            : base(Lib.AppFunctions.SERVER_LIST)
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
                if (Request.HttpMethod.ToUpper() == "POST")
                {
                    string clickedButton = Request.Form.Get("button");

                    //Trường hợp nút Thiết lập trạng thái cho máy chủ được click
                    if (clickedButton == "set_status")
                    {
                        string status_value = Request.Form.Get("status_value");
                        int status;
                        if (status_value == "open") { status = 1; }
                        else if (status_value == "close") { status = 0; }
                        else { status = -1; }

                        if (status == 0 || status == 1)
                        {
                            string listEditServer = "";
                            int countEditServer = 0;
                            for (int i = 0; i < Request.Form.Count; i++)
                            {
                                string checkedServer = Request.Form.GetKey(i);
                                if (checkedServer.StartsWith("SERVER_"))
                                {
                                    int serverid = Converter.ToInt(checkedServer.Replace("SERVER_", ""), 0);
                                    if (serverid != 0)
                                    {
                                        Lib.DataLayer.WebDB.Server_UpdateStatus(serverid, AppManager.GameID, status);
                                        listEditServer += serverid.ToString() + " ";
                                        countEditServer += 1;
                                    }
                                }
                            }
                            if (countEditServer > 0)
                            {
                                Lib.DataLayer.WebDB.WriteLog(_User.UserName, Request.UserHostAddress,
                                                            string.Format("Game: {0}, Status = {1}, Servers: {2}", AppManager.GameID, status, listEditServer));
                            }
                        }
                    }
                    //Trường hợp nút tạo server được click (bỏ qua vì đã xử lý theo kiểu sự kiện của WebForm)
                    else if (clickedButton == "add_server")
                    {
                    }
                }

                //Hiển thị danh sách server
                ListServer();
                if (CheckRight(Lib.AppFunctions.SERVER_ADD) && AppManager.GameID != "")
                {
                    panelAdd.Visible = true;
                    buttonCreate.Click += new EventHandler(buttonCreate_Click);
                }
                else
                {
                    panelAdd.Visible = false;
                }
            }   
        }

        private void ListServer()
        {
            try
            {
                Table table = new Table();
                
                TableRow rowHeader = new TableRow();
                table.CssClass = "table1";
                table.CellSpacing = 1;
                
                rowHeader.Cells.AddRange
                (
                    new TableCell[]
                    {
                        UIHelpers.CreateTableCell("",Unit.Percentage(1), HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Server ID",Unit.Percentage(10),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Server Name", Unit.Percentage(10), HorizontalAlign.Left, "cellHeader"),
                        UIHelpers.CreateTableCell("Full Name", Unit.Percentage(25), HorizontalAlign.Left, "cellHeader"),
                        UIHelpers.CreateTableCell("Identiy Name", Unit.Percentage(10),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Created Time", Unit.Percentage(15),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Status", Unit.Percentage(10), HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Sort Order", Unit.Percentage(10),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Is Hot", Unit.Percentage(5),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("", Unit.Percentage(5),HorizontalAlign.Left,"cellHeader")
                    }
                );
                table.Rows.Add(rowHeader);

                using (DataTable dt = Lib.DataLayer.WebDB.Server_Select(false))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        TableRow row = new TableRow();
                        row.Cells.AddRange
                        (
                            new TableCell[]
                            {
                                UIHelpers.CreateTableCell(string.Format("<input type='checkbox' id='SERVER_{0}' name='SERVER_{0}'>",dr["ServerID"]), HorizontalAlign.Center,"cell1"),
                                UIHelpers.CreateTableCell(Converter.ToString(dr["ServerID"]),HorizontalAlign.Left,"cell1"),
                                UIHelpers.CreateTableCell(Converter.ToString(dr["ServerName"]),HorizontalAlign.Left,"cell1"),
                                UIHelpers.CreateTableCell(Converter.ToString(dr["FullName"]),HorizontalAlign.Left,"cell1"),
                                UIHelpers.CreateTableCell(Converter.ToString(dr["ServerIdentityName"]),HorizontalAlign.Left,"cell1"),
                                UIHelpers.CreateTableCell(string.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["Created"]),HorizontalAlign.Left,"cell1"),
                                UIHelpers.CreateTableCell(Converter.ToInt(dr["ServerStatus"]) == 1 ? "Đang hoạt động" : "<i><font color='#FF0000'>Đóng</font></i>",HorizontalAlign.Left,"cell1"),
                                UIHelpers.CreateTableCell(Converter.ToString(dr["SortOrder"]),HorizontalAlign.Left,"cell1"),
                                UIHelpers.CreateTableCell(Converter.ToInt(dr["IsHot"]) == 1 ? "x" : "" ,HorizontalAlign.Center,"cell1"),
                                UIHelpers.CreateTableCell(string.Format("<a href='ServerEdit.aspx?ServerID={0}'>[Edit]</a>", Converter.ToString(dr["ServerID"]).Trim()),HorizontalAlign.Left,"cell1")

                            }
                        );
                        table.Rows.Add(row);
                    }
                }


                this.panelList.Controls.Clear();
                this.panelList.Controls.Add(table);

            }
            catch (Exception ex)
            {
                Label labelMessage = new Label();
                labelMessage.Text = ex.Message;
                this.panelList.Controls.Add(labelMessage);
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            try
            {
                int serverID = Converter.ToInt(txtServerID.Text);
                if (serverID == 0)
                {
                    labelAddMessage.Text = "Mã Server không hợp lệ. Phải là giá trị số!";
                    return;
                }

                string serverName = txtServerName.Text.Trim();
                if (serverName == "") // || !serverName.StartsWith("S")) 
                {
                    labelAddMessage.Text = "Tên Server không hợp lệ. Phải có dạng Sn";
                    return;
                }

                string fullName = txtServerFullName.Text.Trim();
                if (fullName == "")
                {
                    labelAddMessage.Text = "Tên gọi đầy đủ của Server không hợp lệ";
                    return;
                }

                string serverIdentityName = txtIdentiyName.Text.Trim();
                if (serverIdentityName == "")
                {
                    labelAddMessage.Text = "Tên dùng định danh server không hợp lệ";
                    return;
                }

                int sortOder = Converter.ToInt(txtSortOrder.Text);
                if (sortOder == 0)
                {
                    labelAddMessage.Text = "Chưa nhập thứ tự hiển thị Server";
                    return;
                }

                if (!checkAccept.Checked)
                {
                    labelAddMessage.Text = "Vui lòng xác nhận đồng ý tạo Server";
                    return;
                }

                Lib.DataLayer.WebDB.Server_Insert(serverID, serverName, fullName, serverIdentityName, sortOder);

                Response.Redirect("ServerList.aspx", false);

            }
            catch (Exception ex)
            {
                labelAddMessage.Text = "Lỗi: " + ex.Message;
                checkAccept.Checked = false;
            }
        }

    }
}
