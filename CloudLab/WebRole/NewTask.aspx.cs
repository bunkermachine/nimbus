using System;
using System.Net;
using System.IO;
using System.Collections;
using System.Collections.Generic;
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
            //string DatasetFTP;
            //if ((DatasetFTP = GeneratePath()) != null) {
            //    ListItem item = AvailableFileList.SelectedItem;
            //    ListItem addItem = new ListItem(item.Text, DatasetFTP + "/" + item.Text);
            //    SelectedFileList.Items.Add(addItem);
            //    AvailableFileList.Items.Remove(addItem);
            //}
        }

        protected void RemoveFile(object sender, System.EventArgs e)
        {
            ListItem removeItem = SelectedFileList.SelectedItem;
            removeItem.Selected = false;
            AvailableFileList.Items.Add(removeItem);
            SelectedFileList.Items.Remove(removeItem);
        }

        protected List<string> DateRangeToPaths()
        {
            List<string> paths = new List<string>();
            DateTime startDate, endDate;
            if (DatasetList.SelectedIndex >= 0
                && DateTime.TryParse(FromDateText.Text, out startDate)
                && DateTime.TryParse(ToDateText.Text, out endDate))
            {
                while (startDate.CompareTo(endDate) <= 0)
                {
                    paths.Add("ftp://" + SourceInfo.products[DatasetList.SelectedIndex]
                        .GetFtpUrl(startDate.Year, startDate.DayOfYear));
                    startDate = startDate.AddDays(1);
                }
            }
            return paths;
        }

        protected void PopulateFileList(object sender, System.EventArgs e)
        {
            List<string> paths = DateRangeToPaths();
            List<string> files = new List<string>();
            foreach (string path in paths)
            {
                List<string> ftpFiles = DownloadFTP.GetFileList(path, "anonymous", "guest");
                if (ftpFiles != null)
                {
                    files.AddRange(ftpFiles);
                }
            }
            AvailableFileList.DataSource = files;
            AvailableFileList.DataBind();
        }

        protected void LaunchTask(object sender, System.EventArgs e)
        {
            string projectName = "NewProject";

            StringBuilder queueMsg;
            foreach (ListItem item in SelectedFileList.Items)
            {
                queueMsg = new StringBuilder();
                queueMsg.Append(User.Identity.Name);
                queueMsg.Append("+" + projectName);
                queueMsg.Append("+" + TaskNameText.Text);
                queueMsg.Append("+" + ExeFile.FileName);
                int[] indices = AvailableFileList.GetSelectedIndices();
                queueMsg.Append("+" + indices.Length);
                queueMsg.Append("+" + item.Value);
                queueMsg.Append("+" + DatasetList.SelectedValue);
                queueMsg.Append("+" + item.Text);

                string msg = queueMsg.ToString();

                //Setting Metadata when a project and task are initiated.
                
                /*CloudBlob blobRef = blobStorage.GetBlobReference(projectName + "" + TaskNameText.Text);
                blobRef.Metadata.Add("User", User.Identity.Name + " Blah");
                blobRef.Metadata.Add("ProjectName", projectName);
                blobRef.Metadata.Add("TaskName", TaskNameText.Text);
                
                //Could use a for loop if we know number of exes selected by user.
                blobRef.Metadata.Add("ExeName", ExeFile.FileName);
                blobRef.Metadata.Add("DatasetFTPURI", DatasetFTP);
                blobRef.Metadata.Add("Dataset", DatasetList.SelectedValue);
                blobRef.SetMetadata();*/
                
                Task newTask = new Task(TaskNameText.Text);
                CloudBlob taskBlob = blobStorage.GetBlobReference(projectName + "/" + TaskNameText.Text);
                newTask.taskDummyFileBlob = taskBlob;
                newTask.addTaskMetadata("author", "sudarshan");
                newTask.addTaskMetadata("timestamp", DateTime.Now.ToString("yyyy.MM.dd hh:mm:ss"));
                newTask.commitTaskMetadata();
               
                Response.Write("ENQUEUED => " + msg + "\n"+"Metadata for User task blob : "+newTask.getTaskMetadataFromBlob()["timestamp"]);
                
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