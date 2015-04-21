using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IDAdmin.Lib.DataLayer;
using IDAdmin.Lib.UI;
using IDAdmin.Lib.Utils;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;

namespace IDAdmin.Pages
{
    public partial class AccountDetails : Lib.UI.BasePage
    {
        protected string _CustomerID;
        protected string _ReturnURL;
        private string wsUserName = "cuuam.id.ab.gosu.vn";
        private string wspassword = "CACK:rVLBY33hOfIZgRi1E4wxRCKxVJmZca4OU3JxdZcxwMv1zEhrR1usWiDCe";
        private string Signature = "";
        private string strAPI = "";
        private string result = "";
        JavaScriptSerializer js;
        public AccountDetails()
            : base(Lib.AppFunctions.ACCOUNT_DETAILS)
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
                _ReturnURL = Server.UrlDecode(GetParamter("returnURL"));

                if (!Page.IsPostBack)
                {
                    _CustomerID = GetParamter("id");
                    GetData();
                }
                label1.Text = "";
                this.buttonBack.Click += new EventHandler(buttonBack_Click);

                if (CheckRight(Lib.AppFunctions.ACCOUNT_EDIT))
                {
                    this.buttonSave.Click += new EventHandler(buttonSave_Click);
                    this.buttonChangePass.Click += new EventHandler(buttonChangePass_Click);
                    this.buttonConfirm.Click += new EventHandler(buttonConfirm_Click);
                    if (!Page.IsPostBack)
                    {
                        GetLockUnlockInfo();
                        bool allowChangePassword = CheckRight(Lib.AppFunctions.ACCOUNT_CHANGEPASS);
                        if (allowChangePassword)
                        {
                            buttonChangePass.Enabled = true;
                        }
                        else
                        {
                            buttonChangePass.Enabled = false;
                        }
                        Signature = Md5("cack" + txtUserName.Text + wsUserName + wspassword);
                        strAPI = "http://ab.gosu.vn/cuuamchankinh/validategameraccount?Gamer_account=" + txtUserName.Text + "&wsUserName=" + wsUserName + "&Signature=" + Signature;
                        result = HttpHelper.HttpGet(strAPI, 0);
                        js = new JavaScriptSerializer();
                        CACKQueryResult tempResult = js.Deserialize<CACKQueryResult>(result);
                        if (tempResult.code != "1")
                        {
                            txtStatus.Text = "Chưa kích hoạt tài khoản game";
                        }
                        else
                        {
                            Signature = Md5("cack" + txtUserName.Text + "7100020" + wsUserName + wspassword);
                            strAPI = "http://ab.gosu.vn/cuuamchankinh/querygameraccount?Gamer_account=" + txtUserName.Text + "&Team_id=7100020&wsUserName=" + wsUserName + "&Signature=" + Signature;
                            result = HttpHelper.HttpGet(strAPI, 0);
                            js = new JavaScriptSerializer();
                            CACKQueryResult tempResult1 = js.Deserialize<CACKQueryResult>(result);
                            txtStatus.Text = tempResult1.description;
                        }
                    }
                }
                else
                {
                    this.panelAccountPassword.Visible = false;
                    this.buttonSave.Visible = false;
                    this.panelGameAccount.Visible = false;
                }
            }
        }

        protected void buttonConfirm_Click(object sender, EventArgs e)
        {
            if (ddlSetting.SelectedValue == "1")
            {
                if (ddlReason.SelectedIndex == 0)
                {
                    label1.Text = "Bạn chưa chọn lý do khóa tài khoản";
                }
                else
                {
                    Signature = Md5("cack" + txtUserName.Text + wsUserName + wspassword);
                    strAPI = "http://ab.gosu.vn/cuuamchankinh/lockaccount?Gamer_account=" + txtUserName.Text + "&wsUserName=" + wsUserName + "&Signature=" + Signature;
                    result = HttpHelper.HttpGet(strAPI, 0);
                    js = new JavaScriptSerializer();
                    CACKQueryResult tempResult = js.Deserialize<CACKQueryResult>(result);
                    if (tempResult.code == "1")
                    {
                        txtStatus.Text = "Tài khoản đóng băng";
                        WebDB.LockUnlockAccount(txtUserName.Text, 1, ddlReason.SelectedItem.Text);
                        WebDB.WriteLog(_User.UserName, Request.UserHostAddress, "User_Lockaccount: " + txtCustomerID.Text);
                        GetLockUnlockInfo();
                    }
                    else
                    {
                        label1.Text = tempResult.msg;
                    }
                }
            }
            if (ddlSetting.SelectedValue == "2")
            {
                Signature = Md5("cack" + txtUserName.Text + wsUserName + wspassword);
                strAPI = "http://ab.gosu.vn/cuuamchankinh/unlockaccount?Gamer_account=" + txtUserName.Text + "&wsUserName=" + wsUserName + "&Signature=" + Signature;
                result = HttpHelper.HttpGet(strAPI, 0);
                js = new JavaScriptSerializer();
                CACKQueryResult tempResult = js.Deserialize<CACKQueryResult>(result);
                if (tempResult.code == "1")
                {
                    txtStatus.Text = "Tài khoản đã kích hoạt sử dụng";
                    WebDB.LockUnlockAccount(txtUserName.Text, 0, ddlReason.SelectedItem.Text);
                    WebDB.WriteLog(_User.UserName, Request.UserHostAddress, "User_Unlockaccount: " + txtCustomerID.Text);
                    GetLockUnlockInfo();
                }
                else
                {
                    label1.Text = tempResult.msg;
                }
            }
        }

        protected void buttonBack_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        private void GoBack()
        {
            if (_ReturnURL == "")
            {
                Response.Redirect("AccountList.aspx", false);
            }
            else
            {
                Response.Redirect(_ReturnURL, false);
            }
        }

        protected void GetData()
        {
            try
            {
                using (DataTable dt1 = WebDB.Location_Select())
                {
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        cmbLocation.Items.Add(new ListItem(dr1[Lib.Meta.LOCATION_NAME].ToString(), dr1[Lib.Meta.LOCATION_LOCATIONID].ToString()));
                    }
                }
                using (DataTable dt2 = WebDB.Question_Select())
                {
                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        cmbQuestion.Items.Add(new ListItem(dr2[Lib.Meta.QUESTION_QUESTION].ToString(), dr2[Lib.Meta.QUESTION_ID].ToString()));
                    }
                }

                DataRow dr = Lib.DataLayer.WebDB.User_Details(_CustomerID);

                if (dr != null)
                {
                    hiddenID.Value = dr[Lib.Meta.USER_ID].ToString();
                    txtCustomerID.Text = dr[Lib.Meta.USER_CUSTOMERID].ToString();
                    txtUserName.Text = dr[Lib.Meta.USER_USERNAME].ToString();
                    txtCreated.Text = string.Format("{0:dd/MM/yyyy HH:mm:ss}", dr[Lib.Meta.USER_CREATED]);
                    txtName.Text = dr[Lib.Meta.USER_NAME].ToString();
                    txtNumberID.Text = dr[Lib.Meta.USER_NUMBERID].ToString();
                    cmbLevel.SelectedValue = dr[Lib.Meta.USER_LEVEL].ToString();
                    txtEmail.Text = dr[Lib.Meta.USER_EMAIL].ToString();
                    txtPhoneNumber.Text = dr[Lib.Meta.USER_PHONENUMBER].ToString();
                    txtAddress.Text = dr[Lib.Meta.USER_ADDRESS].ToString();
                    txtBirthday.Text = string.Format("{0:dd/MM/yyyy}", dr[Lib.Meta.USER_BIRTHDAY]);
                    if (Converter.ToBoolean(dr[Lib.Meta.USER_GENDER]))
                    {
                        optNam.Checked = true;
                    }
                    else
                    {
                        optNu.Checked = true;
                    }
                    cmbLocation.SelectedValue = dr[Lib.Meta.USER_LOCATIONID].ToString();
                    cmbStatus.SelectedValue = dr[Lib.Meta.USER_STATUS].ToString();
                    cmbQuestion.SelectedValue = dr[Lib.Meta.USER_QUESTIONID].ToString();
                    txtAnswer.Text = dr[Lib.Meta.USER_ANSWER].ToString();
                    txtPassNow.Text = dr[Lib.Meta.USER_PASSWORD].ToString();
                    txtUpdated.Text = string.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["Updated"]);
                    txtUpdatedContent.Text = dr["Additional"].ToString();
                }
                else
                {
                    GoBack();
                }
            }
            catch
            {
                GoBack();
            }
        }

        private void GetLockUnlockInfo()
        {
            DataTable table = WebDB.GetLockUnlockAccount(txtUserName.Text);
            if (table.Rows.Count > 0)
            {
                string strReason = table.Rows[0]["Note"].ToString();
                string strStatus = table.Rows[0]["Status"].ToString();
                if (strStatus == "0")
                {
                    ddlSetting.SelectedIndex = 2;
                }
                else
                {
                    ddlSetting.SelectedIndex = 1;
                }
                switch (strReason)
                {
                    case "Hack BC":
                        ddlReason.SelectedIndex = 1;
                        break;
                    case "Rao link":
                        ddlReason.SelectedIndex = 2;
                        break;
                    case "Tạm thời khóa tranh chấp":
                        ddlReason.SelectedIndex = 3;
                        break;
                    case "Tạm khóa kiểm tra Hack":
                        ddlReason.SelectedIndex = 4;
                        break;
                    case "Khóa Vĩnh Viễn(Lý Do khác)":
                        ddlReason.SelectedIndex = 5;
                        break;
                }        
            }
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                long id = Converter.ToLong(hiddenID.Value);
                string customerID = txtCustomerID.Text;
                string email = txtEmail.Text.Trim();
                string numberID = txtNumberID.Text.Trim();
                string name = txtName.Text.Trim();
                int level = Converter.ToInt(cmbLevel.SelectedValue);
                string phoneNumber = txtPhoneNumber.Text;
                string address = txtAddress.Text;
                DateTime birthday = Converter.ToDateTime(txtBirthday.Text, new DateTime(1900, 1, 1));
                bool gender = optNam.Checked;
                int locationID = Converter.ToInt(cmbLocation.SelectedValue);
                int status = Converter.ToInt(cmbStatus.SelectedValue);
                int questionID = Converter.ToInt(cmbQuestion.SelectedValue);
                string answer = txtAnswer.Text;

                WebDB.User_ChangeInfo(_User.UserName,
                                      id,
                                      customerID,
                                      email,
                                      numberID,
                                      name,
                                      level,
                                      phoneNumber,
                                      address,
                                      birthday,
                                      gender,
                                      locationID,
                                      status,
                                      questionID,
                                      answer);

                WebDB.WriteLog(_User.UserName, Request.UserHostAddress, "User_ChangeInfo: " + customerID);

                GoBack();

            }
            catch (Exception ex)
            {
                labelInfoMessage.Text = string.Format("Lỗi: {0} ", ex.Message);
            }
        }

        protected void buttonChangePass_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMd5Password.Text.Trim() == "")
                {
                    if (txtPassword.Text.Trim() == "")
                    {
                        labelPassMessage.Text = "Mật khẩu phải khác rỗng";
                        return;
                    }
                    string pass1 = txtPassword.Text;
                    string pass2 = txtPassword2.Text;
                    if (pass1 != pass2)
                    {
                        labelPassMessage.Text = "Xác nhận mật khẩu không đúng";
                        return;
                    }
                    string encryptPass = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pass1, "MD5").ToLower();
                    string customerID = txtCustomerID.Text;

                    WebDB.User_ChangePassword(_User.UserName, customerID, encryptPass);
                    WebDB.WriteLog(_User.UserName, Request.UserHostAddress, "User_ChangePassword: " + customerID);

                    GoBack();
                }
                else
                {
                    string customerID = txtCustomerID.Text;
                    WebDB.User_ChangePassword(_User.UserName, customerID, txtMd5Password.Text);
                    WebDB.WriteLog(_User.UserName, Request.UserHostAddress, "User_ChangePassword: " + customerID);
                    GoBack();
                }
            }
            catch (Exception ex)
            {
                labelPassMessage.Text = string.Format("Lỗi: {0}", ex.Message);
            }
        }

        private string Md5(string s)
        {
            try
            {
                MD5 md5 = MD5CryptoServiceProvider.Create();
                byte[] dataMd5 = md5.ComputeHash(Encoding.Default.GetBytes(s));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < dataMd5.Length; i++)
                    sb.AppendFormat("{0:x2}", dataMd5[i]);
                return sb.ToString();
            }
            catch
            {
                return "";
            }
        }
    }
    public class CACKQueryResult
    {
        public string code { get; set; }
        public string msg { get; set; }
        public string data { get; set; }
        public string description { get; set; }
    }
}
