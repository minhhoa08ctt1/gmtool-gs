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
    public partial class ChanellingServerAdd : Lib.UI.BasePage
    {
        private string _partner = "";

        public ChanellingServerAdd()
            : base(Lib.AppFunctions.CHANELLINGGAMSERVER)
        { }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsLogin())
            {
                Response.Redirect("LogOn.aspx", false);
            }
            if (!CheckRight())
            {
                RedirectToDeniedMessage();
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    DataTable dt = WebDB.Server_Select(false);
                    foreach (DataRow dr in dt.Rows)
                    { 
                        cmbGameServer.Items.Add(new ListItem(dr["FullName"].ToString(), dr["ServerName"].ToString()));
                    }
                }
                _partner = Converter.ToString(Request.QueryString["partner"]);
                if (_partner == "")
                {
                    Response.Redirect("Index.aspx", false);
                }
            }
        }

        protected void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                ListItem selectedServer = cmbGameServer.SelectedItem;
                if (selectedServer != null)
                {
                    int n = WebDB.ChanellingGameServer_Add(_partner, selectedServer.Value);
                    if (n > 0)
                    {
                        WebDB.WriteLog(_User.UserName, Request.UserHostAddress, string.Format("Add ChanellingServer: ({0}, {1})", _partner, selectedServer.Value));
                    }
                    Response.Redirect("ChanellingGameServer.aspx?partner=" + _partner);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message + ": " + ex.StackTrace);
            }
        }
    }
}
