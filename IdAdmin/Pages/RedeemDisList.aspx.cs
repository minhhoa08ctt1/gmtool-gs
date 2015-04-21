using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IDAdmin.Lib.UI;
using System.Web.Script.Serialization;
using IDAdmin.Lib.Utils;
using IDAdmin.Lib.DataLayer;
namespace IDAdmin.Pages
{
    public partial class RedeemDisList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            dataPanel.Controls.Add(get_Listdata());     
        }

        private Table get_Listdata()
        {
            Table table = new Table();
            table.CssClass = "table1";
            TableRow rowHeader = new TableRow();

            rowHeader.Cells.AddRange(
                new TableCell[]
                {                     
                    UIHelpers.CreateTableCell("Trạng thái",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Thư toàn cục",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Thời gian đền",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Tài khoản GM",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Khu vực",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Server",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Hạn chế cấp",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Tên nhân vật",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Chi tiết",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader")
                }
            );
            table.Rows.Add(rowHeader);

            List<AdminLog> dataLogCompensation = WebDB.GetLogCompensation(2);

            try
            {
                string css;
                int stt = 0;
                foreach (var item in dataLogCompensation)
                {
                    string parram = item.parram_input;
                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    var parram_data = ser.Deserialize<Dictionary<string, string>>(parram);
                    stt++;
                    css = stt % 2 == 0 ? "cell1" : "cell2";
                    TableRow row = new TableRow();
                    row.Cells.AddRange
                    (
                        new TableCell[]
                        {
                            UIHelpers.CreateTableCell(AdminLog.listStatus(item.status),HorizontalAlign.Center,css),
                            UIHelpers.CreateTableCell(item.title,HorizontalAlign.Center,css),
                            UIHelpers.CreateTableCell(item.logtime.ToString(),HorizontalAlign.Center,css),    
                            UIHelpers.CreateTableCell(item.username.ToString(),HorizontalAlign.Center,css),
                            UIHelpers.CreateTableCell(parram_data.ElementAt(1).Value,HorizontalAlign.Center,css),
                            UIHelpers.CreateTableCell("",HorizontalAlign.Center,css),
                            UIHelpers.CreateTableCell("",HorizontalAlign.Center,css),
                            UIHelpers.CreateTableCell(parram_data.ElementAt(4).Value.ToString(),HorizontalAlign.Center,css),
                            UIHelpers.CreateTableCell("<a href='RedeemDetail.aspx?logid="+item.id.ToString()+"'>Sửa</a>,<a href='RedeemDetail/"+item.id.ToString()+".aspx'>Xóa</a>",HorizontalAlign.Center,css)
                        }
                    );
                    table.Rows.Add(row);
                }

            }
            catch (Exception ex)
            {

            }
            return table;
        }

    }
}