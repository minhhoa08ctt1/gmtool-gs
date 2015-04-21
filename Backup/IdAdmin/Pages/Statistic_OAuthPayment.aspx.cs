using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IDAdmin.Lib.DataLayer;
using IDAdmin.Lib.UI;
using IDAdmin.Lib.Utils;

namespace IDAdmin.Pages
{
    public partial class Statistic_OAuthPayment : Lib.UI.BasePage
    {
        private string _ServiceID = "";
        private string _ServiceName = "";

        public Statistic_OAuthPayment():base(Lib.AppFunctions.STATISTIC_OAUTHPAYMENT)
        {
        }

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
                _ServiceID = GetParamter("service");
                _ServiceName = HttpUtility.UrlDecode(GetParamter("name"));
                if (_ServiceID == "")
                {
                    ShowPaymentServiceList();
                }
                else
                {
                    ShowSumary();
                }
            }
        }

        private void ShowPaymentServiceList()
        {
            try
            {
                labelTitle.Text = "DANH SÁCH DỊCH VỤ";
                panelDateSelect.Visible = false;

                Table table = new Table();
                table.CssClass = "table1";
                table.CellSpacing = 1;

                TableHeaderRow rowHeader = new TableHeaderRow();
                rowHeader.Cells.AddRange
                (
                    new TableCell[]
                    {     
                        UIHelpers.CreateTableCell("STT",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Mã dịch vụ",Unit.Percentage(30),HorizontalAlign.Center, "cellHeader"),                        
                        UIHelpers.CreateTableCell("Tên dịch vụ đăng ký",Unit.Percentage(40),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Ngày đăng ký",Unit.Percentage(10),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Hình thức thanh toán",Unit.Percentage(15),HorizontalAlign.Center,"cellHeader")
                    }
                );
                table.Rows.Add(rowHeader);

                using (DataTable dt = Lib.DataLayer.WebDB.OAuthPaymentService_Select(""))
                {
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        TableRow rowEmpty = new TableRow();
                        rowEmpty.Cells.Add(UIHelpers.CreateTableCell("<p>&nbsp;</p>", HorizontalAlign.Center, "cell1", 6));
                        table.Rows.Add(rowEmpty);
                    }
                    else
                    {
                        string css = "cell1";
                        int stt = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            css = css == "cell2" ? "cell1" : "cell2";
                            TableRow row = new TableRow();
                            row.Cells.AddRange
                            (
                                new TableCell[]
                                {
                                    UIHelpers.CreateTableCell((++stt).ToString(),HorizontalAlign.Center,css),
                                    UIHelpers.CreateTableCell(string.Format("<a href='Statistic_OAuthPayment.aspx?service={0}&name={1}'>{0}</a>",dr["ServiceID"], HttpUtility.UrlEncode(dr["ServiceName"].ToString())), 
                                                              HorizontalAlign.Left, css),                                    
                                    UIHelpers.CreateTableCell(dr["ServiceName"].ToString(), HorizontalAlign.Left, css),
                                    UIHelpers.CreateTableCell(string.Format("{0:dd/MM/yyyy}", dr["RegisterDate"]), HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(dr["GosuTransferType"].ToString(), HorizontalAlign.Center, css)
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
                throw ex;
            }
            
        }

        private void ShowSumary()
        {
            try
            {
                labelTitle.Text = _ServiceName;
                linkServiceList.Visible = true;
                panelDateSelect.Visible = true;

                DateTime fromDate = Converter.ToDateTime(txtFromDate.Text, DateTime.Now);
                DateTime toDate = Converter.ToDateTime(txtToDate.Text, DateTime.Now);

                Table table = new Table();
                table.CssClass = "table1";
                table.CellSpacing = 1;

                TableHeaderRow rowHeader = new TableHeaderRow();
                rowHeader.Cells.AddRange
                (
                    new TableCell[]
                    {   
                        UIHelpers.CreateTableCell("Ngày",Unit.Percentage(10),HorizontalAlign.Center, "cellHeader"),                        
                        UIHelpers.CreateTableCell("Số lượt giao dịch<br />GOSU",Unit.Percentage(20),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Tổng GOSU<br />giao dịch",Unit.Percentage(25),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Số lượt giao dịch<br />GOSU tặng",Unit.Percentage(20),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Tổng GOSU tặng<br />giao dịch",Unit.Percentage(25),HorizontalAlign.Center,"cellHeader"),
                    }
                );
                table.Rows.Add(rowHeader);

                using (DataTable dt = Lib.DataLayer.WebDB.Statistic_OAuthPayment(_ServiceID, fromDate, toDate))
                {
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        TableRow rowEmpty = new TableRow();
                        rowEmpty.Cells.Add(UIHelpers.CreateTableCell("<p>Không có số liệu</p>", HorizontalAlign.Center, "cell1", 5));
                        table.Rows.Add(rowEmpty);
                    }
                    else
                    {
                        string css = "cell1";
                        int countGOSU = 0, sumGOSU = 0, countPromotion = 0, sumPromotion = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            css = css == "cell2" ? "cell1" : "cell2";
                            TableRow row = new TableRow();
                            row.Cells.AddRange
                            (
                                new TableCell[]
                                {                                    
                                    UIHelpers.CreateTableCell(string.Format("{0:dd/MM/yyyy}",dr["Created"]), HorizontalAlign.Left, css),                                    
                                    UIHelpers.CreateTableCell(string.Format("{0:N0}", dr["CountOfGOSU"]), HorizontalAlign.Left, css),
                                    UIHelpers.CreateTableCell(string.Format("{0:N0}", dr["SumOfGOSU"]), HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(string.Format("{0:N0}", dr["CountOfPromotion"]), HorizontalAlign.Left, css),
                                    UIHelpers.CreateTableCell(string.Format("{0:N0}", dr["SumOfPromotion"]), HorizontalAlign.Left, css),
                                }
                            );
                            countGOSU += Converter.ToInt(dr["CountOfGOSU"]);
                            sumGOSU += Converter.ToInt(dr["SumOfGOSU"]);
                            countPromotion += Converter.ToInt(dr["CountOfPromotion"]);
                            sumPromotion += Converter.ToInt(dr["SumOfPromotion"]);
                            table.Rows.Add(row);
                        }
                        TableRow rowSum = new TableRow();
                        rowSum.Cells.AddRange(
                            new TableCell[]
                            {
                                UIHelpers.CreateTableCell("<b>TỔNG CỘNG:</b>", HorizontalAlign.Right, "cellTitle"),
                                UIHelpers.CreateTableCell(string.Format("{0:N0}", countGOSU), HorizontalAlign.Left, "cellTitle"),
                                UIHelpers.CreateTableCell(string.Format("{0:N0}", sumGOSU), HorizontalAlign.Left, "cellTitle"),
                                UIHelpers.CreateTableCell(string.Format("{0:N0}", countPromotion), HorizontalAlign.Left, "cellTitle"),
                                UIHelpers.CreateTableCell(string.Format("{0:N0}", sumPromotion), HorizontalAlign.Left, "cellTitle")
      
                            }
                        );
                        table.Rows.Add(rowSum);
                    }
                }

                this.panelList.Controls.Clear();
                this.panelList.Controls.Add(table);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
