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
    public partial class ChanellingGameServer : Lib.UI.BasePage
    {
        private System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
        private string _partner = "";
        private string _description = "";

        public ChanellingGameServer()
            : base(Lib.AppFunctions.CHANELLINGGAMSERVER)
        {             
        }

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
                _partner = Converter.ToString(Request.QueryString["partner"]);
                _description = Converter.ToString(Request.QueryString["description"]);

                if (_partner == "")
                {
                    Response.Redirect("ChanellingPartner.aspx", false);
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
                                    if (checkedServer.StartsWith("CHANELLINGSERVER_"))
                                    {
                                        string servername = checkedServer.Replace("CHANELLINGSERVER_", "").Trim();
                                        if (servername != "")
                                        {
                                            Lib.DataLayer.WebDB.ChanellingGameServer_ChangeStatus(_partner, servername, status, status, "");
                                            listEditServer += servername + " ";
                                            countEditServer += 1;
                                        }
                                    }
                                }
                                if (countEditServer > 0)
                                {
                                    Lib.DataLayer.WebDB.WriteLog(_User.UserName, Request.UserHostAddress,
                                                                string.Format("Chanelling Game: {0}, Status = {1}, Servers: {2}", _partner, status, listEditServer));
                                }
                            }
                        }
                    }

                    this.labelPartner.Text = _partner.ToUpper();
                    GetChanellingGameServer();
                }
            }
        }
        
        protected void GetChanellingGameServer()
        {
            try
            {
                string actionLinks = "<a href='ChanellingServerEdit.aspx?partner={0}&server={1}'>[Edit]</a>&nbsp;&nbsp;&nbsp;<a href='ChanellingServerEdit.aspx?action=delete&partner={0}&server={1}' onclick='return confirm(\"Loại bỏ server này khỏi danh sách cấp cho đối tác chanelling?\")'>[Delete]</a>";
                Table table = new Table();
                table.CssClass = "table1";
                table.CellSpacing = 1;
                
                TableRow rowHeader = new TableRow();
                rowHeader.Cells.AddRange
                (
                    new TableCell[]
                    {   
                        UIHelpers.CreateTableCell("",Unit.Percentage(1),HorizontalAlign.Center, "cellHeader"),
                        UIHelpers.CreateTableCell("STT",Unit.Percentage(5),HorizontalAlign.Center, "cellHeader"),
                        UIHelpers.CreateTableCell("Ký hiệu server",Unit.Percentage(10),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Tên server",Unit.Percentage(30),HorizontalAlign.Center,"cellHeader"),                        
                        UIHelpers.CreateTableCell("Vào game?",Unit.Percentage(10),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Nạp tiền?",Unit.Percentage(10),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Ghi chú",Unit.Percentage(20),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("",Unit.Percentage(15),HorizontalAlign.Center,"cellHeader")
                    }
                );
                table.Rows.Add(rowHeader);

                this.panelList.Controls.Clear();
                this.panelList.Controls.Add(table);

                using (DataTable dt = WebDB.ChanellingGameServer_SelectByPartner(_partner))
                {
                    if (dt != null)
                    {
                        int stt = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            TableRow row = new TableRow();
                            row.Cells.Add(UIHelpers.CreateTableCell(string.Format("<input type='checkbox' id='CHANELLINGSERVER_{0}' name='CHANELLINGSERVER_{0}'>", dr["ServerName"]), HorizontalAlign.Center, "cell1"));
                            row.Cells.Add(UIHelpers.CreateTableCell((++stt).ToString(), HorizontalAlign.Center, "cell1"));
                            row.Cells.Add(UIHelpers.CreateTableCell(dr["ServerName"].ToString(), HorizontalAlign.Center, "cell1"));
                            row.Cells.Add(UIHelpers.CreateTableCell(dr["FullName"].ToString(), HorizontalAlign.Left, "cell1"));
                            row.Cells.Add(UIHelpers.CreateTableCell(Converter.ToInt(dr["PlayEnabled"]) == 1 ? "Đang mở" : "<i style='color:#FF0000'>Đóng</i>", HorizontalAlign.Center, "cell1"));
                            row.Cells.Add(UIHelpers.CreateTableCell(Converter.ToInt(dr["TopupEnabled"]) == 1 ? "Đang mở" : "<i style='color:#FF0000'>Đóng</i>", HorizontalAlign.Center, "cell1"));
                            row.Cells.Add(UIHelpers.CreateTableCell(dr["Notes"].ToString(), HorizontalAlign.Left, "cell1"));                                                        
                            row.Cells.Add(UIHelpers.CreateTableCell(string.Format(actionLinks, _partner, dr["ServerName"]), HorizontalAlign.Center, "cell1"));
                            table.Rows.Add(row);
                        }
                    }
                    else
                    {
                        Response.Redirect("Index.aspx", false);
                    }
                }

                linkAddServer.NavigateUrl = "ChanellingServerAdd.aspx?partner=" + _partner;
            }
            catch (Exception ex)
            {
                Response.Write(ex.StackTrace);
            }
        }
    }
}
