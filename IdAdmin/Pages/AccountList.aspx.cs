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
    public partial class AccountList : Lib.UI.BasePage
    {
        protected int _page;
        protected string _FindValue;        //Rỗng nếu bỏ qua
        protected int _Status;              //-100 nếu bỏ qua
        protected int _Level;               //-1 nếu bỏ qua

        public AccountList()
            : base(Lib.AppFunctions.ACCOUNT_LIST)
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
                    _Status = Converter.ToInt(GetParamter("status"));
                    _Level = Converter.ToInt(GetParamter("level"));
                    _FindValue = GetParamter("findvalue");
                    if (_page <= 0) _page = 1;
                    if (_Status == 0) _Status = -100;
                    if (_Level == 0) _Level = -1;

                    ListUsers();
                }
                else
                {
                    _page = 1;
                    _FindValue = txtFindValue.Text.Trim();
                    _Status = Converter.ToInt(cmbStatus.SelectedValue, -100);
                    _Level = Converter.ToInt(cmbLevel.SelectedValue);

                    ListUsers();
                }
            }
        }

        protected void ListUsers()
        {
            try
            {
                string linkFormat = "AccountList.aspx?page={0}&status={1}&level={2}&findvalue={3}";

                int startPos = (_page - 1) * _PageSize + 1;

                Table table = new Table();
                table.CssClass = "table1";
                table.CellSpacing = 1;

                TableHeaderRow rowHeader = new TableHeaderRow();
                rowHeader.Cells.AddRange
                (
                    new TableCell[]
                    {     
                        UIHelpers.CreateTableCell("STT",Unit.Percentage(3), HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Tên tài khoản",Unit.Percentage(15),HorizontalAlign.Left, "cellHeader"),                        
                        UIHelpers.CreateTableCell("Họ tên",Unit.Percentage(15),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Số CMND",Unit.Percentage(10),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Điện thoại",Unit.Percentage(10),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Email",Unit.Percentage(15),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Địa chỉ",Unit.Percentage(17),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Trạng thái",Unit.Percentage(10),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Nguồn",Unit.Percentage(5),HorizontalAlign.Left,"cellHeader")
                    }
                );
                table.Rows.Add(rowHeader);

                using (DataTable dt = Lib.DataLayer.WebDB.User_Select(_FindValue, _Status, _Level, startPos, _PageSize))
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
                        int rowStatus;
                        string returnURL = Server.UrlEncode(string.Format(linkFormat, _page, _Status, _Level, _FindValue));
                        foreach (DataRow dr in dt.Rows)
                        {
                            stt += 1;
                            css = stt % 2 == 0 ? "cell1" : "cell2";
                            rowStatus = Converter.ToInt(dr[Lib.Meta.USER_STATUS]);
                            TableRow row = new TableRow();
                            row.Cells.AddRange
                            (
                                new TableCell[]
                                {
                                    UIHelpers.CreateTableCell(dr[Lib.Meta.COMMON_ROWNUMBER].ToString(),HorizontalAlign.Center,css),
                                    UIHelpers.CreateTableCell(string.Format("<a href='AccountDetails.aspx?id={0}&returnURL={1}'><b>{2}</b></a>",dr[Lib.Meta.USER_CUSTOMERID].ToString().Trim(), returnURL, dr[Lib.Meta.USER_USERNAME]),
                                                              HorizontalAlign.Left,css),                                    
                                    UIHelpers.CreateTableCell(dr[Lib.Meta.USER_NAME].ToString(), HorizontalAlign.Left, css),
                                    UIHelpers.CreateTableCell(dr[Lib.Meta.USER_NUMBERID].ToString(), HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(dr[Lib.Meta.USER_PHONENUMBER].ToString(), HorizontalAlign.Left, css),
                                    UIHelpers.CreateTableCell(dr[Lib.Meta.USER_EMAIL].ToString(), HorizontalAlign.Left, css),
                                    UIHelpers.CreateTableCell(dr[Lib.Meta.USER_ADDRESS].ToString(), HorizontalAlign.Left, css),
                                    UIHelpers.CreateTableCell(rowStatus == -1 ? "<i>Bị khóa</i>" : (rowStatus == 0 ? "<i>Chưa kích hoạt</i>" : "Đã kích hoạt"), 
                                                              HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(dr["Source"].ToString(), HorizontalAlign.Left, css),
                                }
                            );
                            table.Rows.Add(row);
                        }
                    }
                }

                this.panelList.Controls.Clear();
                this.panelList.Controls.Add(table);

                //Set Page Link

                this.linkFirst.NavigateUrl = string.Format(linkFormat, 1, _Status, _Level, _FindValue);
                this.linkPrev.NavigateUrl = string.Format(linkFormat, _page > 0 ? _page - 1 : 1, _Status, _Level, _FindValue);
                this.linkNext.NavigateUrl = string.Format(linkFormat, _page + 1, _Status, _Level, _FindValue);
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

