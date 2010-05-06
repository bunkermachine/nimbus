using System;
using System.Net;
using System.IO;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using CloudLab.Common;

namespace WebRole
{
    public partial class NewTask : System.Web.UI.Page
    {
        private static CloudBlobClient blobStorage;
        private static CloudQueueClient queueStorage;

        private static bool s_createdContainerAndQueue = false;
        private static object s_lock = new Object();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DatasetList.DataSource = SourceInfo.products;
                DatasetList.DataTextField = "productName";
                DatasetList.DataBind();
            }
        }

        protected void AddFile(object sender, System.EventArgs e)
        {
            ListItem addItem = FileList.SelectedItem;
            addItem.Selected = false;
            SelectedFileList.Items.Add(addItem);
            FileList.Items.Remove(addItem);
        }

        protected void RemoveFile(object sender, System.EventArgs e)
        {
            ListItem removeItem = SelectedFileList.SelectedItem;
            removeItem.Selected = false;
            FileList.Items.Add(removeItem);
            SelectedFileList.Items.Remove(removeItem);
        }

        protected void PopulateFileList(object sender, System.EventArgs e)
        {
            if (YearText.Text != "" && DayText.Text != "" && DatasetList.SelectedIndex >= 0)
            {
                int year = Convert.ToInt32(YearText.Text);
                int day = Convert.ToInt32(DayText.Text);
                string DatasetFTP = SourceInfo.products[DatasetList.SelectedIndex].GetFtpUrl(year, day);
                ArrayList files = DownloadFTP.GetFileList("ftp://" + DatasetFTP + "/", "anonymous", "guest");
                FileList.DataSource = files;
                FileList.DataBind();
            }
        }

        protected void LaunchTask(object sender, System.EventArgs e)
        {
            string projectName = "NewProject";

            int year = Convert.ToInt32(YearText.Text);
            int day = Convert.ToInt32(DayText.Text);

            string DatasetFTP = "ftp://" + SourceInfo.products[DatasetList.SelectedIndex].GetFtpUrl(year, day);
            
            StringBuilder queueMsg;
            foreach (ListItem item in SelectedFileList.Items)
            {
                queueMsg = new StringBuilder();
                queueMsg.Append(User.Identity.Name);
                queueMsg.Append("+" + projectName);
                queueMsg.Append("+" + TaskNameText.Text);
                queueMsg.Append("+" + ExeFile.FileName);
                int[] indices = FileList.GetSelectedIndices();
                queueMsg.Append("+" + indices.Length);
                queueMsg.Append("+" + DatasetFTP);
                queueMsg.Append("+" + DatasetList.SelectedValue);
                queueMsg.Append("+" + item.Text);

                string msg = queueMsg.ToString();

                Response.Write("ENQUEUED => " + msg + "\n");

                GetProgramRunnerQueue().AddMessage(new CloudQueueMessage(System.Text.Encoding.UTF8.GetBytes(queueMsg.ToString())));
                System.Diagnostics.Trace.WriteLine(String.Format("Enqueued '{0}'", msg));
            }

            GetProgramContainer().GetBlockBlobReference(User.Identity.Name + "/" + projectName + "/" + TaskNameText.Text + "/" + ExeFile.FileName).UploadFromStream(ExeFile.FileContent);
            Server.Transfer("ViewTask.aspx");
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
                    CloudQueue queue = queueStorage.GetQueueReference("programqueue");
                    
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
            return queueStorage.GetQueueReference("programqueue");
        }
    }
}