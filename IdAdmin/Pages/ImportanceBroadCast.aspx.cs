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
    public partial class ImportanceBroadCast : BasePage
    {
        public const string hostName = "222.255.177.23";
        public const int port = 19906;
        public ImportanceBroadCast()
            : base(Lib.AppFunctions.IMPORTANCEBROADCAST)
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

        protected void buttonAcceptView12_Click(object sender, EventArgs e)
        {
            string gameType = txtGameTypeView12.Text;
            string zoneId = txtZoneIdView12.Text;
            string content = txtContentView12.Text;
            string effectTime = txtEffectTimeView12.Text;

            WebDB.WriteLog(_User.UserName, Request.UserHostAddress, "ApiGH importance broadcast: " + gameType + "," + zoneId + "," + content + "," + effectTime);

            if (checkAcceptView12.Checked)
            {
                labelCheckMessageView12.Text = "";

                if (string.IsNullOrEmpty(gameType) ||
                    string.IsNullOrEmpty(zoneId) ||
                    string.IsNullOrEmpty(content) ||
                    string.IsNullOrEmpty(effectTime)
                    )
                {
                    labelMessageView12.Text = "Dữ liệu nhập không hợp lệ";
                    return;
                }

                //string hostName = "222.255.177.23";
                //string port = "19906";
                string url = "http://{0}:{1}/importance_broadcast?gametype={2}&zoneid={3}&content={4}&effecttime={5}";
                url = string.Format(url, hostName, port, gameType, zoneId, HttpUtility.UrlEncode(content), effectTime);

                string result = HttpHelper.HttpSocket(url, 27);
                int errorCode = 0;
                labelMessageView12.Text = messageApi(result, ref errorCode);
                if (errorCode == 1)
                {

                }
            }
            else
            {
                labelCheckMessageView12.Text = "[Chưa xác nhận]";
            }
        }
    }
}