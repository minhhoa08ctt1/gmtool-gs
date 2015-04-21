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
    public partial class PaymentList : Lib.UI.BasePage
    {
        public PaymentList()
            : base(Lib.AppFunctions.PAYMENT_LIST)
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
                BillList();
            }
        }

        protected void BillList()
        {
            try
            {                
                Table table = new Table();
                table.CssClass = "table1";
                table.CellSpacing = 1;

                TableHeaderRow rowHeader = new TableHeaderRow();
                rowHeader.Cells.AddRange
                (
                    new TableCell[]
                    {     
                        UIHelpers.CreateTableCell("STT",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Thời điểm<br />lập hóa đơn",Unit.Percentage(15),HorizontalAlign.Center, "cellHeader"),                                                
                        UIHelpers.CreateTableCell("Người lập<br />hóa đơn",Unit.Percentage(10),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Tài khoản<br />nạp tiền",Unit.Percentage(10),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Số tiền<br />(thu của KH)",Unit.Percentage(10),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Số tiền<br />(chuyển vào game)",Unit.Percentage(10),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Game",Unit.Percentage(15),HorizontalAlign.Center,"cellHeader"),                       
                        UIHelpers.CreateTableCell("Server",Unit.Percentage(5),HorizontalAlign.Center,"cellHeader"),                       
                        UIHelpers.CreateTableCell("&nbsp;",Unit.Percentage(15),HorizontalAlign.Left,"cellHeader")
                    }
                );
                table.Rows.Add(rowHeader);

                using (DataTable dt = Lib.DataLayer.WebDB.Bill_SelectNotAccept())
                {
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        TableRow rowEmpty = new TableRow();
                        rowEmpty.Cells.Add(UIHelpers.CreateTableCell("<p>Không có hóa đơn cần xử lý</p>", HorizontalAlign.Center, "cell1", 9));
                        table.Rows.Add(rowEmpty);
                    }
                    else
                    {
                        string css;
                        int stt = 0;
                        string linkCellText = "";
                        foreach (DataRow dr in dt.Rows)
                        {
                            stt += 1;
                            css = stt % 2 == 0 ? "cell1" : "cell2";    
                            if (_User.UserName.Trim() == dr[Lib.Meta.BILL_CREATEDUSERID].ToString().Trim())
                            {
                                linkCellText = string.Format("<a href='PaymentAccept.aspx?id={0}&action=delete' {1}>Xóa</a>",
                                                             dr[Lib.Meta.BILL_BILLID],
                                                             UIHelpers.CreateConfirm("Xóa hóa đơn nạp tiền này?"));
                            }
                            else
                            {
                                linkCellText = string.Format("<a href='PaymentAccept.aspx?id={0}&action=delete' {1}>Xóa</a> | <a href='PaymentAccept.aspx?id={0}'>Xử lý hóa đơn</a>",
                                                             dr[Lib.Meta.BILL_BILLID],
                                                             UIHelpers.CreateConfirm("Xóa hóa đơn nạp tiền này?"));
                            }
                            TableRow row = new TableRow();
                            row.Cells.AddRange
                            (
                                new TableCell[]
                                {
                                    UIHelpers.CreateTableCell(stt.ToString(),HorizontalAlign.Center,css),
                                    UIHelpers.CreateTableCell(string.Format("{0:dd/MM/yyyy HH:mm:ss}",dr[Lib.Meta.BILL_CREATEDTIME]),HorizontalAlign.Center,css),
                                    UIHelpers.CreateTableCell(dr[Lib.Meta.BILL_CREATEDUSERID].ToString(), HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(dr[Lib.Meta.BILL_ACCOUNT].ToString(), HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(string.Format("{0:N0}",dr[Lib.Meta.BILL_AMOUNT]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(string.Format("{0:N0}",dr[Lib.Meta.BILL_CARDLOGAMOUNT]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(dr["GameName"].ToString(),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(dr[Lib.Meta.BILL_GAMESERVER].ToString(),HorizontalAlign.Center,css),
                                    UIHelpers.CreateTableCell(linkCellText,HorizontalAlign.Left,css)
                                }
                            );
                            table.Rows.Add(row);
                        }
                    }
                }

                this.panelList.Controls.Clear();
                this.panelList.Controls.Add(table);
               
            }
            catch (Exception ex)
            {
                Label labelMessage = new Label();
                labelMessage.Text = ex.StackTrace;
                this.panelList.Controls.Add(labelMessage);
            }

        }
    }
}
