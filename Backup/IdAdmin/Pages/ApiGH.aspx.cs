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
using IDAdmin.Lib.Utils;
using System.Timers;
using System.Globalization;

namespace IDAdmin.Pages
{
    public partial class ApiGH : Lib.UI.BasePage
    {
        string _ReturnURL = "";
        public const string hostName = HttpHelper.hostName;
        public const int port = HttpHelper.port;
        public ApiGH()
            : base(Lib.AppFunctions.APIGH)
        {
        }

        private void GoBack()
        {
            if (_ReturnURL == "")
            {
                Response.Redirect("Index.aspx", false);
            }
            else
            {
                Response.Redirect(_ReturnURL, false);
            }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsLogin())
            {
                //RedirectToLogOn();
            }
            else if (!CheckRight())
            {
                //RedirectToDeniedMessage();
            }
            else
            {
                if (!IsPostBack)
                {
                    MultiViewApiGH.ActiveViewIndex = 0;
                }
                if (MultiViewApiGH.ActiveViewIndex == 0)
                {
                    changeState();
                }
            }
        }

        private void changeState()
        {
            if (DropDownListReTypeView1.SelectedValue.Equals("7") == false)
            {
                txtItemIDView1.Enabled = false;
                DropDownListIsBindView1.Enabled = false;
                txtUnitView1.Enabled = false;
            }
            else
            {
                txtItemIDView1.Enabled = true;
                DropDownListIsBindView1.Enabled = true;
                txtUnitView1.Enabled = true;
            }
        }

        protected void buttonAcceptView1_Click(object sender, EventArgs e)
        {
            if (checkAcceptView1.Checked)
            {
                labelCheckMessageView1.Text = "";
                string gameType = txtGameTypeView1.Text;
                string zoneId = txtZoneIdView1.Text;
                string uId = txtUidView1.Text;
                string charId = txtCharIdView1.Text;
                string name = txtNameView1.Text;
                string reType = DropDownListReTypeView1.SelectedValue;
                string num = txtNumView1.Text;
                if (string.IsNullOrEmpty(gameType) ||
                    string.IsNullOrEmpty(zoneId) ||
                    string.IsNullOrEmpty(uId) ||
                    string.IsNullOrEmpty(charId) ||
                    string.IsNullOrEmpty(name) ||
                    string.IsNullOrEmpty(reType) ||
                    string.IsNullOrEmpty(num)
                    )
                {
                    labelMessageView1.Text = "Dữ liệu nhập không hợp lệ";
                    return;
                }

                //string hostName = "222.255.177.23";
                //string port = "19906";
                string url = "http://{0}:{1}/redeem?gametype={2}&zoneid={3}&uid={4}&char_id={5}&name={6}&retype={7}&num={8}";
                url = string.Format(url, hostName, port, gameType, zoneId, uId, charId, name, reType, num);

                if (reType.CompareTo("7") == 0)
                {

                    string itemId = txtItemIDView1.Text;
                    string isBind = DropDownListIsBindView1.SelectedValue;
                    string level = txtUnitView1.Text;
                    if (
                        string.IsNullOrEmpty(itemId) ||
                        string.IsNullOrEmpty(isBind)
                        )
                    {
                        labelMessageView1.Text = "Dữ liệu nhập không hợp lệ";
                        return;
                    }
                    url += "&itemID=" + itemId + "&isBind=" + isBind;
                    if (!string.IsNullOrEmpty(level))
                    {
                        url += "&level=" + level;
                    }
                }

                string result = HttpHelper.HttpSocket(url);
                int errorCode = 0;
                labelMessageView1.Text = message(result, ref errorCode);
                if (errorCode == 1)
                {
                    labelResultView1.Text = result.Substring(result.IndexOf("reid"));
                }
            }
            else
            {
                labelCheckMessageView1.Text = "[Chưa xác nhận]";
            }
        }

