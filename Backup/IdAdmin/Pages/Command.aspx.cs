using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IDAdmin.Pages
{
    public partial class Command : Lib.UI.BasePage
    {
        public Command()
            : base(Lib.AppFunctions.COMMAND)
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
            }
        }

        protected void buttonPaymentType_Click(object sender, EventArgs e)
        {
            try
            {
                string type = txtPaymentType.Text;
                int status = Lib.Utils.Converter.ToInt(txtStatus.Text, -1);

                if (status == 1 || status == 0)
                {
                    Lib.DataLayer.WebDB.PaymentType_Update(type, (int)status);
                    Response.Redirect("Command.aspx", false);
                }
                else
                {
                    Response.Write("Status not valid");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message + "<br />" + ex.StackTrace);
            }
        }
    }
}
