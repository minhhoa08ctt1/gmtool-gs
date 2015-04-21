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
    public partial class ImageUpload : Lib.UI.BasePage
    {
        public ImageUpload()
            : base(Lib.AppFunctions.IMAGE_UPLOAD)
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
            }
        }

        protected void buttonUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (fileUploadImage.HasFile)
                { 
                    string path = System.Configuration.ConfigurationManager.AppSettings["UploadFolder"];                    
                    if (!path.EndsWith(@"\"))
                    {
                        path += @"\";
                    }
                    string folder = cmbFolder.SelectedValue;
                    string destFileName = txtFileName.Text.Trim();
                    string extension = System.IO.Path.GetExtension(fileUploadImage.FileName);
                    if (destFileName == "")
                    {
                        destFileName = DateTime.Now.Ticks.ToString();
                        //labelMessage.Text = "Chưa đặt tên file";
                        //return;
                    }
                    //else
                    //{                        
                        fileUploadImage.SaveAs(string.Format(@"{0}{1}\{2}{3}",path, folder,destFileName, extension));
                    //}
                    labelMessage.Text = string.Format("Đã upload file! URL: {0}/Images/{1}/{2}{3}",System.Configuration.ConfigurationManager.AppSettings["IdLink"], folder, destFileName, extension);
                }
                else
                {
                    labelMessage.Text = "Chưa chọn file cần upload";
                }
            }
            catch (Exception ex)
            {
                labelMessage.Text = "Lỗi: " + ex.Message;
            }        
        }

        protected void buttonListFile_Click(object sender, EventArgs e)
        {
            try
            {
                string path = string.Format("{0}{1}", System.Configuration.ConfigurationManager.AppSettings["UploadFolder"], cmbFolder.SelectedValue);
                System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(path);
                System.IO.FileInfo[] fileInfos = dirInfo.GetFiles();

                Table table = new Table();
                string link = "";
                TableRow rowHeading = new TableRow();
                rowHeading.Cells.Add(UIHelpers.CreateTableCell(string.Format("Danh sách file trong thư mục <b>{0}</b>", cmbFolder.SelectedValue),
                                                               HorizontalAlign.Left,
                                                               "",
                                                               2));
                table.Rows.Add(rowHeading);

                foreach (System.IO.FileInfo f in fileInfos)
                {
                    TableRow row = new TableRow();
                    link = string.Format("{0}/Images/{1}/{2}",
                                          System.Configuration.ConfigurationManager.AppSettings["IdLink"], 
                                          cmbFolder.SelectedValue,f.Name);
                    row.Cells.Add(UIHelpers.CreateTableCell(f.Name,Unit.Percentage(40), HorizontalAlign.Left, ""));
                    row.Cells.Add(UIHelpers.CreateTableCell(string.Format("<a href='{0}'>{0}<a/>",link), 
                                                            Unit.Percentage(60),HorizontalAlign.Left, ""));
                    table.Rows.Add(row);
                }
                panelListFile.Controls.Clear();
                panelListFile.Controls.Add(table);

            }
            catch (Exception ex)
            {
                labelMessage.Text = "Lỗi: " + ex.Message;
            }
        }
    }
}
