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
    public partial class LockRole : BasePage
    {
        public const string hostName = "222.255.177.23";
        public const int port = 19906;
        public LockRole()
            : base(Lib.AppFunctions.GETCHARINFO)
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

        protected void buttonAcceptView3_Click(object sender, EventArgs e)
        {
            string gameType = txtGameTypeView3.Text;
            string zoneId = txtZoneIdView3.Text;
            string accId = txtAccIdView3.Text;
            string timeLimit = txtTimeLimitView3.Text;

            WebDB.WriteLog(_User.UserName, Request.UserHostAddress, "ApiGH Lock character: " + gameType + "," + zoneId + "," + accId + "," + timeLimit);

            if (checkAcceptView3.Checked)
            {
                labelCheckMessageView3.Text = "";

                if (string.IsNullOrEmpty(gameType) ||
                    string.IsNullOrEmpty(zoneId) ||
                    string.IsNullOrEmpty(accId) ||
                    string.IsNullOrEmpty(timeLimit)
                    )
                {
                    labelMessageView3.Text = "Dữ liệu nhập không hợp lệ";
                    return;
                }

                string url = "http://{0}:{1}/lockrole?gametype={2}&zoneid={3}&accid={4}&timelimit={5}";
                url = string.Format(url, hostName, port, gameType, zoneId, accId, timeLimit);

                string result = HttpHelper.HttpSocket(url, 27);
                int errorCode = 0;
                labelMessageView3.Text = messageApi(result, ref errorCode);
                if (errorCode == 1)
                {

                }
            }
            else
            {
                labelCheckMessageView3.Text = "[Chưa xác nhận]";
            }

        }
    }
}