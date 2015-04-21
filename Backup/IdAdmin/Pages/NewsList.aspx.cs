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
    public partial class NewsList : Lib.UI.BasePage
    {
        public NewsList()
            : base(Lib.AppFunctions.NEWS_LIST)
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
                long id = Converter.ToLong(GetParamter("id"));
                ListNews(id);
            }
        }

        protected void ListNews(long id)
        {
            try
            {
                long newsID = -1;
                string linkFormat = "NewsList.aspx?id={0}";
                string returnURL = Server.UrlEncode(string.Format(linkFormat, id));

                Table table = new Table();
                table.CssClass = "table1";
                table.CellSpacing = 1;

                TableHeaderRow rowHeader = new TableHeaderRow();
                rowHeader.Cells.AddRange
                (
                    new TableCell[]
                    {     
                        UIHelpers.CreateTableCell("STT",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Ngày đăng bài",Unit.Percentage(10),HorizontalAlign.Left, "cellHeader"),                        
                        UIHelpers.CreateTableCell("Bài viết",Unit.Percentage(65),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Người viết bài",Unit.Percentage(10),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("&nbsp;",Unit.Percentage(10),HorizontalAlign.Left,"cellHeader")
                    }
                );
                table.Rows.Add(rowHeader);

                using (DataTable dt = Lib.DataLayer.WebDB.News_Select(id))
                {
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        TableRow rowEmpty = new TableRow();
                        rowEmpty.Cells.Add(UIHelpers.CreateTableCell("<p>Không có bài viết!</p>", HorizontalAlign.Center, "cell1", 5));
                        table.Rows.Add(rowEmpty);
                    }
                    else
                    {
                        string css;
                        int stt = 0;


                        foreach (DataRow dr in dt.Rows)
                        {
                            stt += 1;
                            css = stt % 2 == 0 ? "cell1" : "cell2";
                            newsID = Converter.ToLong(dr[Lib.Meta.NEWS_ID]);
                            TableRow row = new TableRow();
                            row.Cells.AddRange
                            (
                                new TableCell[]
                                {
                                    UIHelpers.CreateTableCell(stt.ToString(),HorizontalAlign.Center,css),
                                    UIHelpers.CreateTableCell(string.Format("{0:dd/MM/yyyy HH:mm}",dr[Lib.Meta.NEWS_CREATED]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(string.Format("<a href='{0}/News/Details/{1}' target='_blank'><b>{2}</b></a><br />{3}...",
                                                                            AppManager.IDLink,
                                                                            newsID, 
                                                                            dr[Lib.Meta.NEWS_TITLE], 
                                                                            Converter.StripHTML(dr[Lib.Meta.NEWS_CONTENT],50)),
                                                             HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(dr[Lib.Meta.NEWS_WRITTER].ToString(), HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(string.Format("<a href='NewsEdit.aspx?id={0}&action=edit&returnURL={1}'>Sửa bài</a>&nbsp;|&nbsp;<a href='NewsDelete.aspx?id={0}&returnURL={1}' {2}>Xóa bài</a>",newsID,returnURL, UIHelpers.CreateConfirm("Xóa bài viết này?")), HorizontalAlign.Left, css)
                                }
                            );
                            table.Rows.Add(row);
                        }
                    }
                }

                this.panelList.Controls.Clear();
                this.panelList.Controls.Add(table);

                this.linkPrev.NavigateUrl = string.Format(linkFormat, id + 20);
                this.linkNext.NavigateUrl = string.Format(linkFormat, newsID);

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
