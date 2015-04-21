using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IDAdmin.Lib.DataLayer;
using IDAdmin.Lib.UI;
using IDAdmin.Lib.Utils;

namespace IDAdmin.Pages
{
    public partial class Index : Lib.UI.BasePage
    {
        public Index()
            : base("Index")
        { }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsLogin())
            {
                RedirectToLogOn();
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    string _CurrentGameID = AppManager.GameID;
                    ListItem itemNull = new ListItem("-- Chọn dịch vụ/game quản trị --", "");
                    cmbGame.Items.Add(itemNull);

                    DataTable dtGame = WebDB.Game_SelectForAdmin(_User.UserName);
                    if (dtGame != null)
                    {
                        foreach (DataRow dr in dtGame.Rows)
                        {
                            ListItem item = new ListItem(dr["GameName"].ToString(), dr["GameID"].ToString());
                            if (dr["GameID"].ToString() == _CurrentGameID)
                            {
                                item.Selected = true;
                            }
                            cmbGame.Items.Add(item);
                        }
                    }

                }
            }
        }

        protected void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                ListItem selItem = cmbGame.SelectedItem;
                if (selItem != null)
                {
                    if (selItem.Value != "")
                    {
                        AppManager.GameID = selItem.Value;
                        AppManager.GameName = selItem.Text;
                    }
                }
                Response.Redirect("Index.aspx",false);
            }
            catch
            { 

            }
        }
    }
}
