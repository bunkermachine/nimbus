using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System.Net;

namespace WebRole1
{
    public partial class _Default : System.Web.UI.Page
    {

        private static CloudBlobClient blobStorage;
        private static CloudQueueClient queueStorage;

        private static bool s_createdContainerAndQueue = false;
        private static object s_lock = new Object();
      
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void CreateOnceContainerAndQueue()
        {
            if (s_createdContainerAndQueue)
                return;
            lock (s_lock)
            {
                if (s_createdContainerAndQueue)
                {
                    return;
                }

                try
                {
                    var storageAccount = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");

                    blobStorage = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobStorage.GetContainerReference("program");

                    container.CreateIfNotExist();

                    var permissions = container.GetPermissions();

                    permissions.PublicAccess = BlobContainerPublicAccessType.Container;

                    container.SetPermissions(permissions);

                    queueStorage = storageAccount.CreateCloudQueueClient();
                    CloudQueue queue = queueStorage.GetQueueReference("programrunner");

                    queue.CreateIfNotExist();
                }
                catch (WebException)
                {
                    // display a nice error message if the local development storage tool is not running or if there is 
                    // an error in the account configuration that causes this exception
                    throw new WebException("The Windows Azure storage services cannot be contacted " +
                         "via the current account configuration or the local development storage tool is not running. " +
                         "Please start the development storage tool if you run the service locally!");
                }

                s_createdContainerAndQueue = true;
            }
        }


        private CloudBlobContainer GetProgramContainer()
        {
            CreateOnceContainerAndQueue();
            return blobStorage.GetContainerReference("program");
        }

        private CloudQueue GetProgramRunnerQueue()
        {
            CreateOnceContainerAndQueue();
            return queueStorage.GetQueueReference("programrunner");
        }

        protected void submitButton_Click(object sender, EventArgs e)
        {   
            if ((exe.PostedFile != null) && (exe.PostedFile.ContentLength > 0)
                && (hdf.PostedFile != null) && (hdf.PostedFile.ContentLength > 0))
            {

                var exe_name = exe.FileName;
                var hdf_name = hdf.FileName;
                var queue_msg = exe_name + "+" + hdf_name;

                Response.Write("Starting upload... \n");

                GetProgramContainer().GetBlockBlobReference(exe_name).UploadFromStream(exe.FileContent);
                exe.FileContent.Flush();
                GetProgramContainer().GetBlockBlobReference(hdf_name).UploadFromStream(hdf.FileContent);

                Response.Write("The file has been uploaded. \n");

                GetProgramRunnerQueue().AddMessage(new CloudQueueMessage(System.Text.Encoding.UTF8.GetBytes(queue_msg)));

                Response.Write("The file has been added to QUEUE. \n");

                System.Diagnostics.Trace.WriteLine(String.Format("Enqueued '{0}'", queue_msg));

             /*
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
              */
            }
            else
            {
                Response.Write("Please select a file to upload.");
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
                programOutput.Text = GetProgramContainer().GetBlockBlobReference("output.txt").DownloadText();
               
            }
            catch (Exception)
            {
            }
        }

    }
}
