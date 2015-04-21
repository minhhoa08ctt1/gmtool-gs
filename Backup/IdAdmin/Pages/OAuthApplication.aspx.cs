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
    public partial class OAuthApplication : Lib.UI.BasePage
    {
        protected int _page;
        protected string _SearchValue;

        public OAuthApplication()
            : base(Lib.AppFunctions.OAUTH_MANAGER)
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
                    _SearchValue = GetParamter("searchvalue");
                    if (_page <= 0) _page = 1;

                    ListApplication();
                }
                else
                {
                    _page = 1;
                    _SearchValue = txtSearchValue.Text.Trim();

                    ListApplication();
                }
            }
        }

        protected void ListApplication()
        {
            try
            {
                string linkFormat = "OAuthApplication.aspx?page={0}&searchvalue={1}";

                Table table = new Table();
                table.CssClass = "table1";
                table.CellSpacing = 1;

                TableHeaderRow rowHeader = new TableHeaderRow();
                rowHeader.Cells.AddRange
                (
                    new TableCell[]
                    {     
                        UIHelpers.CreateTableCell("STT",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("ClientID",Unit.Percentage(20),HorizontalAlign.Center, "cellHeader"),                        
                        UIHelpers.CreateTableCell("Application Name",Unit.Percentage(30),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("UserName",Unit.Percentage(15),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Enabled",Unit.Percentage(7),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Alway Trust?",Unit.Percentage(8),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("&nbsp;",Unit.Percentage(15),HorizontalAlign.Center,"cellHeader")
                    }
                );
                table.Rows.Add(rowHeader);

                using (DataTable dt = Lib.DataLayer.WebDB.OAuthApplication_Select(_SearchValue, _page, 50))
                {
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        TableRow rowEmpty = new TableRow();
                        rowEmpty.Cells.Add(UIHelpers.CreateTableCell("<p>Không có ứng dụng cần tìm</p>", HorizontalAlign.Center, "cell1", 7));
                        table.Rows.Add(rowEmpty);
                    }
                    else
                    {
                        string css = "cell1";
                        string returnURL = Server.UrlEncode(string.Format(linkFormat, _page, _SearchValue));
                        foreach (DataRow dr in dt.Rows)
                        {
                            css = css == "cell2" ? "cell1" : "cell2";
                            TableRow row = new TableRow();
                            row.Cells.AddRange
                            (
                                new TableCell[]
                                {
                                    UIHelpers.CreateTableCell(dr[Lib.Meta.COMMON_ROWNUMBER].ToString(),HorizontalAlign.Center,css),
                                    UIHelpers.CreateTableCell(dr["ClientID"].ToString(), HorizontalAlign.Left, css),                                    
                                    UIHelpers.CreateTableCell(dr["ApplicationName"].ToString(), HorizontalAlign.Left, css),
                                    UIHelpers.CreateTableCell(string.Format("<a href='AccountList.aspx?findvalue={0}' target='_blank'>{0}</a>",dr["UserName"]), HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(Converter.ToBoolean(dr["Enabled"]).ToString(), HorizontalAlign.Center, css),
                                    UIHelpers.CreateTableCell(Converter.ToBoolean(dr["AlwaysTrust"]).ToString(), HorizontalAlign.Center, css),                                                                        
                                    UIHelpers.CreateTableCell(string.Format("<a href='OAuthApplication_Edit.aspx?action=edit&clientid={0}&returnURL={1}'>[Edit]</a>&nbsp;&nbsp;<a href='OAuthPaymentService.aspx?clientid={0}&returnURL={1}'>[Payment service]<a/>", dr["ClientID"].ToString(),returnURL), 
                                                              HorizontalAlign.Center, css),
                                }
                            );
                            table.Rows.Add(row);
                        }
                    }
                }

                this.panelList.Controls.Clear();
                this.panelList.Controls.Add(table);

                //Set Page Link

                this.linkFirst.NavigateUrl = string.Format(linkFormat, 1, _SearchValue);
                this.linkPrev.NavigateUrl = string.Format(linkFormat, _page > 0 ? _page - 1 : 1, _SearchValue);
                this.linkNext.NavigateUrl = string.Format(linkFormat, _page + 1, _SearchValue);
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
