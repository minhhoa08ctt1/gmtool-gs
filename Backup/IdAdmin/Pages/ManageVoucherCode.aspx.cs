using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IDAdmin.Lib.UI;
using IDAdmin.Lib.Utils;
using System.Text;

namespace IDAdmin.Pages
{
    public partial class ManageVoucherCode : Lib.UI.BasePage
    {
        protected int _page;
        public ManageVoucherCode()
            : base(Lib.AppFunctions.MANAGEVOUCHERCODE)
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
            else if (AppManager.GameID == "")
            {
                Response.Redirect("Index.aspx", false);
            }
            else
            {
                buttonExecute.Click += new EventHandler(buttonExecute_Click);
            }
        }

        protected override void Page_PreRender(object sender, EventArgs e)
        {
            _page = 1;
            _page = Converter.ToInt(GetParamter("page"));
            if (GetParamter("PinCode") != "")
            {
                txtPinCode.Text = GetParamter("PinCode");
            }
            if (GetParamter("Rate") != "")
            {
                ddlRate.SelectedValue = GetParamter("Rate");
            }
            if (GetParamter("Status") != "")
            {
                ddlStatus.SelectedValue = GetParamter("Status");
            }
            if (GetParamter("UsedAccount") != "")
            {
                txtUsedAccount.Text = GetParamter("UsedAccount");
            }
            if (_page <= 0) _page = 1;
            ListVoucherCode();
        }

        private void ListVoucherCode()
        {
            try
            {
                string linkFormat = "ManageVoucherCode.aspx?page={0}&PinCode={1}&Rate={2}&Status={3}&UsedAccount={4}";
                string strPinCode = txtPinCode.Text;
                string strRate = ddlRate.SelectedValue == "All" ? "" : ddlRate.SelectedValue;
                float rate = 0;
                float.TryParse(strRate, out rate);
                string strStatus = ddlStatus.SelectedValue == "All" ? "" : ddlStatus.SelectedValue;
                int status = 0;
                int.TryParse(strStatus, out status);
                string strUsedAccount = txtUsedAccount.Text;
                txtTotal.Text = "Số lượng: " + Lib.DataLayer.WebDB.GetTotalVoucherCode(strPinCode, rate.ToString(), status, strUsedAccount).ToString();
                Table table = new Table();
                TableRow rowHeader = new TableRow();
                table.CssClass = "table1";
                table.CellSpacing = 1;
                rowHeader.Cells.AddRange
                (
                    new TableCell[]
                    {
                        UIHelpers.CreateTableCell("ID",Unit.Percentage(10),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("PinCode",Unit.Percentage(15),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Rate", Unit.Percentage(15), HorizontalAlign.Left, "cellHeader"),
                        UIHelpers.CreateTableCell("Status", Unit.Percentage(15), HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("UsedAccount", Unit.Percentage(15),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("UsedDate", Unit.Percentage(20),HorizontalAlign.Left,"cellHeader")
                    }
                );
                table.Rows.Add(rowHeader);
                using (DataTable dt = Lib.DataLayer.WebDB.GetVoucherCode(_page, 50, strPinCode, rate.ToString(), status, strUsedAccount))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        TableRow row = new TableRow();
                        string _strStatus = "";
                        switch (Converter.ToInt(dr["Status"]))
                        {
                            case 1 :
                                _strStatus = "Chưa sử dụng";
                                break;
                            case 2:
                                _strStatus = "Đã sử dụng";
                                break;
                        }
                        string datetime = Converter.ToString(dr["UsedDate"]);
                        if (!string.IsNullOrEmpty(datetime))
                        {
                            datetime = string.Format("{0:dd/MM/yyyy HH:mm:ss}", Convert.ToDateTime(datetime));
                        }
                        row.Cells.AddRange
                        (
                            new TableCell[]
                            {
                                UIHelpers.CreateTableCell(Converter.ToString(dr["ID"]),HorizontalAlign.Left,"cell1"),
                                UIHelpers.CreateTableCell(Converter.ToString(dr["PinCode"]),HorizontalAlign.Left,"cell1"),
                                UIHelpers.CreateTableCell(Converter.ToString(dr["Rate"]),HorizontalAlign.Left,"cell1"),
                                UIHelpers.CreateTableCell(_strStatus,HorizontalAlign.Left,"cell1"),
                                UIHelpers.CreateTableCell(Converter.ToString(dr["UsedAccount"]),HorizontalAlign.Left,"cell1"),
                                UIHelpers.CreateTableCell(datetime,HorizontalAlign.Left,"cell1")
                            }
                        );
                        table.Rows.Add(row);
                    }
                }
                this.panelList.Controls.Clear();
                this.panelList.Controls.Add(table);
                this.linkPrev.NavigateUrl = string.Format(linkFormat, _page > 0 ? _page - 1 : 1, strPinCode, strRate, strStatus, strUsedAccount);
                this.linkNext.NavigateUrl = string.Format(linkFormat, _page + 1, strPinCode, strRate, strStatus, strUsedAccount);
            }
            catch (Exception ex)
            {
                Label labelMessage = new Label();
                labelMessage.Text = ex.Message;
                this.panelList.Controls.Add(labelMessage);
            }
        }

        protected void buttonExecute_Click(object sender, EventArgs e)
        {
            string linkFormat = "ManageVoucherCode.aspx?page={0}&PinCode={1}&Rate={2}&Status={3}&UsedAccount={4}";
            string strPinCode = txtPinCode.Text;
            string strRate = ddlRate.SelectedValue == "All" ? "" : ddlRate.SelectedValue;
            float rate = 0;
            float.TryParse(strRate, out rate);
            string strStatus = ddlStatus.SelectedValue == "All" ? "" : ddlStatus.SelectedValue;
            int status = 0;
            int.TryParse(strStatus, out status);
            string strUsedAccount = txtUsedAccount.Text;
            Response.Redirect(string.Format(linkFormat, _page, strPinCode, strRate, strStatus, strUsedAccount));
        }
    }
}
