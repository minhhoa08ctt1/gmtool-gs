using IDAdmin.Lib.DataLayer;
using IDAdmin.Lib.UI;
using IDAdmin.Lib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IDAdmin.Pages
{
    public partial class UnlockRole : BasePage
    {
        public const string hostName = "222.255.177.23";
        public const int port = 19906;
        public UnlockRole()
            : base(Lib.AppFunctions.UNLOCKROLE)
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
        }

        protected void buttonAcceptView4_Click(object sender, EventArgs e)
        {
            string gameType = txtGameTypeView4.Text;
            string zoneId = txtZoneIdView4.Text;
            string accId = txtAccIdView4.Text;

            WebDB.WriteLog(_User.UserName, Request.UserHostAddress, "ApiGH UnLock character: " + gameType + "," + zoneId + "," + accId);

            if (checkAcceptView4.Checked)
            {
                labelCheckMessageView4.Text = "";


                if (string.IsNullOrEmpty(gameType) ||
                    string.IsNullOrEmpty(zoneId) ||
                    string.IsNullOrEmpty(accId)
                    )
                {
                    labelMessageView4.Text = "Dữ liệu nhập không hợp lệ";
                    return;
                }

                //string hostName = "222.255.177.23";
                //string port = "19906";
                string url = "http://{0}:{1}/unlockrole?gametype={2}&zoneid={3}&accid={4}";
                url = string.Format(url, hostName, port, gameType, zoneId, accId);

                string result = HttpHelper.HttpSocket(url, 27);
                int errorCode = 0;
                labelMessageView4.Text = messageApi(result, ref errorCode);
                if (errorCode == 1)
                {

                }
            }
            else
            {
                labelCheckMessageView4.Text = "[Chưa xác nhận]";
            }

        }

    }
}