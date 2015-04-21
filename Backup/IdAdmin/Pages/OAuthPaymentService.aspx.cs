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
    public partial class OAuthPaymentService : Lib.UI.BasePage
    {
        protected string _clientID;
        protected string _action;
        protected string _returnURL;
        protected string _serviceID;

        public OAuthPaymentService()
            : base(Lib.AppFunctions.OAUTH_MANAGER)
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
                _clientID = GetParamter("clientid");
                _action = GetParamter("action");
                _serviceID = GetParamter("serviceid");
                _returnURL = HttpUtility.UrlDecode(GetParamter("returnURL"));                

                if (!Page.IsPostBack)
                {
                    switch (_action)
                    { 
                        case "edit":
                            labelEditTitle.Text = "Edit OAuthPaymentService";
                            linkAddNew.Enabled = true;
                            linkAddNew.NavigateUrl = string.Format("OAuthPaymentService.aspx?clientid={0}&returnURL={1}", _clientID, HttpUtility.UrlEncode(_returnURL));
                            ViewEditData();
                            break;
                        default:
                            labelEditTitle.Text = "Add new OAuthPaymentService";
                            linkAddNew.Enabled = false;                            
                            break;
                    }
                    ListData();
                }

                linkBack.NavigateUrl = _returnURL;
                this.buttonSave.Click += new EventHandler(buttonSave_Click);
            }
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                string serviceid = txtServiceID.Text.Trim();
                string serviceName = txtServiceName.Text.Trim();
                string serviceKey = txtServiceKey.Text.Trim();
                string serviceDesc = txtServiceDesc.Text.Trim();
                string gosuTransferType = cmbGosuTransferType.SelectedValue;

                if (string.IsNullOrEmpty(serviceid) ||
                    string.IsNullOrEmpty(serviceName) ||
                    string.IsNullOrEmpty(serviceKey) ||
                    string.IsNullOrEmpty(gosuTransferType))
                {
                    labelMessage.Text = "Dữ liệu nhập không hợp lệ";
                    return;
                }

                if (_action == "edit")
                {
                    WebDB.OAuthPaymentService_Update(serviceid, serviceName, serviceKey, serviceDesc, gosuTransferType, _clientID);
                }
                else
                { 
                    if (WebDB.OAuthPaymentService_Exists(serviceid))
                    {
                        labelMessage.Text = "ServiceID bị trùng";
                        return;
                    }
                    WebDB.OAuthPaymentService_Insert(serviceid, serviceName, serviceKey, serviceDesc, gosuTransferType, _clientID);
                }
                Response.Redirect(string.Format("OAuthPaymentService.aspx?clientid={0}&returnURL={1}", _clientID, _returnURL), false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ViewEditData()
        {
            try
            {
                DataRow dr = WebDB.OAuthPaymentService_Details(_serviceID, _clientID);
                if (dr != null)
                {
                    txtServiceID.Text = dr["ServiceID"].ToString();
                    txtServiceName.Text = dr["ServiceName"].ToString();
                    txtServiceKey.Text = dr["ServiceKey"].ToString();
                    txtServiceDesc.Text = dr["ServiceDesc"].ToString();
                    cmbGosuTransferType.SelectedValue = dr["GosuTransferType"].ToString();

                    txtServiceID.Enabled = false;
                }
                else
                {
                    Response.Redirect(string.Format("OAuthPaymentService.aspx?clientid={0}&returnURL={1}", _clientID, _returnURL), false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ListData()
        {
            try
            {
                DataRow drApp = WebDB.OAuthApplication_Details(_clientID);
                if (drApp == null)
                {
                    Response.Redirect(_returnURL, false);
                    return;
                }
                else
                {
                    labelClientID.Text = drApp["ClientID"].ToString();
                    labelApplicationName.Text = drApp["ApplicationName"].ToString();
                    labelUserName.Text = drApp["UserName"].ToString();

                    Table table = new Table();
                    table.CssClass = "table1";
                    table.CellSpacing = 1;

                    TableHeaderRow rowHeader = new TableHeaderRow();
                    rowHeader.Cells.AddRange
                    (
                        new TableCell[]
                    {     
                        UIHelpers.CreateTableCell("STT",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("ServiceID",Unit.Percentage(25),HorizontalAlign.Center, "cellHeader"),                        
                        UIHelpers.CreateTableCell("Service Name",Unit.Percentage(30),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Register Date",Unit.Percentage(15),HorizontalAlign.Center,"cellHeader"),
                        UIHelpers.CreateTableCell("Transfer Type",Unit.Percentage(10),HorizontalAlign.Center,"cellHeader"),                        
                        UIHelpers.CreateTableCell("&nbsp;",Unit.Percentage(15),HorizontalAlign.Center,"cellHeader")
                    }
                    );
                    table.Rows.Add(rowHeader);

                    using (DataTable dt = Lib.DataLayer.WebDB.OAuthPaymentService_Select(_clientID))
                    {
                        if (dt == null || dt.Rows.Count == 0)
                        {
                            TableRow rowEmpty = new TableRow();
                            rowEmpty.Cells.Add(UIHelpers.CreateTableCell("<p>&nbsp;</p>", HorizontalAlign.Center, "cell1", 6));
                            table.Rows.Add(rowEmpty);
                        }
                        else
                        {
                            string css = "cell1";
                            int stt = 0;
                            foreach (DataRow dr in dt.Rows)
                            {
                                css = css == "cell2" ? "cell1" : "cell2";
                                TableRow row = new TableRow();
                                row.Cells.AddRange
                                (
                                    new TableCell[]
                                    {
                                        UIHelpers.CreateTableCell((++stt).ToString(),HorizontalAlign.Center,css),
                                        UIHelpers.CreateTableCell(dr["ServiceID"].ToString(), 
                                                                  HorizontalAlign.Left, css),                                    
                                        UIHelpers.CreateTableCell(dr["ServiceName"].ToString(), HorizontalAlign.Left, css),
                                        UIHelpers.CreateTableCell(string.Format("{0:dd/MM/yyyy HH:mm}", dr["RegisterDate"]), HorizontalAlign.Left,css),
                                        UIHelpers.CreateTableCell(dr["GosuTransferType"].ToString(), HorizontalAlign.Center, css),                                    
                                        UIHelpers.CreateTableCell(string.Format("<a href='OAuthPaymentService.aspx?action=edit&clientid={0}&serviceid={1}&returnURL={2}'>[Edit]</a>",_clientID,dr["ServiceID"].ToString(),HttpUtility.UrlEncode(_returnURL)), 
                                                                  HorizontalAlign.Center, css),
                                    }
                                );
                                table.Rows.Add(row);
                            }
                        }
                    }

                    this.panelList.Controls.Clear();
                    this.panelList.Controls.Add(table);
                }
            }
            catch (Exception ex)
            {
                Label labelMessage = new Label();
                labelMessage.Text = ex.Message;
                this.panelList.Controls.Add(labelMessage);
            }
        }
    }
}
