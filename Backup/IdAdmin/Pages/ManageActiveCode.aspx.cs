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
    public partial class ManageActiveCode : Lib.UI.BasePage
    {
        protected int _page;
        public ManageActiveCode()
            : base(Lib.AppFunctions.MANAGEACTIVECODE)
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
            if (GetParamter("Serial") != "")
            {
                txtSerial.Text = GetParamter("Serial");
            }
            if (GetParamter("Type") != "")
            {
                ddlType.SelectedValue = GetParamter("Type");
            }
            if (GetParamter("Kind") != "")
            {
                ddlKind.SelectedValue = GetParamter("Kind");
            }
            if (GetParamter("Turn") != "")
            {
                ddlTurn.SelectedValue = GetParamter("Turn");
            }
            if (GetParamter("Status") != "")
            {
                ddlStatus.SelectedValue = GetParamter("Status");
            }
            if (GetParamter("Account") != "")
            {
                txtAccount.Text = GetParamter("Account");
            }
            if (GetParamter("UsedAccount") != "")
            {
                txtUsedAccount.Text = GetParamter("UsedAccount");
            }
            if (_page <= 0) _page = 1;
            ListActiveCode();
        }

        private void ListActiveCode()
        {
            try
            {
                string linkFormat = "ManageActiveCode.aspx?page={0}&PinCode={1}&Serial={2}&Type={3}&Kind={4}&Turn={5}&Status={6}&Account={7}&UsedAccount={8}";
                string strPinCode = txtPinCode.Text;
                string strPinEncrypt = "";
                if (strPinCode != "")
                {
                    strPinEncrypt = Encrypt(strPinCode, "MR9tjjJBDB6bCmH3KxxzKDPNy38jk:23x1", true);
                }
                string strSerial = txtSerial.Text;
                string strType = ddlType.SelectedValue == "All" ? "" : ddlType.SelectedValue;
                string strKind = ddlKind.SelectedValue == "All" ? "" : ddlKind.SelectedValue;
                int strTurn = ddlTurn.SelectedValue == "All" ? 0 : Converter.ToInt(ddlTurn.SelectedValue);
                int strCodeStatus = ddlStatus.SelectedValue == "All" ? 0 : Converter.ToInt(ddlStatus.SelectedValue);
                string strAccount = txtAccount.Text;
                string strUsedAccount = txtUsedAccount.Text;
                txtTotal.Text = "Số lượng: " + Lib.DataLayer.WebDB.GetTotalActiveCode(strPinEncrypt, strSerial, strType, strKind, strTurn, strCodeStatus, strAccount, strUsedAccount).ToString();
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
                        UIHelpers.CreateTableCell("Serial", Unit.Percentage(15), HorizontalAlign.Left, "cellHeader"),
                        UIHelpers.CreateTableCell("Type", Unit.Percentage(10), HorizontalAlign.Left, "cellHeader"),
                        UIHelpers.CreateTableCell("Kind", Unit.Percentage(10),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Turn", Unit.Percentage(10),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Price", Unit.Percentage(10),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Status", Unit.Percentage(10), HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("Account", Unit.Percentage(15),HorizontalAlign.Left,"cellHeader"),
                        UIHelpers.CreateTableCell("UsedAccount", Unit.Percentage(15),HorizontalAlign.Left,"cellHeader")
                    }
                );
                table.Rows.Add(rowHeader);
                using (DataTable dt = Lib.DataLayer.WebDB.GetActiveCode(_page, 50, strPinEncrypt, strSerial, strType, strKind, strTurn, strCodeStatus, strAccount, strUsedAccount))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        TableRow row = new TableRow();
                        string strStatus = "";
                        switch (Converter.ToInt(dr["Status"]))
                        {
                            case 1 :
                                strStatus = "Chưa bán";
                                break;
                            case 2:
                                strStatus = "Đã bán";
                                break;
                            case 3:
                                strStatus = "Đã sử dụng";
                                break;
                        }
                        row.Cells.AddRange
                        (
                            new TableCell[]
                            {
                                UIHelpers.CreateTableCell(Converter.ToString(dr["CodeStoreID"]),HorizontalAlign.Right,"cell1"),
                                UIHelpers.CreateTableCell(Decrypt(Converter.ToString(dr["PinCode"]),"MR9tjjJBDB6bCmH3KxxzKDPNy38jk:23x1",true),HorizontalAlign.Left,"cell1"),
                                UIHelpers.CreateTableCell(Converter.ToString(dr["Serial"]),HorizontalAlign.Left,"cell1"),
                                UIHelpers.CreateTableCell(Converter.ToString(dr["Type"]),HorizontalAlign.Left,"cell1"),
                                UIHelpers.CreateTableCell(Converter.ToString(dr["Kind"]),HorizontalAlign.Left,"cell1"),
                                UIHelpers.CreateTableCell(Converter.ToString(dr["Turn"]),HorizontalAlign.Left,"cell1"),
                                UIHelpers.CreateTableCell(string.Format("{0:N0}",dr["Price"]),HorizontalAlign.Right,"cell1"),
                                UIHelpers.CreateTableCell(strStatus,HorizontalAlign.Left,"cell1"),
                                UIHelpers.CreateTableCell(Converter.ToString(dr["Account"]),HorizontalAlign.Left,"cell1"),
                                UIHelpers.CreateTableCell(Converter.ToString(dr["UsedAccount"]),HorizontalAlign.Left,"cell1")
                            }
                        );
                        table.Rows.Add(row);
                    }
                }
                this.panelList.Controls.Clear();
                this.panelList.Controls.Add(table);
                this.linkPrev.NavigateUrl = string.Format(linkFormat, _page > 0 ? _page - 1 : 1, strPinCode, strSerial, strType, strKind, strTurn, strCodeStatus,strAccount,strUsedAccount);
                this.linkNext.NavigateUrl = string.Format(linkFormat, _page + 1, strPinCode, strSerial, strType, strKind, strTurn, strCodeStatus, strAccount, strUsedAccount);
            }
            catch (Exception ex)
            {
                Label labelMessage = new Label();
                labelMessage.Text = ex.Message;
                this.panelList.Controls.Add(labelMessage);
            }
        }

        private string Encrypt(string toEncrypt, string key, bool useHashing)
        {
            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

                if (useHashing)
                {
                    System.Security.Cryptography.MD5CryptoServiceProvider hashmd5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                }
                else
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);

                System.Security.Cryptography.TripleDESCryptoServiceProvider tdes = new System.Security.Cryptography.TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = System.Security.Cryptography.CipherMode.ECB;
                tdes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

                System.Security.Cryptography.ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch
            {
                return "";
            }
        }

        private string Decrypt(string toDecrypt, string key, bool useHashing)
        {
            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

                if (useHashing)
                {
                    System.Security.Cryptography.MD5CryptoServiceProvider hashmd5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                }
                else
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);

                System.Security.Cryptography.TripleDESCryptoServiceProvider tdes = new System.Security.Cryptography.TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = System.Security.Cryptography.CipherMode.ECB;
                tdes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

                System.Security.Cryptography.ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch
            {
                return "";
            }
        }

        protected void buttonExecute_Click(object sender, EventArgs e)
        {
            string linkFormat = "ManageActiveCode.aspx?page={0}&PinCode={1}&Serial={2}&Type={3}&Kind={4}&Turn={5}&Status={6}&Account={7}&UsedAccount={8}";
            string strPinCode = txtPinCode.Text;
            string strSerial = txtSerial.Text;
            string strType = ddlType.SelectedValue == "All" ? "" : ddlType.SelectedValue;
            string strKind = ddlKind.SelectedValue == "All" ? "" : ddlKind.SelectedValue;
            int strTurn = ddlTurn.SelectedValue == "All" ? 0 : Converter.ToInt(ddlTurn.SelectedValue);
            int strCodeStatus = ddlStatus.SelectedValue == "All" ? 0 : Converter.ToInt(ddlStatus.SelectedValue);
            string strAccount = txtAccount.Text;
            string strUsedAccount = txtUsedAccount.Text;
            Response.Redirect(string.Format(linkFormat, _page, strPinCode, strSerial, strType, strKind, strTurn, strCodeStatus, strAccount, strUsedAccount));
        }
    }
}