        protected void buttonAcceptView2_Click(object sender, EventArgs e)
        {
            if (checkAcceptView2.Checked)
            {
                labelCheckMessageView2.Text = "";
                string gameType = txtGameTypeView2.Text;
                string zoneId = txtZoneIdView2.Text;
                string accId = txtAccIdView2.Text;
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

                string result = HttpHelper.HttpSocket(url);
                int errorCode = 0;
                labelMessageView2.Text = message(result, ref errorCode);

            }
            else
            {
                labelCheckMessageView2.Text = "[Chưa xác nhận]";
            }
        }

        protected void buttonAcceptView3_Click(object sender, EventArgs e)
        {
            if (checkAcceptView3.Checked)
            {
                labelCheckMessageView3.Text = "";
                string gameType = txtGameTypeView3.Text;
                string zoneId = txtZoneIdView3.Text;
                string accId = txtAccIdView3.Text;
                string timeLimit = txtTimeLimitView3.Text;

                if (string.IsNullOrEmpty(gameType) ||
                    string.IsNullOrEmpty(zoneId) ||
                    string.IsNullOrEmpty(accId) ||
                    string.IsNullOrEmpty(timeLimit)
                    )
                {
                    labelMessageView3.Text = "Dữ liệu nhập không hợp lệ";
                    return;
                }

                //string hostName = "222.255.177.23";
                //string port = "19906";
                string url = "http://{0}:{1}/lockrole?gametype={2}&zoneid={3}&accid={4}&timelimit={5}";
                url = string.Format(url, hostName, port, gameType, zoneId, accId, timeLimit);

                string result = HttpHelper.HttpSocket(url);
                int errorCode = 0;
                labelMessageView3.Text = message(result, ref errorCode);
            }
            else
            {
                labelCheckMessageView3.Text = "[Chưa xác nhận]";
            }
        }

        protected void buttonAcceptView4_Click(object sender, EventArgs e)
        {
            if (checkAcceptView4.Checked)
            {
                labelCheckMessageView4.Text = "";
                string gameType = txtGameTypeView4.Text;
                string zoneId = txtZoneIdView4.Text;
                string accId = txtAccIdView4.Text;

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

                string result = HttpHelper.HttpSocket(url);
                int errorCode = 0;
                labelMessageView4.Text = message(result, ref errorCode);
            }
            else
            {
                labelCheckMessageView4.Text = "[Chưa xác nhận]";
            }
        }

        protected void buttonAcceptView5_Click(object sender, EventArgs e)
        {
            string zoneId = txtZoneIdView5.Text;

            if (string.IsNullOrEmpty(zoneId)
                )
            {
                labelMessageView5.Text = "Dữ liệu nhập không hợp lệ";
                return;
            }

            //string hostName = "222.255.177.23";
            //string port = "19906";
            string url = "http://{0}:{1}/getone?zoneid={2}";
            url = string.Format(url, hostName, port, zoneId);

            string result = HttpHelper.HttpSocket(url);
            int errorCode = 0;
            labelMessageView5.Text = message(result, ref errorCode);
        }

        protected void buttonAcceptView6_Click(object sender, EventArgs e)
        {
            //string hostName = "222.255.177.23";
            // string port = "19906";
            string url = "http://{0}:{1}/getone?zoneid=all";
            url = string.Format(url, hostName, port);

            string result = HttpHelper.HttpSocket(url);
            int errorCode = 0;
            labelMessageView6.Text = message(result, ref errorCode);
        }

        protected void buttonAcceptView7_Click(object sender, EventArgs e)
        {
            //string hostName = "222.255.177.23";
            //string port = "19906";
            string url = "http://{0}:{1}/getserverlist?zone=all";
            url = string.Format(url, hostName, port);

            string result = HttpHelper.HttpSocket(url);
            int errorCode = 0;
            labelMessageView7.Text = message(result, ref errorCode);
            txtServerList.Text = result;
        }

