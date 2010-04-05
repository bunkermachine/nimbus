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

namespace HDFFileReceiver
{
    /// <summary>
    /// Summary description for WebForm1.
    /// </summary>
    public class Default : System.Web.UI.Page
    {
        protected System.Web.UI.HtmlControls.HtmlInputFile File1;
        protected System.Web.UI.HtmlControls.HtmlInputFile File2;
        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;
        protected System.Web.UI.HtmlControls.HtmlContainerControl Data;

        private void Page_Load(object sender, System.EventArgs e)
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
            this.Submit1.ServerClick += new System.EventHandler(this.Submit1_ServerClick);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        private void Submit1_ServerClick(object sender, System.EventArgs e)
        {
            if ((File1.PostedFile != null) && (File1.PostedFile.ContentLength > 0)
                && (File2.PostedFile != null) && (File2.PostedFile.ContentLength > 0))
            {
                string fn2 = System.IO.Path.GetFileName(File2.PostedFile.FileName);
                string SaveLocation2 = Server.MapPath("App_Data") + "\\" + fn2;
                string fn1 = System.IO.Path.GetFileName(File1.PostedFile.FileName);
                string SaveLocation1 = Server.MapPath("App_Data") + "\\" + fn1;
                try
                {
                    Program HDFParser = new Program();
                    
                    File1.PostedFile.SaveAs(SaveLocation1);
                    File2.PostedFile.SaveAs(SaveLocation2);
                    Response.Write("The file has been uploaded.");
                    StreamReader stream = HDFParser.parseHDF(SaveLocation2, SaveLocation1);
                    String line = stream.ReadToEnd();

                    Data.InnerHtml = line;
                    
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.Message);
                    //Note: Exception.Message returns a detailed message that describes the current exception. 
                    //For security reasons, we do not recommend that you return Exception.Message to end users in 
                    //production environments. It would be better to return a generic error message. 
                }
            }
            else
            {
                Response.Write("Please select a file to upload.");
            }
        }
    }
}
