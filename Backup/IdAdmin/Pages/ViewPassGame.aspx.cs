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
using System.Web.Script.Serialization;
using System.Security.Cryptography;
using IDAdmin.Lib;

namespace IDAdmin.Pages
{
    public partial class ViewPassGame : Lib.UI.BasePage
    {
        public ViewPassGame()
            : base(Lib.AppFunctions.VIEWPASSGAME)
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
                buttonExecute.Click += new EventHandler(buttonExecute_Click);
            }
        }

        protected void buttonExecute_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Trim() != "")
            {
                string wsUserName = "cuuam.id.ab.gosu.vn";
                string wspassword = "CACK:rVLBY33hOfIZgRi1E4wxRCKxVJmZca4OU3JxdZcxwMv1zEhrR1usWiDCe";
                string Signature = Md5(txtUserName.Text.Trim() + wsUserName + wspassword);
                string strAPI = "http://ab.gosu.vn/Common/GetGamePassword?username=" + txtUserName.Text.Trim() + "&wsusername=" + wsUserName + "&signature=" + Signature;
                string result = HttpHelper.HttpGet(strAPI, 0);
                JavaScriptSerializer js = new JavaScriptSerializer();
                CACKQueryResult tempResult = js.Deserialize<CACKQueryResult>(result);
                txtPassword.Text = tempResult.msg;
                WebUser _User = new WebUser();
                _User.GetInfo();
                Lib.DataLayer.WebDB.WriteLog(_User.UserName, Request.UserHostAddress, "Xem password cua user: " + txtUserName.Text);
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
}
