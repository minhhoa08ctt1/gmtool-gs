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
using IDAdmin.Lib.DataLayer;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace IDAdmin.Pages
{
    public partial class AdminCACK : Lib.UI.BasePage
    {
        private CuuAmChanKinh _GameCACK = new CuuAmChanKinh();
        public AdminCACK()
            : base(Lib.AppFunctions.ADMINCACK)
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
            else if (AppManager.GameID != "cack")
            {
                Response.Redirect("Index.aspx", false);
            }
            else
            {
                if (!IsPostBack)
                {
                    txtCaptcha.Text = "";
                    Session[Lib.SessionName.CAPTCHA] = Lib.CaptchaImage.GetCaptchaString();
                    LoadListServer();
                }
                if (ddlType.SelectedIndex == 0)
                {
                    lblName.Text = "Nhập ID vật phẩm/Số lượng";
                }
                if (ddlType.SelectedIndex == 1)
                {
                    lblName.Text = "Nhập số vàng";
                }
                if (ddlType.SelectedIndex == 2)
                {
                    lblName.Text = "Nhập số ngày";
                }
                labelMessage.Text = "";
                buttonAdd.Click += new EventHandler(buttonAdd_Click);
            }
        }

        private void LoadListServer()
        {
            this.ddlServer.DataValueField = Lib.Meta.SERVER_NAME;
            this.ddlServer.DataTextField = Lib.Meta.SERVER_FULLNAME;
            this.ddlServer.DataSource = WebDB.Server_Select(false);
            this.ddlServer.DataBind();
            this.ddlServer.SelectedIndex = 0;
        }

        protected void buttonAdd_Click(object sender, EventArgs e)
        {
            Lib.WebUser _user = new IDAdmin.Lib.WebUser();
            _user.GetInfo();
            if (txtCaptcha.Text != Session[Lib.SessionName.CAPTCHA].ToString())
            {
                labelMessage.ForeColor = System.Drawing.Color.Red;
                labelMessage.Text = "Mã bảo vệ không đúng!";
                txtCaptcha.Text = "";
                Session[Lib.SessionName.CAPTCHA] = Lib.CaptchaImage.GetCaptchaString();
                return;
            }
            string username = txtAccount.Text.Trim();
            if (username == "")
            {
                labelMessage.Text = "Chưa nhập tên tài khoản nhận";
                labelMessage.ForeColor = System.Drawing.Color.Red;
                Session[Lib.SessionName.CAPTCHA] = Lib.CaptchaImage.GetCaptchaString();
                return;
            }
            if (!WebDB.User_Exists(username))
            {
                labelMessage.Text = "Tài khoản nhận không tồn tại";
                labelMessage.ForeColor = System.Drawing.Color.Red;
                Session[Lib.SessionName.CAPTCHA] = Lib.CaptchaImage.GetCaptchaString();
                return;
            }
            string amount = txtAmount.Text.Trim();
            if (amount == "")
            {
                labelMessage.Text = "Bạn chưa nhập ID vật phẩm hoặc số lượng";
                labelMessage.ForeColor = System.Drawing.Color.Red;
                Session[Lib.SessionName.CAPTCHA] = Lib.CaptchaImage.GetCaptchaString();
                return;
            }
            //Add Item
            if (ddlType.SelectedIndex == 0)
            {
                string exchangeID = string.Format("{0}{1}", "EVENT000_", DateTime.Now.Ticks.ToString().PadLeft(15, '0'));
                if (amount != "")
                {
                    GoldTransferResult addGoldResult = _GameCACK.SendPresent(username, exchangeID, ddlServer.SelectedItem.Value, amount);
                    labelMessage.Text = addGoldResult.Message;
                    if (addGoldResult.ResponseCode == "1")
                    {
                        labelMessage.ForeColor = System.Drawing.Color.Blue;
                        Lib.DataLayer.WebDB.Write_CACK_EventLog(username, ddlServer.SelectedItem.Value, "Add Item : " + amount + " cho user: " + username + "_ExchangeID: " + exchangeID + " từ user: " + _user.UserName);
                    }
                    else
                    {
                        labelMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    Session[Lib.SessionName.CAPTCHA] = Lib.CaptchaImage.GetCaptchaString();
                }
            }
            //Add Gold
            if (ddlType.SelectedIndex == 1)
            {
                string exchangeID = string.Format("{0}{1}", "EVENT000_", DateTime.Now.Ticks.ToString().PadLeft(15, '0'));
                int gold = Converter.ToInt(amount);
                if (gold > 0)
                {
                    GoldTransferResult addGoldResult = _GameCACK.GoldTransfer(username, exchangeID,
                                                                                          gold,
                                                                                          gold * 3000, ddlServer.SelectedItem.Value);
                    labelMessage.Text = addGoldResult.Message;
                    if (addGoldResult.ResponseCode == "1")
                    {
                        labelMessage.ForeColor = System.Drawing.Color.Blue;
                        Lib.DataLayer.WebDB.Write_CACK_EventLog(username, ddlServer.SelectedItem.Value, "Nạp " + amount + " vàng cho user: " + username + "_ExchangeID: " + exchangeID + " từ user: " + _user.UserName);
                    }
                    else
                    {
                        labelMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    Session[Lib.SessionName.CAPTCHA] = Lib.CaptchaImage.GetCaptchaString();
                }
            }
            //Add Vip
            if (ddlType.SelectedIndex == 2)
            {
                string exchangeID = string.Format("{0}{1}", "EVENT000_", DateTime.Now.Ticks.ToString().PadLeft(15, '0'));
                int day = Converter.ToInt(amount);
                int weeks = 0;
                if (day > 0)
                {
                    int money = 7000 * day;
                    switch (day)
                    {
                        case 7 :
                            money = 50000;
                            weeks = 1;
                            break;
                        case 14:
                            money = 95000;
                            weeks = 2;
                            break;
                        case 28:
                            money = 180000;
                            weeks = 4;
                            break;
                    }
                    GoldTransferResult addGoldResult;
                    if (weeks == 0)
                    {
                        addGoldResult = _GameCACK.AddMinJunByDays(username, exchangeID, ddlServer.SelectedItem.Value,
                                                                          day,
                                                                          money);
                    }
                    else
                    {
                        addGoldResult = _GameCACK.AddMinJun(username, exchangeID, ddlServer.SelectedItem.Value, weeks, money);
                    }
                    labelMessage.Text = addGoldResult.Message;
                    if (addGoldResult.ResponseCode == "1")
                    {
                        labelMessage.ForeColor = System.Drawing.Color.Blue;
                        Lib.DataLayer.WebDB.Write_CACK_EventLog(username, ddlServer.SelectedItem.Value, "Nạp " + amount + " ngày danh tuấn cho user: " + username + "_ExchangeID: " + exchangeID + " từ user: " + _user.UserName);
                    }
                    else
                    {
                        labelMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    Session[Lib.SessionName.CAPTCHA] = Lib.CaptchaImage.GetCaptchaString();
                }
                
                
            }
        }

        public class CuuAmChanKinh
        {
            private System.Web.Script.Serialization.JavaScriptSerializer _jsSerialize = new System.Web.Script.Serialization.JavaScriptSerializer();
            public const string GAME_ID = "cack";                   //Mã game (dùng giao tiếp với ab.gosu.vn)
            public const string PAYPURPOSE_ADDGOLD = "addgold";     //Mục đích nạp tiền: nạp vàng vào game
            public const string PAYPURPOSE_ADDMINJUN = "addminjun"; //Mục đích nạp tiền: nạp mua thẻ Danh tuấn giang hồ
            private string _wsUserName = "cuuam.id.ab.gosu.vn";
            private string _wsPassword = "CACK:rVLBY33hOfIZgRi1E4wxRCKxVJmZca4OU3JxdZcxwMv1zEhrR1usWiDCe";
            private string _API_URL = "http://ab.gosu.vn";

            /// <summary>
            /// Nạp tiền vào game
            /// </summary>
            /// <param name="Account"></param>
            /// <param name="ServerName"></param>
            /// <param name="ExchangeID"></param>
            /// <param name="Gold"></param>
            /// <param name="Money"></param>
            /// <returns></returns>
            public GoldTransferResult GoldTransfer(string Account, string ExchangeID, int Gold, int Amount, string ServerName)
            {
                GoldTransferResult result = new GoldTransferResult();
                try
                {
                    GameServerModel serverObj = GameServerModel.DetailsByName(ServerName, GAME_ID);
                    if (serverObj != null)
                    {
                        Account = Account.ToLower();
                        string[] arr = serverObj.ServerIdentityName.Split('_');
                        string timespan = Converter.ToUnixTimestamp(DateTime.Now).ToString();
                        string signature = (GAME_ID + Account + ExchangeID + timespan + Gold.ToString() + Amount.ToString() + arr[0] + _wsUserName + _wsPassword).Md5();
                        string goldTransferLink = string.Format("{0}/CuuAmChanKinh/Recharge?gamer_account={1}&exchange_id={2}&timespan={3}&gold={4}&amount={5}&team_id={6}&wsusername={7}&signature={8}",
                                                serverObj.DNS,
                                                Account,
                                                ExchangeID,
                                                timespan,
                                                Gold,
                                                Amount,
                                                arr[0],
                                                _wsUserName,
                                                signature);
                        string responseResult = HttpHelper.HttpGet(goldTransferLink, 0);
                        CACK_APIResult resultObj = _jsSerialize.Deserialize<CACK_APIResult>(responseResult);
                        if (resultObj == null)
                        {
                            result.ResponseCode = "-999";
                            result.Message = "Hệ thống tạm thời gián đoạn";
                            result.Succeed = false;
                        }
                        else if (resultObj.code.Trim() == "1")
                        {
                            result.ResponseCode = resultObj.code;
                            result.Message = "Nạp vàng thành công";
                            result.Succeed = true;
                        }
                        else
                        {
                            result.ResponseCode = resultObj.code;
                            result.Message = CACK_Messages.GetMessage(result.ResponseCode);
                            result.Succeed = false;
                        }
                    }
                    else
                    {
                        result.ResponseCode = "-999";
                        result.Message = "Máy chủ game không tồn tại";
                        result.Succeed = false;
                    }
                }
                catch (Exception ex)
                {
                    result.ResponseCode = "-999";
                    result.Message = "Hệ thống tạm thời gián đoạn";
                    result.Succeed = false;
                }
                return result;
            }

            /// <summary>
            /// Nạp thẻ Danh tuấn giang hồ
            /// </summary>
            /// <param name="Account"></param>
            /// <param name="ExchangeID"></param>
            /// <param name="ServerName"></param>
            /// <param name="Weeks"></param>
            /// <param name="Amount"></param>
            /// <returns></returns>
            public GoldTransferResult AddMinJun(string Account, string ExchangeID, string ServerName, int Weeks, int Amount)
            {
                GoldTransferResult result = new GoldTransferResult();
                try
                {
                    GameServerModel serverObj = GameServerModel.DetailsByName(ServerName, GAME_ID);
                    if (serverObj != null)
                    {
                        Account = Account.ToLower();
                        string timespan = Converter.ToUnixTimestamp(DateTime.Now).ToString();
                        string[] arr = serverObj.ServerIdentityName.Split('_');
                        string signature = (GAME_ID + Account + ExchangeID + timespan + arr[0] + arr[1] + Weeks.ToString() + Amount.ToString() + _wsUserName + _wsPassword).Md5();
                        string link = string.Format("{0}/CuuAmChanKinh/AddMinJun?gamer_account={1}&exchange_id={2}&timespan={3}&team_id={4}&server_id={5}&weeks={6}&amount={7}&wsusername={8}&signature={9}",
                                                    serverObj.DNS,
                                                    Account,
                                                    ExchangeID,
                                                    timespan,
                                                    arr[0],
                                                    arr[1],
                                                    Weeks,
                                                    Amount,
                                                    _wsUserName,
                                                    signature);
                        string responseResult = HttpHelper.HttpGet(link, 0);
                        CACK_APIResult resultObj = _jsSerialize.Deserialize<CACK_APIResult>(responseResult);
                        if (resultObj == null)
                        {
                            result.ResponseCode = "-999";
                            result.Message = "Hệ thống tạm thời gián đoạn";
                            result.Succeed = false;
                        }
                        else if (resultObj.code.Trim() == "1")
                        {
                            result.ResponseCode = resultObj.code;
                            result.Message = "Nạp danh tuấn thành công";
                            result.Succeed = true;
                        }
                        else
                        {
                            result.ResponseCode = resultObj.code;
                            result.Message = CACK_Messages.GetMessage(result.ResponseCode);
                            result.Succeed = false;
                        }

                    }
                    else
                    {
                        result.ResponseCode = "-999";
                        result.Message = "Máy chủ game không tồn tại";
                        result.Succeed = false;
                    }
                }
                catch (Exception ex)
                {
                    result.ResponseCode = "-999";
                    result.Message = "Hệ thống tạm thời gián đoạn";
                    result.Succeed = false;
                }
                return result;
            }

            /// <summary>
            /// Nạp thẻ Danh tuấn giang hồ theo ngày
            /// </summary>
            /// <param name="Account"></param>
            /// <param name="ExchangeID"></param>
            /// <param name="ServerName"></param>
            /// <param name="Weeks"></param>
            /// <param name="Amount"></param>
            /// <returns></returns>
            public GoldTransferResult AddMinJunByDays(string Account, string ExchangeID, string ServerName, int Days, int Amount)
            {
                GoldTransferResult result = new GoldTransferResult();
                try
                {
                    GameServerModel serverObj = GameServerModel.DetailsByName(ServerName, GAME_ID);
                    if (serverObj != null)
                    {
                        Account = Account.ToLower();
                        string timespan = Converter.ToUnixTimestamp(DateTime.Now).ToString();
                        string[] arr = serverObj.ServerIdentityName.Split('_');
                        string signature = (GAME_ID + Account + ExchangeID + timespan + arr[0] + arr[1] + Days.ToString() + Amount.ToString() + _wsUserName + _wsPassword).Md5();
                        string link = string.Format("{0}/CuuAmChanKinh/AddMinJunByDays?gamer_account={1}&exchange_id={2}&timespan={3}&team_id={4}&server_id={5}&days={6}&amount={7}&wsusername={8}&signature={9}",
                                                    serverObj.DNS,
                                                    Account,
                                                    ExchangeID,
                                                    timespan,
                                                    arr[0],
                                                    arr[1],
                                                    Days,
                                                    Amount,
                                                    _wsUserName,
                                                    signature);
                        string responseResult = HttpHelper.HttpGet(link, 0);
                        CACK_APIResult resultObj = _jsSerialize.Deserialize<CACK_APIResult>(responseResult);
                        if (resultObj == null)
                        {
                            result.ResponseCode = "-999";
                            result.Message = "Hệ thống tạm thời gián đoạn";
                            result.Succeed = false;
                        }
                        else if (resultObj.code.Trim() == "1")
                        {
                            result.ResponseCode = resultObj.code;
                            result.Message = "Nạp danh tuấn thành công";
                            result.Succeed = true;
                        }
                        else
                        {
                            result.ResponseCode = resultObj.code;
                            result.Message = CACK_Messages.GetMessage(result.ResponseCode);
                            result.Succeed = false;
                        }

                    }
                    else
                    {
                        result.ResponseCode = "-999";
                        result.Message = "Máy chủ game không tồn tại";
                        result.Succeed = false;
                    }
                }
                catch (Exception ex)
                {
                    result.ResponseCode = "-999";
                    result.Message = "Hệ thống tạm thời gián đoạn";
                    result.Succeed = false;
                }
                return result;
            }

            public GoldTransferResult SendPresent(string Account, string ExchangeID, string ServerName, string items_info)
            {
                GoldTransferResult result = new GoldTransferResult();
                try
                {
                    GameServerModel serverObj = GameServerModel.DetailsByName(ServerName, GAME_ID);
                    if (serverObj != null)
                    {
                        Account = Account.ToLower();
                        string[] arr = serverObj.ServerIdentityName.Split('_');
                        string signature = (GAME_ID + Account + ExchangeID + arr[0] + arr[1] + _wsUserName + _wsPassword).Md5();
                        string goldTransferLink = string.Format("{0}/CuuAmChanKinh/SendPresent?gamer_account={1}&exchange_id={2}&team_id={3}&server_id={4}&items_info={5}&wsUserName={6}&signature={7}",
                                                serverObj.DNS,
                                                Account,
                                                ExchangeID,
                                                arr[0],
                                                arr[1],
                                                items_info,
                                                _wsUserName,
                                                signature);
                        string responseResult = HttpHelper.HttpGet(goldTransferLink, 0);
                        CACK_APIResult resultObj = _jsSerialize.Deserialize<CACK_APIResult>(responseResult);
                        if (resultObj == null)
                        {
                            result.ResponseCode = "-999";
                            result.Message = "Hệ thống tạm thời gián đoạn";
                            result.Succeed = false;
                        }
                        else if (resultObj.code.Trim() == "1")
                        {
                            result.ResponseCode = resultObj.code;
                            result.Message = "Thêm vật phẩm thành công";
                            result.Succeed = true;
                        }
                        else
                        {
                            result.ResponseCode = resultObj.code;
                            result.Message = CACK_Messages.GetMessage(result.ResponseCode);
                            result.Succeed = false;
                        }
                    }
                    else
                    {
                        result.ResponseCode = "-999";
                        result.Message = "Máy chủ game không tồn tại";
                        result.Succeed = false;
                    }
                }
                catch (Exception ex)
                {
                    result.ResponseCode = "-999";
                    result.Message = "Hệ thống tạm thời gián đoạn";
                    result.Succeed = false;
                }
                return result;
            }
        }

        public class GoldTransferResult
        {
            public GoldTransferResult()
            {
                this.Succeed = false;
                this.ResponseCode = "-999";
                this.Message = "Lỗi. Không nạp được tiền";
            }

            public bool Succeed { get; set; }
            public string ResponseCode { get; set; }
            public string Message { get; set; }
        }

        public class GameServerModel
        {
            public int ServerID { get; set; }
            public string ServerName { get; set; }
            public string GameID { get; set; }
            public string FullName { get; set; }
            public string ServerIdentityName { get; set; }
            public DateTime Created { get; set; }
            public int ServerStatus { get; set; }
            public int PlayEnabled { get; set; }
            public int TopupEnabled { get; set; }
            public string DNS { get; set; }
            public string GoldTransferLink { get; set; }
            public string IDCheckLink { get; set; }
            public string GetCharacterLink { get; set; }
            public string LoginLink { get; set; }

            /// <summary>
            /// Lấy GameServer theo tên
            /// </summary>
            /// <param name="Name"></param>
            /// <returns></returns>
            public static GameServerModel DetailsByName(string ServerName, string GameID)
            {
                try
                {
                    using (SqlConnection connection = ConnectionHelper.GetConnection())
                    {
                        SqlCommand cmd = new SqlCommand("sp_GameServer_DetailsByName", connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ServerName", ServerName);
                        cmd.Parameters.AddWithValue("@GameID", GameID);
                        SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        if (dbReader.Read())
                        {
                            GameServerModel obj = new GameServerModel()
                            {
                                ServerID = Converter.ToInt(dbReader["ServerID"]),
                                ServerName = Converter.ToString(dbReader["ServerName"]),
                                FullName = Converter.ToString(dbReader["FullName"]),
                                ServerIdentityName = Converter.ToString(dbReader["ServerIdentityName"]),
                                Created = Converter.ToDateTime(dbReader["Created"].ToString(), DateTime.Today),
                                ServerStatus = Converter.ToInt(dbReader["ServerStatus"]),
                                DNS = Converter.ToString(dbReader["DNS"]),
                                GoldTransferLink = Converter.ToString(dbReader["GoldTransferLink"]),
                                IDCheckLink = Converter.ToString(dbReader["IDCheckLink"]),
                                GetCharacterLink = Converter.ToString(dbReader["GetCharacterLink"]),
                                LoginLink = Converter.ToString(dbReader["LoginLink"]),
                                GameID = Converter.ToString(dbReader["GameID"])
                            };
                            return obj;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public class CACK_APIResult
        {
            public string code { get; set; }
            public string msg { get; set; }
            public string data { get; set; }
        }

        public class CACK_Messages
        {
            /// <summary>
            /// Lấy thông báo căn cứ vào mã trả về của API
            /// </summary>
            /// <param name="code"></param>
            /// <returns></returns>
            public static string GetMessage(string code)
            {
                switch (code.Trim())
                {
                    case "1": return "Thành công";

                    case "-1": return "Đăng nhập thất bại";
                    case "-2": return "Tên tài khoản không hợp lệ";
                    case "-3": return "Email không hợp lệ";
                    case "-4": return "Mật khẩu không hợp lệ";
                    case "-5": return "Tài khoản không tồn tại";
                    case "-6": return "Đăng ký thất bại";
                    case "-99": return "Tài khoản ws không hợp lệ";
                    case "-100": return "Tài khoản ws không tồn tại";
                    case "-101": return "IP bị cấm truy cập";
                    case "-102": return "Chữ ký không hợp lệ";
                    case "-103": return "Chứng thực không hợp lệ";
                    case "-110": return "Tham số không đầy đủ hoặc không đúng";
                    case "-120": return "Mã kích hoạt không hợp lệ";
                    case "-200": return "Đơn hàng đã tồn tại";

                    case "1000": return "Thiếu tham số";
                    case "1001": return "giá trị tham số không hợp lệ";
                    case "1002": return "Lỗi mạng";
                    case "1003": return "Không thể vào";
                    case "1004": return "Kiểm chứng thất bại";
                    case "1005": return "Thời gian URL thất bại   ";
                    case "1006": return "ID hệ thống không tồn tại";
                    case "1007": return "ID nhà vận hành không tồn tại";
                    case "1008": return "Database Snail lỗi bất thường";
                    case "3001": return "ID game không tồn tại";
                    case "3002": return "ID cụm server game không tồn tại";
                    case "3003": return "ID server game không tồn tại";
                    case "3004": return "Đạo cụ game không tồn tại";
                    case "3005": return "ID phân trạm game không tồn tại";
                    case "5001": return "Tài khoản người chơi không tồn tại";
                    case "5002": return "Mật mã người chơi sai";
                    case "5003": return "Tài khoản đóng băng";
                    case "5004": return "Tài khoản hủy bỏ";
                    case "5011": return "Tài khoản đăng ký đã tồn tại";
                    case "5021": return "Số dư của gamer không đủ";
                    case "5022": return "Thời hạn game của gamer đã đến hạn";
                    case "5031": return "Vùng game gamer đăng nhập chưa kích hoạt";
                    case "7001": return "Số giao dịch trùng lặp, đã tồn tại";
                    case "7002": return "Số giao dịch không tồn tại";
                    case "7003": return "Đơn giao dịch đã nạp thẻ";
                    case "7004": return "Số tiền trả với số tiền đặt không khớp nhau";
                    case "7011": return "ID điểm thẻ game không tồn tại";
                    case "7012": return "Điểm thẻ game không tồn tại";
                    case "7013": return "Điểm thẻ game đã sử dụng";
                    case "7014": return "Điểm thẻ game đã xóa bỏ";
                    case "7031": return "ID thẻ đạo cụ không tồn tại";
                    case "7032": return "Thẻ đạo cụ không tồn tại";
                    case "7033": return "Thẻ đạo cụ đã sư dụng";
                    case "7034": return "Thẻ đạo cụ đã xóa bỏ";
                    case "7051": return "ID thẻ thời gian game không tồn tại";
                    case "7052": return "Thẻ thời gian game không tồn tại";
                    case "7053": return "Thẻ thời gian game đã sử dụng";
                    case "7054": return "Thẻ thời gian game đã xóa bỏ";
                    case "7071": return "Mã kích hoạt không tồn tại";
                    case "7072": return "Mã kích hoạt đã sử dụng";
                    case "7073": return "Mã kích hoạt đã xóa bỏ";
                    case "7074": return "Vùng yêu cầu kích hoạt đã kích hoạt";
                    case "7075": return "Không nhận được gói quà";
                    default: return "Hệ thống tạm thời gián đoạn";
                }
            }
        }
    }

    public static class StringUtils
    {
        public static string Md5(this string s)
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
