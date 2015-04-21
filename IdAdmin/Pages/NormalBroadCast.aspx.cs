using IDAdmin.Lib.DataLayer;
using IDAdmin.Lib.UI;
using IDAdmin.Lib.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IDAdmin.Pages
{
    public partial class NormalBroadCast : BasePage
    {
        public const string hostName = "222.255.177.23";
        public const int port = 19906;
        public NormalBroadCast()
            : base(Lib.AppFunctions.NORMALBROADCAST)
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

        void timerElapsed()
        {
            labelMessageView13.Text = "";
            if (DateTime.Now.CompareTo(DateTime.ParseExact(txtEndDateTime.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)) >= 0)
            {
                UpdateTimer.Enabled = false;
                labelMessageView13.Text = "Quá trình thông báo kết thúc";
            }

            if (UpdateTimer.Enabled == true)
            {
                labelCheckMessageView13.Text = "";
                string gameType = txtGameTypeView13.Text;
                string zoneId = txtZoneIdView13.Text;
                string content = txtContentView13.Text;


                //string hostName = "222.255.177.23";
                // string port = "19906";
                string url = "http://{0}:{1}/normal_broadcast?gametype={2}&zoneid={3}&content={4}";
                url = string.Format(url, hostName, port, gameType, zoneId, HttpUtility.UrlEncode(content));

                string result = HttpHelper.HttpSocket(url, 27);
                int errorCode = 0;

                labelMessageView13.Text = messageApi(result, ref errorCode);
                if (errorCode != 1)
                {
                    UpdateTimer.Enabled = false;
                }
            }
        }

        protected void UpdateTimer_Tick(object sender, EventArgs e)
        {
            timerElapsed();
        }

        protected void buttonAcceptView13_Click(object sender, EventArgs e)
        {
            UpdateTimer.Enabled = false;
            WebDB.WriteLog(_User.UserName, Request.UserHostAddress, "ApiGH normal broadcast: " + txtGameTypeView13.Text + "," + txtZoneIdView13.Text + "," + txtContentView13.Text + "," + UpdateTimer.Interval + "," + txtEndDateTime.Text);

            labelCheckMessageView13.Text = "";
            labelMessageView13.Text = "";

            if (txtContentView13.Text.Length <= 255)
            {
                if (checkAcceptView13.Checked)
                {
                    string gameType = txtGameTypeView13.Text;
                    string zoneId = txtZoneIdView13.Text;
                    string content = txtContentView13.Text;

                    if (string.IsNullOrEmpty(gameType) ||
                       string.IsNullOrEmpty(zoneId) ||
                       string.IsNullOrEmpty(content) ||
                        string.IsNullOrEmpty(txtIntervalMinute.Text) ||
                        string.IsNullOrEmpty(txtEndDateTime.Text)
                        )
                    {
                        labelMessageView13.Text = "Dữ liệu nhập không hợp lệ";
                        return;
                    }

                    if (txtIntervalMinute.Text.All(char.IsDigit) == false)
                    {
                        labelMessageView13.Text = "Bao nhiêu phút thì thông báo một lần phải là số";
                        return;
                    }

                    try
                    {
                        DateTime.ParseExact(txtEndDateTime.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    }
                    catch (Exception ex)
                    {
                        labelMessageView13.Text = "Thời điểm kết thúc sai định dạng";
                        return;
                    }

                    if (DateTime.Now.CompareTo(DateTime.ParseExact(txtEndDateTime.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)) >= 0)
                    {
                        labelMessageView13.Text = "Thời điểm kết thúc trước thời điểm hiện tại";
                        return;
                    }
                    labelCheckMessageView13.Text = "";
                    UpdateTimer.Interval = int.Parse(txtIntervalMinute.Text) * 60000;
                    UpdateTimer.Enabled = true;
                    timerElapsed();
                }
                else
                {
                    labelCheckMessageView13.Text = "[Chưa xác nhận]";
                }
            }
            else
            {
                labelMessageView13.Text = "Số ký tự của nội dung thông báo là " + txtContentView13.Text.Length + " vượt quá mức cho phép";
            }
        }

    }
}