using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System.Net;
using AjaxControlToolkit;

namespace WebRole
{
  public partial class Default : System.Web.UI.Page
  {
    ArrayList files;

    private static CloudBlobClient blobStorage;
    private static CloudQueueClient queueStorage;

    private static bool s_createdContainerAndQueue = false;
    private static object s_lock = new Object();

    protected void Page_Load(object sender, EventArgs e)
    {
      //files = GetFileList("ftp://e4ftl01u.ecs.nasa.gov/MOLT/MOD09A1.005/2000.02.18/", "anonymous", "guest");
      //fileList.DataSource = files;
      //fileList.DataBind();
    }

    protected void LoadWorkspace(object sender, System.EventArgs e)
    {

    }

    protected void UploadComplete(object sender, AsyncFileUploadEventArgs e)
    {
    }

    protected void CreateProject(object sender, System.EventArgs e)
    {
        workspace.ContentTemplateContainer.Controls.Clear();
        workspace.ContentTemplateContainer.Controls.Add(LoadControl("NewProject.ascx"));
    }

    //protected void CreateTask(object sender, System.EventArgs e)
    //{
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
    //}

    //protected void DatasetSelected(object sender, EventArgs e)
    //{
    //  // ftp://e4ftl01u.ecs.nasa.gov/MOLT/MOD09A1.005/2000.02.18/
    //  // fileList.DataSource = GetFileList(datasetList.Text, "anonymous", "guest");
    //  files = new ArrayList();
    //  files.Add("Test");
    //  fileList.DataSource = files;
    //  fileList.DataBind();
    //}

    //Connects to the FTP server and request the list of available files
    protected ArrayList GetFileList(string FTPAddress, string username, string password)
    {
      ArrayList files = new ArrayList();

      try
      {
        //Create FTP request
        FtpWebRequest request = FtpWebRequest.Create(FTPAddress) as FtpWebRequest;

        request.Method = WebRequestMethods.Ftp.ListDirectory;
        request.Credentials = new NetworkCredential(username, password);
        request.UsePassive = true;
        request.UseBinary = true;
        request.KeepAlive = false;

        //Read the server's response
        FtpWebResponse response = request.GetResponse() as FtpWebResponse;
        Stream responseStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(responseStream);

        while (!reader.EndOfStream)
        {
          files.Add(reader.ReadLine());
        }


        //Clean-up
        reader.Close();
        responseStream.Close(); //redundant
        response.Close();
        return files;
      }
      catch (Exception)
      {
      }

      username = string.Empty;
      password = string.Empty;

      return null;
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

    //protected void Page_PreRender(object sender, EventArgs e)
    //{
    //  try
    //  {
    //    programOutput.Text = GetProgramContainer().GetBlockBlobReference("output.txt").DownloadText();
    //  }
    //  catch (Exception)
    //  {
    //  }
    //}

  }
}
