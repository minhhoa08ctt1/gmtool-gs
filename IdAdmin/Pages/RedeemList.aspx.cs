using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IDAdmin.Lib.UI;
using System.Web.Script.Serialization;
using IDAdmin.Lib.Utils;
using IDAdmin.Lib.DataLayer;
namespace IDAdmin.Pages
{
    public partial class RedeemList : BasePage {
        protected override void Page_Load(object sender, EventArgs e)
        {
            dataPanel.Controls.Add(get_Listdata());               
        }
      

        protected void acceptCom_Click(object sender, EventArgs e)
        {
            var list_checkbox = Request.Form["logitem"];
            List<AdminLog> logList = IDAdmin.Lib.DataLayer.WebDB.GetLogCompensationWhere(list_checkbox);
           // string[] separators = { ","};
           // int[] list = list_checkbox.Split(separators, IntSplitOptions.RemoveEmptyEntries);
            if (logList.Count > 0)
            {

            }
            foreach (var item in logList)
            {
                JavaScriptSerializer ser = new JavaScriptSerializer();
                var parram_data = ser.Deserialize<Dictionary<string, string>>(item.parram_input.ToString());               
                
                   // labelCheckMessageView1.Text = "";
                    string gameType = parram_data.ElementAt(0).Value;
                    string zoneId = parram_data.ElementAt(1).Value;
                    string uId = parram_data.ElementAt(2).Value;
                    string charId = parram_data.ElementAt(3).Value;
                    string name = parram_data.ElementAt(4).Value;
                    string reType = parram_data.ElementAt(5).Value;
                    string num = parram_data.ElementAt(6).Value;
                    if (string.IsNullOrEmpty(gameType) ||
                        string.IsNullOrEmpty(zoneId) ||
                        string.IsNullOrEmpty(uId) ||
                        string.IsNullOrEmpty(charId) ||
                        string.IsNullOrEmpty(name) ||
                        string.IsNullOrEmpty(reType) ||
                        string.IsNullOrEmpty(num)
                        )
                    {
                        //labelMessageView1.Text = "Dữ liệu nhập không hợp lệ";
                        return;
                    }

                    string hostName = "222.255.177.23";
                    string port = "19906";
                    string url = "http://{0}:{1}/redeem?gametype={2}&zoneid={3}&uid={4}&char_id={5}&name={6}&retype={7}&num={8}";
                    url = string.Format(url, hostName, port, gameType, zoneId, uId, charId, name, reType, num);

                    if (reType.CompareTo("7") == 0)
                    {

                        string itemId = parram_data.ElementAt(7).Value;
                        string isBind = parram_data.ElementAt(8).Value;
                        string level = parram_data.ElementAt(9).Value;
                        if (
                            string.IsNullOrEmpty(itemId) ||
                            string.IsNullOrEmpty(isBind)
                            )
                        {
                           // labelMessageView1.Text = "Dữ liệu nhập không hợp lệ";
                            return;
                        }
                        url += "&itemID=" + itemId + "&isBind=" + isBind;
                        if (!string.IsNullOrEmpty(level))
                        {
                            url += "&level=" + level;
                        }
                    }

                    string result = HttpHelper.HttpSocket(url, 27); // goi api add item
                    
                
                    int errorCode = 0;
                    string a = messageApi(result, ref errorCode);
                    if (errorCode == 1)
                    {
                        bool b = WebDB.UpdateStatusCompensation(item.id, errorCode,result);
                        Response.Redirect(Request.RawUrl);
                    }
                    else
                    {
                        bool b = WebDB.UpdateStatusCompensation(item.id, errorCode, result);
                    }
            }
        }

        protected void dismissCom_Click(object sender, EventArgs e)
        {
            var list_checkbox = Request.Form["logitem"];
            List<AdminLog> logList = WebDB.GetLogCompensationWhere(list_checkbox);
            // string[] separators = { ","};
            // int[] list = list_checkbox.Split(separators, IntSplitOptions.RemoveEmptyEntries);
            if (logList.Count > 0)
            {

            }
            foreach (var item in logList)
            {               
                bool b = WebDB.UpdateStatusCompensation(item.id, -1001, "");              
            }
            Response.Redirect(Request.RawUrl);
        }


        private Table get_Listdata()
        {
            Table table = new Table();
            table.CssClass = "table1";
            TableRow rowHeader = new TableRow();          
          
            rowHeader.Cells.AddRange(
                new TableCell[]
                { 
                    UIHelpers.CreateTableCell("Chọn",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
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

            List<AdminLog> dataLogCompensation = WebDB.GetLogCompensation(0);           

            try
            {
                string css;
                int stt = 0;
                foreach (var item in dataLogCompensation)
                {
                    string parram = item.parram_input;
                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    var parram_data = ser.Deserialize<Dictionary<string,string>>(parram);
                    stt++;
                    css = stt % 2 == 0 ? "cell1" : "cell2";
                    TableRow row = new TableRow();
                    row.Cells.AddRange
                    (
                        new TableCell[]
                        {
                            UIHelpers.CreateTableCell("<input type='checkbox' name='logitem' value='"+item.id.ToString()+"'>",HorizontalAlign.Center,css),
                            UIHelpers.CreateTableCell(AdminLog.listStatus(item.status),HorizontalAlign.Center,css),
                            UIHelpers.CreateTableCell(item.title,HorizontalAlign.Center,css),
                            UIHelpers.CreateTableCell(item.logtime.ToString(),HorizontalAlign.Center,css),    
                            UIHelpers.CreateTableCell(item.username.ToString(),HorizontalAlign.Center,css),
                            UIHelpers.CreateTableCell(parram_data.ElementAt(1).Value,HorizontalAlign.Center,css),
                            UIHelpers.CreateTableCell("",HorizontalAlign.Center,css),
                            UIHelpers.CreateTableCell("",HorizontalAlign.Center,css),
                            UIHelpers.CreateTableCell(parram_data.ElementAt(4).Value.ToString(),HorizontalAlign.Center,css),
                            UIHelpers.CreateTableCell("<a href='RedeemDetail.aspx?logid="+item.id.ToString()+"'>Xem</a>",HorizontalAlign.Center,css)
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