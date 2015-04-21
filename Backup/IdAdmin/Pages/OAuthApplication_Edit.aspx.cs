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
    public partial class OAuthApplication_Edit : Lib.UI.BasePage 
    {
        protected string _action;
        protected string _clientId;
        protected string _returnURL;

        public OAuthApplication_Edit()
            : base(Lib.AppFunctions.OAUTH_MANAGER)
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
                _action = GetParamter("action");
                _clientId = GetParamter("clientid");
                _returnURL = HttpUtility.UrlDecode(GetParamter("returnURL"));

                if (!Page.IsPostBack)
                {
                    if (_action == "edit")
                    {
                        ViewData();
                    }
                    else if (_action == "add")
                    {
                    }
                    else
                    {
                        Response.Redirect(_returnURL, false);
                    }
                }
                this.buttonCancel.Click += new EventHandler(buttonCancel_Click);
                this.buttonSave.Click += new EventHandler(buttonSave_Click);                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ViewData()
        {
            try
            {
                DataRow dr = WebDB.OAuthApplication_Details(_clientId);
                if (dr != null)
                {
                    this.txtClientID.Text = dr["ClientID"].ToString();
                    this.txtApplicationName.Text = dr["ApplicationName"].ToString();
                    this.txtSecret.Text = dr["Secret"].ToString();
                    this.txtSite.Text = dr["Site"].ToString();
                    this.txtLogo.Text = dr["Logo"].ToString();
                    this.chkEnabled.Checked = Converter.ToBoolean(dr["Enabled"]) ? true : false;
                    this.chkAlwaysTrust.Checked = Converter.ToBoolean(dr["AlwaysTrust"]) ? true : false;
                    this.txtUserName.Text = dr["UserName"].ToString();
                    this.txtCssLink.Text = Converter.ToString(dr["CssLink"]);
                    this.txtPopupCssLink.Text = Converter.ToString(dr["PopupCssLink"]);
                    this.txtJsLink.Text = Converter.ToString(dr["JsLink"]);
                    this.txtRegSourceID.Text = Converter.ToString(dr["RegSourceID"]);

                    txtClientID.Enabled = false;                    

                    string redirectURIs = "";
                    using (DataTable dtRedirectURI = WebDB.OAuthApplication_SelectRedirectURI(_clientId))
                    {
                        foreach (DataRow drURI in dtRedirectURI.Rows)
                        {
                            redirectURIs += string.Format("{0}\n", drURI["RedirectURI"]);
                        }
                    }
                    txtRedirectURIs.Text = redirectURIs;

                }
                else
                {
                    Response.Redirect(_returnURL, false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void butUpdate_Click(object sender, EventArgs e)
        {
            Response.Redirect(_returnURL, false);
            ViewData();
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                string clientID = txtClientID.Text.Trim();                
                string applicationName = txtApplicationName.Text.Trim();                
                string secret = txtSecret.Text.Trim();
                string site = txtSite.Text.Trim();
                string logo = txtLogo.Text.Trim();
                bool enabled = chkEnabled.Checked;
                bool alwaysTrust = chkAlwaysTrust.Checked;
                string username = txtUserName.Text;
                string cssLink = txtCssLink.Text.Trim();
                string popupCssLink = txtPopupCssLink.Text.Trim();
                string jsLink = txtJsLink.Text.Trim();
                string regSourceID = txtRegSourceID.Text.Trim();

                string[] redirectURIs = txtRedirectURIs.Text.Split('\n');

                if (string.IsNullOrEmpty(clientID) ||
                    string.IsNullOrEmpty(applicationName) ||
                    string.IsNullOrEmpty(secret) ||
                    string.IsNullOrEmpty(username))
                { 
                    labelMessage.Text = "Thông tin nhập không đầy đủ!"; return;
                }

                if (!WebDB.User_Exists(username))
                {
                    labelMessage.Text = "Lỗi: Tài khoản sở hữu ứng dụng không tồn tại!";
                    return;
                }

                if (_action == "edit")
                {
                    WebDB.OAuthApplication_Update(username, clientID, secret, applicationName, site, logo, enabled, alwaysTrust, cssLink, popupCssLink, jsLink, regSourceID);
                    if (redirectURIs != null && redirectURIs.Length > 0)
                    {
                        WebDB.OAuthApplication_UpdateRedirectURIs(clientID, redirectURIs);
                    }
                    Response.Redirect(_returnURL, false);
                }
                else if (_action == "add")
                {
                    if (WebDB.OAuthApplication_Exists(clientID))
                    {
                        labelMessage.Text = "Lỗi: ClientID bị trùng!";
                        return;
                    }
                    WebDB.OAuthApplication_Insert(username, clientID, secret, applicationName, site, logo, enabled, alwaysTrust, cssLink, popupCssLink, jsLink, regSourceID);
                    if (redirectURIs != null && redirectURIs.Length > 0)
                    {
                        WebDB.OAuthApplication_UpdateRedirectURIs(clientID, redirectURIs);
                    }
                    Response.Redirect(string.Format("OAuthApplication.aspx?page=1&searchvalue={0}", clientID), false);
                }
                else
                {
                    Response.Redirect(_returnURL, false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void buttonCancel_Click(object sender, EventArgs e)
        {
            if (_returnURL != "")
            {
                Response.Redirect(_returnURL, false);
            }
            else
            {
                Response.Redirect("OAuthApplication.aspx", false);
            }
        }
    }
}
