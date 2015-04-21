using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IDAdmin.Lib;
using IDAdmin.Lib.Utils;
using IDAdmin.Lib.DataLayer;

namespace IDAdmin.Pages
{
    public partial class ServerEdit : Lib.UI.BasePage
    {
        private int _ServerID;

        public ServerEdit()
            : base(Lib.AppFunctions.SERVER_EDIT)
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
            else
            {
                _ServerID = Converter.ToInt(GetParamter("ServerID"));
                if (_ServerID == 0)
                {
                    Response.Redirect("ServerList.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        GetData();
                    }
                    buttonSave.Click += new EventHandler(buttonSave_Click);
                }
            }
        }

        private void GetData()
        {
            try
            {
                DataRow dr = WebDB.Server_Details(_ServerID);
                if (dr == null)
                {
                    Response.Redirect("ServerList.aspx", false);
                }
                else if (Converter.ToString(dr["GameID"]) != AppManager.GameID)
                {
                    Response.Redirect("ServerList.aspx", false);
                }
                else
                {                    
                    txtServerID.Text = Converter.ToString(dr["ServerID"]);
                    txtServerName.Text = Converter.ToString(dr["ServerName"]);
                    txtFullName.Text = Converter.ToString(dr["FullName"]);
                    txtIdentityName.Text = Converter.ToString(dr["ServerIdentityName"]);
                    txtSortOrder.Text = Converter.ToString(dr["SortOrder"]);
                    txtStatus.Text = Converter.ToString(dr["ServerStatus"]);
                    chkIsHot.Checked = Converter.ToInt(dr["IsHot"]) == 1;

                    txtDNS.Text = Converter.ToString(dr["DNS"]);
                    txtGoldTransferLink.Text = Converter.ToString(dr["GoldTransferLink"]);
                    txtIDCheckLink.Text = Converter.ToString(dr["IDCheckLink"]);
                    txtGetCharacterLink.Text = Converter.ToString(dr["GetCharacterLink"]);
                    txtLoginLink.Text = Converter.ToString(dr["LoginLink"]);
                                        
                }
            }
            catch //(Exception ex)
            {                
                Response.Redirect("ServerList.aspx", false);
            }
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                int serverID = Converter.ToInt(txtServerID.Text);
                string serverName = txtServerName.Text.Trim();

                string fullName = txtFullName.Text.Trim();
                if (fullName == "")
                {
                    labelAddMessage.Text = "Tên gọi đầy đủ của Server không hợp lệ";
                    return;
                }

                string serverIdentityName = txtIdentityName.Text.Trim();
                if (serverIdentityName == "")
                {
                    labelAddMessage.Text = "Tên dùng định danh server không hợp lệ";
                    return;
                }

                int sortOder = Converter.ToInt(txtSortOrder.Text);
                if (sortOder == 0)
                {
                    labelAddMessage.Text = "Chưa nhập thứ tự hiển thị Server";
                    return;
                }

                int serverStatus = Converter.ToInt(txtStatus.Text, -1);
                if (serverStatus != 0 && serverStatus != 1)
                {
                    labelAddMessage.Text = "Trạng thái của Server phải là 0 (chưa mở/bảo trì) hoặc 1 (đang hoạt động)";
                    return;
                }
                int isHot = chkIsHot.Checked ? 1 : 0;

                if (!checkAccept.Checked)
                {
                    labelAddMessage.Text = "Ê, có muốn cập nhật server không? Check cái coi !!!";
                    return;
                }

                Lib.DataLayer.WebDB.Server_Update(_ServerID, serverName, fullName, serverIdentityName,
                                                  sortOder, serverStatus, isHot,
                                                  txtDNS.Text.Trim(),
                                                  txtGoldTransferLink.Text.Trim(),
                                                  txtIDCheckLink.Text.Trim(),
                                                  txtGetCharacterLink.Text.Trim(),
                                                  txtLoginLink.Text.Trim());

                WebDB.WriteLog(_User.UserName, Request.UserHostAddress, string.Format("Edit Server: {0} {1} {2}",AppManager.GameID, _ServerID, serverName));

                Response.Redirect("ServerList.aspx", false);

            }
            catch (Exception ex)
            {
                labelAddMessage.Text = "Lỗi: " + ex.Message;
                checkAccept.Checked = false;
            }
        }
    }
}
