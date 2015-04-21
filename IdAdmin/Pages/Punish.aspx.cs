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
    public partial class Punish : BasePage
    {
        public const string hostName = "222.255.177.23";
        public const int port = 19906;
        public Punish()
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

        protected void buttonAcceptView2_Click(object sender, EventArgs e)
        {
            string gameType = txtGameTypeView2.Text;
            string zoneId = txtZoneIdView2.Text;
            string accId = txtAccIdView2.Text;
            WebDB.WriteLog(_User.UserName, Request.UserHostAddress, "ApiGH Kick player out network: " + gameType + "," + zoneId + "," + accId);
            if (checkAcceptView2.Checked)
            {
                labelCheckMessageView2.Text = "";
                if (string.IsNullOrEmpty(gameType) ||
                    string.IsNullOrEmpty(zoneId) ||
                    string.IsNullOrEmpty(accId)
                    )
                {

                    labelMessageView2.Text = "Dữ liệu nhập không hợp lệ";
                    return;
                }

                //string hostName = "222.255.177.23";
                //string port = "19906";
                string url = "http://{0}:{1}/punish?gametype={2}&zoneid={3}&accid={4}";
                url = string.Format(url, hostName, port, gameType, zoneId, accId);

                string result = HttpHelper.HttpSocket(url, 27);
                int errorCode = 0;
                labelMessageView2.Text = messageApi(result, ref errorCode);
                if (errorCode == 1)
                {

                }
            }
            else
            {
                labelCheckMessageView2.Text = "[Chưa xác nhận]";
            }

        }

    }
}