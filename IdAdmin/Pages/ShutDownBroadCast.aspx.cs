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
    public partial class ShutDownBroadCast : BasePage
    {
        public const string hostName = "222.255.177.23";
        public const int port = 19906;
        public ShutDownBroadCast()
            : base(Lib.AppFunctions.SHUTDOWNBROADCAST)
        {
 
        }
        protected override  void Page_Load(object sender, EventArgs e)
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

        protected void buttonAcceptView11_Click(object sender, EventArgs e)
        {
            string gameType = txtGameTypeView11.Text;
            string zoneId = txtZoneIdView11.Text;
            string content = txtContentView11.Text;
            string shutDownTime = txtShutDownTimeView11.Text;

            WebDB.WriteLog(_User.UserName, Request.UserHostAddress, "ApiGH shutdown broadcast: " + gameType + "," + zoneId + "," + content + "," + shutDownTime);

            if (checkAcceptView11.Checked)
            {
                labelCheckMessageView11.Text = "";

                if (string.IsNullOrEmpty(gameType) ||
                    string.IsNullOrEmpty(zoneId) ||
                    string.IsNullOrEmpty(content) ||
                    string.IsNullOrEmpty(shutDownTime)
                    )
                {
                    labelMessageView11.Text = "Dữ liệu nhập không hợp lệ";
                    return;
                }

                //string hostName = "222.255.177.23";
                //string port = "19906";
                string url = "http://{0}:{1}/shutdown_broadcast?gametype={2}&zoneid={3}&content={4}&shutdowntime={5}";
                url = string.Format(url, hostName, port, gameType, zoneId, HttpUtility.UrlEncode(content), shutDownTime);

                string result = HttpHelper.HttpSocket(url, 27);
                int errorCode = 0;
                labelMessageView11.Text = messageApi(result, ref errorCode);
                if (errorCode == 1)
                {

                }
            }
            else
            {
                labelCheckMessageView11.Text = "[Chưa xác nhận]";
            }
        }
    }
}