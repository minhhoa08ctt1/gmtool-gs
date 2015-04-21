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
    public partial class ChanellingServerEdit : Lib.UI.BasePage
    {
        private string _partner = "";
        private string _server = "";
        private string _action = "";

        public ChanellingServerEdit()
            : base(Lib.AppFunctions.CHANELLINGGAMSERVER)
        {
        }

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
                _partner = Converter.ToString(Request.QueryString["partner"]);
                _server = Converter.ToString(Request.QueryString["server"]);
                _action = Converter.ToString(Request.QueryString["action"]);

                if (_partner == "")
                {
                    Response.Redirect("Index.aspx", false);
                }
                else if (_server == "")
                {
                    Response.Redirect("ChanellingGameServer.aspx?partner=" + _partner, false);
                }
                else if (_action == "delete")
                {
                    WebDB.ChanellingGameServer_Delete(_partner, _server, AppManager.GameID);
                    WebDB.WriteLog(_User.UserName, Request.UserHostAddress, string.Format("Delete ChanellingServer: ({0}, {1})", _partner, _server));
                    Response.Redirect("ChanellingGameServer.aspx?partner=" + _partner);
                }
                else
                {
                    ViewServerInfo();
                }
            }
        }

        private void ViewServerInfo()
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    DataRow dr = WebDB.ChanellingGameServer_Details(_partner, _server);
                    if (dr == null)
                    {
                        Response.Redirect("ChanellingGameServer.aspx?partner=" + _partner, false);
                    }
                    else if (Converter.ToString(dr["GameID"]) != AppManager.GameID)
                    {
                        Response.Redirect("Index.aspx", false);
                    }
                    else
                    {
                        labelGame.Text = Converter.ToString(dr["GameName"]);
                        labelPartnerName.Text = Converter.ToString(dr["PartnerName"]);
                        labelServer.Text = string.Format("{0} - {1}", dr["ServerName"], dr["FullName"]);

                        if (Converter.ToInt(dr["PlayEnabled"]) == 1)
                        {
                            optPlayEnabled.Checked = true; optPlayDisabled.Checked = false;
                        }
                        else
                        {
                            optPlayEnabled.Checked = false; optPlayDisabled.Checked = true;
                        }

                        if (Converter.ToInt(dr["TopupEnabled"]) == 1)
                        {
                            optTopupEnabled.Checked = true; optTopupDisabled.Checked = false;
                        }
                        else
                        {
                            optTopupEnabled.Checked = false; optTopupDisabled.Checked = true;
                        }

                        txtNotes.Text = Converter.ToString(dr["Notes"]);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.StackTrace);
                }
            }
        }

        protected void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int playEnabled = optPlayEnabled.Checked ? 1 : 0;
                int topupEnabled = optTopupEnabled.Checked ? 1 : 0;
                WebDB.ChanellingGameServer_ChangeStatus(_partner, _server, playEnabled, topupEnabled, txtNotes.Text);
                WebDB.WriteLog(_User.UserName, Request.UserHostAddress, string.Format("Edit ChanellingServer: ({0}, {1}) => ({2},{3})", _partner, _server, playEnabled, topupEnabled));
                Response.Redirect("ChanellingGameServer.aspx?partner=" + _partner, false);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message + ": " + ex.StackTrace);
            }
        }
    }
}
