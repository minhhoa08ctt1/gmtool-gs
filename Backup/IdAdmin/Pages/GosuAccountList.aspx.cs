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
    public partial class GosuAccountList : Lib.UI.BasePage
    {
        protected int _page;
        protected string _FindValue;        //Rỗng nếu bỏ qua
        protected int _Status;              //-100 nếu bỏ qua
        protected int _Level;               //-1 nếu bỏ qua

        public GosuAccountList()
            : base(Lib.AppFunctions.GOSU_ACCOUNT_LIST)
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
                    _page = Converter.ToInt(GetParamter("page"));
                    _FindValue = GetParamter("findvalue");
                    if (_page <= 0) _page = 1;
                    ListGosuUsers();
                }
                else
                {
                    _page = 1;
                    _FindValue = txtFindValue.Text.Trim();
                    ListGosuUsers();
                }
            }
        }

        protected void ListGosuUsers()
        {
            try
            {
                string linkFormat = "GosuAccountList.aspx?page={0}&findvalue={1}";
                int startPos = (_page - 1) * _PageSize + 1;
                Table table = new Table();
                table.CssClass = "table1";
                table.CellSpacing = 1;
                TableHeaderRow rowHeader = new TableHeaderRow();
                rowHeader.Cells.AddRange
                (
                    new TableCell[]
                    {     
                        UIHelpers.CreateTableCell("STT",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Tên tài khoản",Unit.Percentage(20),HorizontalAlign.Center, "cellHeader"),                        
                        UIHelpers.CreateTableCell("Số gosu đã nạp",Unit.Percentage(15),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Số gosu đã sử dụng",Unit.Percentage(15),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Số gosu hiện có",Unit.Percentage(15),HorizontalAlign.Center,"cellHeader")
                    }
                );
                table.Rows.Add(rowHeader);
                using (DataTable dt = Lib.DataLayer.WebDB.Gosu_User_Select(_FindValue, startPos, _PageSize))
                {
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        TableRow rowEmpty = new TableRow();
                        rowEmpty.Cells.Add(UIHelpers.CreateTableCell("<p>Không có tài khoản cần tìm</p>", HorizontalAlign.Center, "cell1", 9));
                        table.Rows.Add(rowEmpty);
                    }
                    else
                    {
                        string css;
                        int stt = 0;
                        string returnURL = Server.UrlEncode(string.Format(linkFormat, _page, _FindValue));
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
                                    UIHelpers.CreateTableCell(dr["Account"].ToString(), HorizontalAlign.Left, css),
                                    UIHelpers.CreateTableCell(string.Format("{0:N0}", dr["Recharge"]), HorizontalAlign.Right,css),
                                    UIHelpers.CreateTableCell(string.Format("{0:N0}", dr["Spend"]), HorizontalAlign.Right, css),
                                    UIHelpers.CreateTableCell(string.Format("{0:N0}", dr["Balance"]), HorizontalAlign.Right, css),
                                }
                            );
                            table.Rows.Add(row);
                        }
                    }
                }

                this.panelList.Controls.Clear();
                this.panelList.Controls.Add(table);
                //Set Page Link
                this.linkFirst.NavigateUrl = string.Format(linkFormat, 1, _FindValue);
                this.linkPrev.NavigateUrl = string.Format(linkFormat, _page > 0 ? _page - 1 : 1, _FindValue);
                this.linkNext.NavigateUrl = string.Format(linkFormat, _page + 1, _FindValue);
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

