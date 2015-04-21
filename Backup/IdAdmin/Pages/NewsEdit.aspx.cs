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
    public partial class NewsEdit : Lib.UI.BasePage
    {
        protected string _action;
        protected long _id;
        protected string _returnURL;

        public NewsEdit()
            : base(Lib.AppFunctions.NEWS_EDIT)
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
                _action = GetParamter("action");
                _id = Converter.ToLong(GetParamter("id"));
                _returnURL = Server.UrlDecode(GetParamter("returnURL"));

                if (!Page.IsPostBack)
                {
                    ShowNews();
                }
                else
                {
                    SaveNews();
                }                
            }
        }

        protected void ShowNews()
        {            
            cmbGame.Items.Add(new ListItem("Dùng chung",""));
            //DataTable dtGame = WebDB.Game_Select();
            //foreach (DataRow dr in dtGame.Rows)
            //{
            //    cmbGame.Items.Add(new ListItem(dr["GameName"].ToString(), dr["GameID"].ToString()));
            //}

            if (_action == "edit")
            {
                DataRow dr = WebDB.News_Details(_id);
                if (dr == null)
                {
                    Response.Redirect(_returnURL, false);
                }
                else
                {
                    this.txtTitle.Text = dr[Lib.Meta.NEWS_TITLE].ToString();
                    this.txtContent.Text = dr[Lib.Meta.NEWS_CONTENT].ToString();
                    this.cmbCategory.SelectedValue = dr[Lib.Meta.NEWS_CATEGORY].ToString();
                    this.cmbGame.SelectedValue = dr["GameID"].ToString();
                }
            }
            else
            {
            }
        
        }

        protected void SaveNews()
        {
            string title = txtTitle.Text.Trim();            
            if (title == "")
            {
                labelTitleMsg.Text = "Chưa nhập tiêu đề của bài viết";
                return;
            }
            string content = txtContent.Text.Trim();
            if (content == "")
            {
                labelContentMsg.Text = "Chưa nhập nội dung bài viết";
                return;
            }
            int category = Converter.ToInt(cmbCategory.SelectedValue,1);
            string GameID = cmbGame.SelectedValue; 

            if (_action == "edit")
            {
                WebDB.News_Update(_id, title, content, category, _User.UserName, GameID);
                WebDB.WriteLog(_User.UserName, Request.UserHostAddress, "News_Edit: " + _id.ToString());
                Response.Redirect(_returnURL);
            }
            else
            {
                WebDB.News_Insert(title, content, category, _User.UserName, GameID);
                Response.Redirect("NewsList.aspx");
            }
        }
    }
}
