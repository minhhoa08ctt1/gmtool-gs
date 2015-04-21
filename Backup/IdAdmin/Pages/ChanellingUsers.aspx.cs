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
    public partial class ChanellingUsers : Lib.UI.BasePage
    {
        int _page;
        string _community;
        string _userName;

        public ChanellingUsers()
            : base(Lib.AppFunctions.CHANELLINGUSER)
        {
            _PageSize = 50;
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
            else
            {
                if (!Page.IsPostBack)
                {
                    _page = Converter.ToInt(GetParamter("page"));
                    _community = Converter.ToString(GetParamter("community"));
                    _userName = Converter.ToString(GetParamter("username"));
                                        
                    txtUserName.Text = _userName;

                    if (_page <= 0) _page = 1;

                    ListUser();

                }
                else
                {
                    _page = 1;                    
                    _community = Converter.ToString(cmbCommunity.SelectedValue);
                    _userName = Converter.ToString(txtUserName.Text);

                    ListUser();
                }
            }
        }

        private void ListUser()
        {
            try
            {
                string linkFormat = "ChanellingUsers.aspx?community={0}&username={1}&page={2}";

                Table table = new Table();
                table.CssClass = "table1";
                table.CellSpacing = 1;

                TableRow rowSumary = new TableRow();
                TableCell cellSumary = UIHelpers.CreateTableCell("", HorizontalAlign.Left, "cell0", 6);
                rowSumary.Cells.Add(cellSumary);
                table.Rows.Add(rowSumary);

                TableRow rowHeader = new TableRow();
                rowHeader.Cells.AddRange
                (
                    new TableCell[]
                    {   
                        UIHelpers.CreateTableCell("STT",Unit.Percentage(10),HorizontalAlign.Center, "cellHeader"),                                                
                        UIHelpers.CreateTableCell("Tên tài khoản<br />(phía đối tác)",Unit.Percentage(25),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Tên tài khoản<br />(bên GOSU ID)",Unit.Percentage(20),HorizontalAlign.Center,"cellHeader"),                        
                        UIHelpers.CreateTableCell("Cộng đồng<br />(đối tác Chanelling)",Unit.Percentage(15),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Thời điểm đăng ký",Unit.Percentage(15),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("",Unit.Percentage(15),HorizontalAlign.Center,"cellHeader")
                    }
                );
                table.Rows.Add(rowHeader);

                using (DataTable dt = WebDB.ChanellingUser_Select(_community, _userName, _page, _PageSize))
                {
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        TableRow rowEmpty = new TableRow();
                        rowEmpty.Cells.Add(UIHelpers.CreateTableCell("<p>Không có dữ liệu</p>", HorizontalAlign.Center, "cell1", 6));
                        table.Rows.Add(rowEmpty);
                    }
                    else
                    {
                        string s = string.Format("- Số lượng tài khoản: <b>{0:N0}</b>", dt.Rows[0]["CountOfUser"]);
                        cellSumary.Text = s;

                        foreach (DataRow dr in dt.Rows)
                        {
                            TableRow row = new TableRow();
                            row.Cells.AddRange
                            (
                                new TableCell[]
                                {
                                    UIHelpers.CreateTableCell(dr["RowNumber"].ToString(),HorizontalAlign.Center,"cell1"),                                    
                                    UIHelpers.CreateTableCell(dr["UserName"].ToString(),HorizontalAlign.Left,"cell1"),
                                    UIHelpers.CreateTableCell(dr["CustomerID"].ToString(),HorizontalAlign.Left,"cell1"),                                    
                                    UIHelpers.CreateTableCell(dr["Community"].ToString(),HorizontalAlign.Center,"cell1"),
                                    UIHelpers.CreateTableCell(string.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["Created"]),HorizontalAlign.Left,"cell1"),
                                    UIHelpers.CreateTableCell("",HorizontalAlign.Center,"cell1")
                                }
                            );
                            table.Rows.Add(row);
                        }
                    }
                }
                this.panelList.Controls.Clear();
                this.panelList.Controls.Add(table);

                this.linkPrev.NavigateUrl = string.Format(linkFormat,_community, _userName,  _page > 0 ? _page - 1 : 1);
                this.linkNext.NavigateUrl = string.Format(linkFormat,_community, _userName, _page + 1);
            }
            catch (Exception ex)
            {
                Label labelErrorMessage = new Label();
                labelErrorMessage.ForeColor = System.Drawing.Color.Red;
                labelErrorMessage.Text = string.Format("Lỗi: {0}: {1}", ex.Message,ex.StackTrace);
                this.panelList.Controls.Clear();
                this.panelList.Controls.Add(labelErrorMessage);
            }
        }
    }
}
