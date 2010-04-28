using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebRole
{
    public partial class NewTask : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateTask(object sender, System.EventArgs e)
        {
        //  string exeName = exeFile.FileName;
        //  GetProgramContainer().GetBlockBlobReference(exeName).UploadFromStream(exeFile.FileContent);
        //  foreach (ListItem item in fileList.Items)
        //  {
        //    if (!item.Selected) continue;

        //    string hdfName = item.Value;
        //    string queueMsg = exeName + "+" + hdfName;
        //    //GetProgramContainer().GetBlockBlobReference(hdfName).UploadFromStream(hdf.FileContent);

        //    GetProgramRunnerQueue().AddMessage(new CloudQueueMessage(System.Text.Encoding.UTF8.GetBytes(queueMsg)));

        //    System.Diagnostics.Trace.WriteLine(String.Format("Enqueued '{0}'", queueMsg));
        //  }
        }
    }
}