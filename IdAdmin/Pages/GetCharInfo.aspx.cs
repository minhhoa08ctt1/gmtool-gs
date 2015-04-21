using IDAdmin.Lib.DataLayer;
using IDAdmin.Lib.UI;
using IDAdmin.Lib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IDAdmin.Pages
{
    public partial class GetCharInfo : BasePage
    {
        public GetCharInfo()
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

        protected void getCharIdButtonView15_Click(object sender, EventArgs e)
        {
            string gameType = gameTypeTextBoxView15.Text;
            string gameZone = gameZoneTextBoxView15.Text;
            string gosuAccount = gosuAccountTextBoxView15.Text;

            WebDB.WriteLog(_User.UserName, Request.UserHostAddress, "ApiGH get character info: " + gosuAccount + "," + gameType + "," + gameZone);

            if (string.IsNullOrEmpty(gameType) ||
                string.IsNullOrEmpty(gameZone) ||
                string.IsNullOrEmpty(gosuAccount)
                )
            {
                messageLabelView15.Text = "Dữ liệu nhập không hợp lệ";
                return;
            }

            string hostName = "222.255.177.27";
            string port = "9823";
            string url = "http://{0}:{1}/getCharidName?gosuAccount={2}&game={3}&zone={4}";
            url = string.Format(url, hostName, port, gosuAccount, gameType, gameZone);

            string result = HttpHelper.HttpSocket(url, 26);
            int errorCode = 0;

            if (string.IsNullOrEmpty(result) == false)
            {
                string code = result.Substring(0, 3);

                if (code.Contains("-12"))
                {
                    errorCode = -12;
                }
                else
                    if (code.Contains("-14"))
                    {
                        errorCode = -14;
                    }
                    else
                        if (code.Contains("10"))
                        {
                            errorCode = 10;
                        }
                        else
                            if (code.Contains("-10"))
                            {
                                errorCode = -10;
                            }
                            else
                                if (code.Contains("-1"))
                                {
                                    errorCode = -1;
                                }
                                else
                                    if (code.Contains("-2"))
                                    {
                                        errorCode = -2;
                                    }
                                    else
                                        if (code.Contains("-3"))
                                        {
                                            errorCode = -3;
                                        }
                                        else
                                            if (code.Contains("-5"))
                                            {
                                                errorCode = -5;
                                            }
                                            else
                                                if (code.Contains("-6"))
                                                {
                                                    errorCode = -6;
                                                }
                                                else
                                                    if (code.Contains("-7"))
                                                    {
                                                        errorCode = -7;
                                                    }
                                                    else
                                                        if (code.Contains("-9"))
                                                        {
                                                            errorCode = -9;
                                                        }

                switch (errorCode)
                {
                    case 0:
                        CharidName cin = new JavaScriptSerializer().Deserialize<CharidName>(result.Substring(2, result.IndexOf("}", result.IndexOf("name")) - 1));
                        messageLabelView15.Text = "0 thành công, " + "thông tin nhân vật là: " + "accid=" + cin.accid + " charid=" + cin.charid + " name=" + cin.name;
                        break;
                    case -1:
                        messageLabelView15.Text = "-1 lỗi cách thức http";
                        break;
                    case -2:
                        messageLabelView15.Text = "-2 ip yêu cầu không hợp lệ";
                        break;
                    case -3:
                        messageLabelView15.Text = "-3 lỗi hệ thống, yêu cầu gửi thất bại";
                        break;
                    case -5:
                        messageLabelView15.Text = "-5 lỗi tham số, thiếu tham số";
                        break;
                    case -6:
                        messageLabelView15.Text = "-6 lỗi cách thức accid，không hợp lệ";
                        break;
                    case -7:
                        messageLabelView15.Text = "-7 tên nhân vật chứa ký tự không hợp lệ";
                        break;
                    case -9:
                        messageLabelView15.Text = "-9 thất bại, yêu cầu thông tin tài khoản từ DBAccessServer thất bại";
                        break;
                    case -12:
                        messageLabelView15.Text = "-12 khu yêu cầu này, lưu lượng phút đã đầy";
                        break;
                    case -14:
                        messageLabelView15.Text = "-14 cụm này không tìm được nhận vật có gosu account như trên";
                        break;
                    case 10:
                        messageLabelView15.Text = "10 trong mysql database không tìm thấy nhân vật tương ứng với accid này.";
                        break;
                    case -10:
                        messageLabelView15.Text = "-10 Không tìm được bất cứ nhân vật nào";
                        break;
                }
            }
            else
            {
                messageLabelView15.Text = "Trả về chuỗi rỗng khi gọi api";
            }
        }
    }
}