        protected void buttonAcceptView8_Click(object sender, EventArgs e)
        {
            string gameType = txtGameTypeView8.Text;
            string zoneId = txtZoneIdView8.Text;
            string type = DropDownListTypeView8.SelectedValue;
            string key = txtKeyView8.Text;

            if (string.IsNullOrEmpty(gameType) ||
                string.IsNullOrEmpty(zoneId) ||
                string.IsNullOrEmpty(type) ||
                string.IsNullOrEmpty(key)
                )
            {
                labelMessageView8.Text = "Dữ liệu nhập không hợp lệ";
                return;
            }

            //string hostName = "222.255.177.23";
            //string port = "19906";
            string url = "http://{0}:{1}/queryCharInfo?game={2}&zone={3}&type={4}&key={5}";
            url = string.Format(url, hostName, port);

            string result = HttpHelper.HttpSocket(url);
            int errorCode = 0;
            labelMessageView8.Text = message(result, ref errorCode);
        }
        protected void DropDownListCommandView9_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeStateView9();
        }

        private string getFormVariabel(string controlId)
        {
            string[] ctrls = Request.Form.ToString().Split('&');
            string value = "";
            foreach (string var in ctrls)
            {
                if (var.Contains(controlId))
                {
                    value = var.Substring(var.IndexOf(controlId)).Split('=')[1].Replace('+', ' ');
                }
            }
            return value;
        }

        private void changeStateView9()
        {
            if (DropDownListCommandView9.SelectedIndex == 0)
            {

                TextBox txtTypeId = new TextBox();
                txtTypeId.ID = "txtTypeId";
                //tbList.Add(txtTypeId);


                HtmlGenericControl typeIdText = new HtmlGenericControl("span");
                typeIdText.InnerText = "Đạo cụ";

                panelLeftView9.Controls.Add(typeIdText);
                panelLeftView9.Controls.Add(new HtmlGenericControl("br"));
                panelRightView9.Controls.Add(txtTypeId);
                panelRightView9.Controls.Add(new HtmlGenericControl("br"));

                TextBox txtNum = new TextBox();
                txtNum.ID = "txtNum";
                //tbList.Add(txtNum);


                HtmlGenericControl numText = new HtmlGenericControl("span");
                numText.InnerText = "Số lượng đạo cụ";

                panelLeftView9.Controls.Add(numText);
                panelRightView9.Controls.Add(txtNum);
            }

            if (DropDownListCommandView9.SelectedIndex == 1)
            {
                TextBox txtNum = new TextBox();
                txtNum.ID = "txtNum";
                //tbList.Add(txtNum);

                HtmlGenericControl numText = new HtmlGenericControl("span");
                numText.InnerText = "Số lượng:";

                panelLeftView9.Controls.Add(numText);
                panelRightView9.Controls.Add(txtNum);
            }

            if (DropDownListCommandView9.SelectedIndex == 2)
            {
                TextBox txtNum = new TextBox();
                txtNum.ID = "txtNum";
                //tbList.Add(txtNum);

                HtmlGenericControl numText = new HtmlGenericControl("span");
                numText.InnerText = "Số lượng:";

                panelLeftView9.Controls.Add(numText);
                panelRightView9.Controls.Add(txtNum);
            }

            if (DropDownListCommandView9.SelectedIndex == 3)
            {
                TextBox txtNum = new TextBox();
                txtNum.ID = "txtNum";
                //tbList.Add(txtNum);

                HtmlGenericControl numText = new HtmlGenericControl("span");
                numText.InnerText = "Cấp thêm:";

                panelLeftView9.Controls.Add(numText);
                panelRightView9.Controls.Add(txtNum);

            }

            if (DropDownListCommandView9.SelectedIndex == 4)
            {
                TextBox txtName = new TextBox();
                txtName.ID = "txtName";
                //tbList.Add(txtName);

                HtmlGenericControl nameText = new HtmlGenericControl("span");
                nameText.InnerText = "Tên nhân vật:";

                panelLeftView9.Controls.Add(nameText);
                panelRightView9.Controls.Add(txtName);

            }

            if (DropDownListCommandView9.SelectedIndex == 5)
            {
                TextBox txtName = new TextBox();
                txtName.ID = "txtName";
                //tbList.Add(txtName);

                HtmlGenericControl nameText = new HtmlGenericControl("span");
                nameText.InnerText = "Tên nhân vật:";

                panelLeftView9.Controls.Add(nameText);
                panelRightView9.Controls.Add(txtName);

                TextBox txtTime = new TextBox();
                txtTime.ID = "txtTime";
                //tbList.Add(txtTime);

                HtmlGenericControl timeText = new HtmlGenericControl("span");
                timeText.InnerText = "Số giây:";

                panelLeftView9.Controls.Add(timeText);
                panelRightView9.Controls.Add(txtTime);

            }
        }
        protected void buttonAcceptView9_Click(object sender, EventArgs e)
        {
            if (checkAcceptView9.Checked)
            {
                labelCheckMessageView9.Text = "";
                string gameType = txtGameTypeView9.Text;
                string zoneId = txtZoneIdView9.Text;
                string name = txtNameView9.Text;
                if (string.IsNullOrEmpty(gameType) ||
                   string.IsNullOrEmpty(zoneId) ||
                   string.IsNullOrEmpty(name)
                   )
                {
                    labelMessageView9.Text = "Dữ liệu nhập không hợp lệ";
                    return;
                }
                string command = "";
                if (DropDownListCommandView9.SelectedIndex == 0)
                {
                    string typeId = Request.Form["txtTypeId"];
                    string num = getFormVariabel("txtNum");
                    if (string.IsNullOrEmpty(typeId) || string.IsNullOrEmpty(num))
                    {
                        labelMessageView9.Text = "Dữ liệu nhập không hợp lệ";
                        return;
                    }
                    command = string.Format(DropDownListCommandView9.SelectedValue, typeId, num);
                }

                if (DropDownListCommandView9.SelectedIndex == 1)
                {
                    string num = getFormVariabel("txtNum");
                    if (string.IsNullOrEmpty(num))
                    {
                        labelMessageView9.Text = "Dữ liệu nhập không hợp lệ";
                        return;
                    }
                    command = string.Format(DropDownListCommandView9.SelectedValue, num);
                }

                if (DropDownListCommandView9.SelectedIndex == 2)
                {
                    string num = getFormVariabel("txtNum");
                    if (string.IsNullOrEmpty(num))
                    {
                        labelMessageView9.Text = "Dữ liệu nhập không hợp lệ";
                        return;
                    }
                    command = string.Format(DropDownListCommandView9.SelectedValue, num);
                }

                if (DropDownListCommandView9.SelectedIndex == 3)
                {
                    string num = getFormVariabel("txtNum");
                    if (string.IsNullOrEmpty(num))
                    {
                        labelMessageView9.Text = "Dữ liệu nhập không hợp lệ";
                        return;
                    }
                    command = string.Format(DropDownListCommandView9.SelectedValue, num);
                }

                if (DropDownListCommandView9.SelectedIndex == 4)
                {
                    string strName = getFormVariabel("txtName");
                    if (string.IsNullOrEmpty(strName))
                    {
                        labelMessageView9.Text = "Dữ liệu nhập không hợp lệ";
                        return;
                    }
                    command = string.Format(DropDownListCommandView9.SelectedValue, strName);
                }

                if (DropDownListCommandView9.SelectedIndex == 5)
                {
                    string strName = getFormVariabel("txtName");
                    string time = getFormVariabel("txtTime");
                    if (string.IsNullOrEmpty(strName) || string.IsNullOrEmpty(time))
                    {
                        labelMessageView9.Text = "Dữ liệu nhập không hợp lệ";
                        return;
                    }
                    command = string.Format(DropDownListCommandView9.SelectedValue, strName, time);
                }


                //string hostName = "222.255.177.23";
                //string port = "19906";
                string url = "http://{0}:{1}/commonWebCommand?game={2}&zone={3}&name={4}&command={5}";
                url = string.Format(url, hostName, port, gameType, zoneId, name, command);

                string result = HttpHelper.HttpSocket(url);
                int errorCode = 0;
                labelMessageView9.Text = message(result, ref errorCode);
            }
            else
            {
                labelCheckMessageView9.Text = "[Chưa xác nhận]";
            }
        }

        protected void buttonAcceptView10_Click(object sender, EventArgs e)
        {
            string gameType = txtGameTypeView10.Text;
            string zoneId = txtZoneIdView10.Text;
            string date = txtDateView10.Text;
            string endDate = txtEndDateView10.Text;

            if (string.IsNullOrEmpty(gameType) ||
                string.IsNullOrEmpty(zoneId) ||
                string.IsNullOrEmpty(date)
                )
            {
                labelMessageView10.Text = "Dữ liệu nhập không hợp lệ";
                return;
            }

            ///string hostName = "222.255.177.23";
            //string port = "19906";
            string url = "http://{0}:{1}/dayLoginCount?game={2}&zone={3}&date={4}&enddate={5}";
            url = string.Format(url, hostName, port, gameType, zoneId, date, endDate);

            string result = HttpHelper.HttpSocket(url);
            int errorCode = 0;
            labelMessageView10.Text = message(result, ref errorCode);
            dateLogicCount.Text = result;
        }

        protected void buttonAcceptView11_Click(object sender, EventArgs e)
        {
            if (checkAcceptView11.Checked)
            {
                labelCheckMessageView11.Text = "";
                string gameType = txtGameTypeView11.Text;
                string zoneId = txtZoneIdView11.Text;
                string content = txtContentView11.Text;
                string shutDownTime = txtShutDownTimeView11.Text;
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

                string result = HttpHelper.HttpSocket(url);
                int errorCode = 0;
                labelMessageView11.Text = message(result, ref errorCode);
            }
            else
            {
                labelCheckMessageView11.Text = "[Chưa xác nhận]";
            }
        }

        protected void buttonAcceptView12_Click(object sender, EventArgs e)
        {


            if (checkAcceptView12.Checked)
            {
                labelCheckMessageView12.Text = "";
                string gameType = txtGameTypeView12.Text;
                string zoneId = txtZoneIdView12.Text;
                string content = txtContentView12.Text;
                string effectTime = txtEffectTimeView12.Text;
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

                string result = HttpHelper.HttpSocket(url);
                int errorCode = 0;
                labelMessageView12.Text = message(result, ref errorCode);
            }
            else
            {
                labelCheckMessageView12.Text = "[Chưa xác nhận]";
            }
        }
        public static string[] convertStringToDateTime(string date)
        {
            string[] dateArrray = date.Split('/');
            //DateTime dt = new DateTime(int.Parse(dateArrray[2]), int.Parse(dateArrray[1]), int.Parse(dateArrray[0]));
            return dateArrray;
        }
        public static string[] convertTimeArray(string time)
        {
            string[] timeArray = time.Split(':');
            return timeArray;
        }
        void timerElapsed()//(object sender, ElapsedEventArgs e)
        {
            labelMessageView13.Text = "";
            if (DateTime.Now.CompareTo(DateTime.ParseExact(txtEndDateTime.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)) >= 0)
            {
                //  timer.Stop();
                UpdateTimer.Enabled = false;
                labelMessageView13.Text = "Quá trình thông báo kết thúc";
            }

            if (UpdateTimer.Enabled == true)
            {
                labelCheckMessageView13.Text = "";
                string gameType = txtGameTypeView13.Text;
                string zoneId = txtZoneIdView13.Text;
                string content = txtContentView13.Text;
                if (string.IsNullOrEmpty(gameType) ||
                    string.IsNullOrEmpty(zoneId) ||
                    string.IsNullOrEmpty(content)
                    )
                {
                    labelMessageView13.Text = "Dữ liệu nhập không hợp lệ";
                    return;
                }

                //string hostName = "222.255.177.23";
                // string port = "19906";
                string url = "http://{0}:{1}/normal_broadcast?gametype={2}&zoneid={3}&content={4}";
                url = string.Format(url, hostName, port, gameType, zoneId, HttpUtility.UrlEncode(content));

                string result = HttpHelper.HttpSocket(url);
                int errorCode = 0;

                labelMessageView13.Text = message(result, ref errorCode);
            }
            //else
            //{
            //labelCheckMessageView13.Text = "[Chưa xác nhận]";
            //}
        }

        protected void UpdateTimer_Tick(object sender, EventArgs e)
        {
            timerElapsed();
        }

        //System.Timers.Timer timer;
        protected void buttonAcceptView13_Click(object sender, EventArgs e)
        {
            //timer = new System.Timers.Timer(int.Parse(txtIntervalMinute.Text)*1000);
            //timer.Elapsed += new ElapsedEventHandler(timerElapsed); 
            //timer.Enabled = true;
            //timer.Stop();
            labelCheckMessageView13.Text = "";
            labelMessageView13.Text = "";
            if (txtContentView13.Text.Length <= 255)
            {
                if (checkAcceptView13.Checked)
                {
                    labelCheckMessageView13.Text = "";
                    UpdateTimer.Interval = int.Parse(txtIntervalMinute.Text) * 60000;
                    UpdateTimer.Enabled = true;
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
            // UpdateTimer.Tick += new EventHandler(timerElapsed);
        }

        protected void buttonAcceptView14_Click(object sender, EventArgs e)
        {
            if (checkAcceptView1.Checked)
            {
                labelCheckMessageView14.Text = "";
                string gameType = txtGameTypeView14.Text;
                string zoneId = txtZoneIdView14.Text;
                string uId = txtUidView14.Text;
                string charId = txtCharIdView14.Text;
                string name = txtNameView14.Text;
                string reType = DropDownListReTypeView14.SelectedValue;
                string num = txtNumView14.Text;
                string point = txtPointView14.Text;
                string source = txtSourceView14.Text;
                if (string.IsNullOrEmpty(gameType) ||
                    string.IsNullOrEmpty(zoneId) ||
                    string.IsNullOrEmpty(uId) ||
                    string.IsNullOrEmpty(charId) ||
                    string.IsNullOrEmpty(name) ||
                    string.IsNullOrEmpty(reType) ||
                    string.IsNullOrEmpty(num) ||
                     string.IsNullOrEmpty(point) ||
                     string.IsNullOrEmpty(source)
                    )
                {
                    labelMessageView14.Text = "Dữ liệu nhập không hợp lệ";
                    return;
                }

                //string hostName = "222.255.177.23";
                //string port = "19906";
                string url = "http://{0}:{1}/redeem_giftpacket?gametype={2}&zoneid={3}&uid={4}&char_id={5}&name={6}&retype={7}&num={8}&point={9}&source={10}";
                url = string.Format(url, hostName, port, gameType, zoneId, uId, charId, name, reType, num, point, source);

                if (reType.CompareTo("7") == 0)
                {

                    string itemId = txtItemIdView14.Text;
                    string isBind = DropDownListIsBindView14.SelectedValue;
                    string level = txtLevelView14.Text;
                    if (
                        string.IsNullOrEmpty(itemId) ||
                        string.IsNullOrEmpty(isBind)
                        )
                    {
                        labelMessageView1.Text = "Dữ liệu nhập không hợp lệ";
                        return;
                    }
                    url += "&itemID=" + itemId + "&isBind=" + isBind;
                    if (!string.IsNullOrEmpty(level))
                    {
                        url += "&level=" + level;
                    }
                }

                string result = HttpHelper.HttpSocket(url);
                int errorCode = 0;
                labelMessageView14.Text = message(result, ref errorCode);
            }
            else
            {
                labelCheckMessageView14.Text = "[Chưa xác nhận]";
            }
        }

        private void changeStateView14()
        {
            if (DropDownListReTypeView14.SelectedValue.Equals("7") == false)
            {
                txtItemIdView14.Enabled = false;
                DropDownListIsBindView14.Enabled = false;
                txtLevelView14.Enabled = false;
            }
            else
            {
                txtItemIdView14.Enabled = true;
                DropDownListIsBindView14.Enabled = true;
                txtLevelView14.Enabled = true;
            }
        }

        protected void DropDownListReTypeView14_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeStateView14();
        }

        protected void DropDownListFunction_SelectedIndexChanged(object sender, EventArgs e)
        {
            MultiViewApiGH.ActiveViewIndex = Int16.Parse(DropDownListFunction.SelectedValue);
        }

        protected void DropDownListReTypeView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeState();
        }

        protected void buttonCancelView1_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        protected void buttonCancelView2_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        protected void buttonCancelView3_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        protected void buttonCancelView4_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        protected void buttonCancelView11_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        protected void buttonCancelView12_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        protected void buttonCancelView13_Click(object sender, EventArgs e)
        {
            GoBack();
        }
    }
}
