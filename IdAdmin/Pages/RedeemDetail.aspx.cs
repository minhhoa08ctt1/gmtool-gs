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
    public partial class RedeemDetail : BasePage
    {
      
        protected override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack)
                {
                    update_log();
                }
                string logid = Request.QueryString["logid"];
                if (logid != null)
                {
                    List<AdminLog> aLog = WebDB.GetLogCompensationWhere(logid);
                    if (aLog != null)
                    {
                        string parram = aLog[0].parram_input;
                        JavaScriptSerializer ser = new JavaScriptSerializer();
                        var parram_data = ser.Deserialize<Dictionary<string, string>>(parram);
                        txtGameTypeView1.Text = parram_data.ElementAt(0).Value;
                        txtZoneIdView1.Text = parram_data.ElementAt(1).Value;
                        txtUidView1.Text = parram_data.ElementAt(2).Value;
                        txtCharIdView1.Text = parram_data.ElementAt(3).Value;
                        txtNameView1.Text = parram_data.ElementAt(4).Value;
                        DropDownListReTypeView1.SelectedItem.Value = parram_data.ElementAt(5).Value;
                        txtNumView1.Text = parram_data.ElementAt(6).Value;
                        txtItemIDView1.Text = parram_data.ElementAt(7).Value;
                        DropDownListIsBindView1.SelectedItem.Value = parram_data.ElementAt(8).Value;
                        txtUnitView1.Text = parram_data.ElementAt(9).Value;
                        txtTitleView1.Text = parram_data.ElementAt(10).Value;
                    }

                }
            }
            catch (Exception ex)
            {
                Server.Transfer("RedeemList.aspx", true);
            }           
        }

        protected void update_log()
        {
            try
            {
                int logid = Convert.ToInt32(Request.QueryString["logid"].ToString());
                if (logid > 0)
                {
                   
                        string LogAction = "ApiGH Compensate: " + txtGameTypeView1.Text + "," + txtZoneIdView1.Text + "," + txtUidView1.Text + "," + txtCharIdView1.Text + "," + txtNameView1.Text + "," + DropDownListReTypeView1.SelectedItem.Text + "," + txtNumView1.Text + "," + txtItemIDView1.Text + "," + DropDownListIsBindView1.SelectedItem.Text + "," + txtUnitView1.Text;

                        var parram = new Dictionary<string, string>();
                        parram.Add("game", txtGameTypeView1.Text);
                        parram.Add("zoneid", txtZoneIdView1.Text);
                        parram.Add("uid", txtUidView1.Text);
                        parram.Add("charid", txtCharIdView1.Text);
                        parram.Add("name", txtNameView1.Text);
                        parram.Add("type", DropDownListReTypeView1.SelectedItem.Value);
                        parram.Add("num", txtNumView1.Text);
                        parram.Add("itemid", txtItemIDView1.Text);
                        parram.Add("bind", DropDownListIsBindView1.SelectedItem.Text);
                        parram.Add("Unit", txtUnitView1.Text);
                        parram.Add("Title", txtTitleView1.Text);
                    
                   

                        var jsonSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                        string parram_json = jsonSerializer.Serialize(parram);

                        if (checkAcceptView1.Checked)
                        {
                            bool result = WebDB.UpdateLogCompensation(logid, _User.UserName, Request.UserHostAddress, LogAction, parram_json, txtTitleView1.Text);
                            if (result==true)
                            {
                                Server.Transfer("RedeemList.aspx", true);
                                return;
                            }
                            else
                            {
                                labelMessageView1.Text = "Update không thành công";
                            }
                        }
                        else
                        {
                            labelCheckMessageView1.Text = "[Chưa xác nhận]";
                        }
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
        protected void DropDownListReTypeView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeState();
        }

        protected void buttonAcceptView1_Click(object sender, EventArgs e)
        {

        }
    }
}