﻿using System;
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

        protected void PopulateListBtn_Click(object sender, System.EventArgs e)
        {
            int year = Convert.ToInt32(YearText.Text);
            int day = Convert.ToInt32(DayText.Text);
            string DatasetFTP = SourceInfo.products[DatasetList.SelectedIndex].GetFtpUrl(year, day);
            ArrayList files = DownloadFTP.GetFileList("ftp://" + DatasetFTP + "/", "anonymous", "guest");
            FileList.DataSource = files;
            FileList.DataBind();
        }

        protected void LaunchTaskBtn_Click(object sender, System.EventArgs e)
        {
            int year = Convert.ToInt32(YearText.Text);
            int day = Convert.ToInt32(DayText.Text);
            string DatasetFTP = SourceInfo.products[DatasetList.SelectedIndex].GetFtpUrl(year, day);

            StringBuilder queueMsg = new StringBuilder();
            queueMsg.Append(TaskNameText.Text);
            queueMsg.Append(" " + exeFile.FileName);
            queueMsg.Append(" " + DatasetFTP);

            foreach (ListItem item in FileList.Items)
            {
                if (item.Selected)
                {
                    queueMsg.Append(" " + item.Text);
                }
            }

            GetProgramContainer().GetBlockBlobReference(exeFile.FileName).UploadFromStream(exeFile.FileContent);
            GetProgramRunnerQueue().AddMessage(new CloudQueueMessage(System.Text.Encoding.UTF8.GetBytes(queueMsg.ToString())));
            System.Diagnostics.Trace.WriteLine(String.Format("Enqueued '{0}'", queueMsg));
            Server.Transfer("MapControl.aspx");
        }

        //protected void DatasetSelected(object sender, EventArgs e)
        //{
        //  // ftp://e4ftl01u.ecs.nasa.gov/MOLT/MOD09A1.005/2000.02.18/
        //  // fileList.DataSource = GetFileList(datasetList.Text, "anonymous", "guest");
        //  files = new ArrayList();
        //  files.Add("Test");
        //  fileList.DataSource = files;
        //  fileList.DataBind();
        //}

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
    }
}