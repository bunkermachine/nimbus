using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using AjaxControlToolkit;

namespace CloudLab
{
    /// <summary>
    /// Summary description for WebForm1.
    /// </summary>
    public partial class Default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            // 
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            // 
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        protected void ExeFile_UploadedComplete(object sender, AsyncFileUploadEventArgs e)
        {
            string savePath = MapPath("App_Data/" + Path.GetFileName(e.filename));
            exeFile.SaveAs(savePath);
        }

        protected void Submit_ServerClick(object sender, System.EventArgs e)
        {
          string exeFileLocation = Server.MapPath("App_Data/" + System.IO.Path.GetFileName(exeFile.PostedFile.FileName));
          try {
            Program HDFParser = new Program();
            Response.Write("The file has been uploaded.");
            StreamReader stream = HDFParser.parseHDF(exeFileLocation,Server.MapPath("App_Data/MLS-Aura_L2GP-GPH_v01-52-c01_2007d032.he5"));
            String line = stream.ReadToEnd();
            data.InnerHtml = line.Replace("\n", "<br />");
          } catch (Exception ex) {
            Response.Write("Error: " + ex.Message);
          }
        }
    }
}
