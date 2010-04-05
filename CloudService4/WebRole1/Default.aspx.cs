using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace WebRole1
{
    public partial class _Default : System.Web.UI.Page
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {

        }

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
