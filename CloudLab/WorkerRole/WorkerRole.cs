using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System.IO;
using CloudLab.Common;

namespace WorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            var storageAccount = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");

            CloudBlobClient blobStorage = storageAccount.CreateCloudBlobClient();
            CloudBlobClient otherBlobStorage = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobStorage.GetContainerReference("program");
            CloudBlobContainer downloadContainer = otherBlobStorage.GetContainerReference("downloadcontainer");
            
            CloudQueueClient queueStorage = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueStorage.GetQueueReference("programrunner");

            Trace.TraceInformation("Creating container and queue...");

            // If the Start() method throws an exception, the role recycles.
            // If this sample is run locally and the development storage tool has not been started, this 
            // can cause a number of exceptions to be thrown because roles are restarted repeatedly.
            // Lets try to create the queue and the container and check whether the storage services are running
            // at all.
            bool containerAndQueueCreated = false;
            while (!containerAndQueueCreated)
            {
                try
                {
                    container.CreateIfNotExist();
                    downloadContainer.CreateIfNotExist();

                    var permissions = container.GetPermissions();
                    var downloadPermissions = downloadContainer.GetPermissions();

                    permissions.PublicAccess = BlobContainerPublicAccessType.Container;
                    downloadPermissions.PublicAccess = BlobContainerPublicAccessType.Container;

                    container.SetPermissions(permissions);
                    downloadContainer.SetPermissions(downloadPermissions);

                    permissions = container.GetPermissions();
                    downloadPermissions = downloadContainer.GetPermissions();

                    queue.CreateIfNotExist();

                    containerAndQueueCreated = true;
                }
                catch (StorageClientException e)
                {
                    if (e.ErrorCode == StorageErrorCode.TransportError)
                    {
                        Trace.TraceError(string.Format("Connect failure! The most likely reason is that the local " +
                            "Development Storage tool is not running or your storage account configuration is incorrect. " +
                            "Message: '{0}'", e.Message));
                        System.Threading.Thread.Sleep(5000);
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            Trace.TraceInformation("Listening for queue messages...");

            // Now that the queue and the container have been created in the above initialization process, get messages
            // from the queue and process them individually.
            while (true)
            {
                try
                {
                    CloudQueueMessage msg = queue.GetMessage();
                    if (msg != null)
                    {
                        string queueMsg = msg.AsString;
                        string exeName = queueMsg.Substring(0, queueMsg.IndexOf('+'));
                        string hdfName = queueMsg.Substring(queueMsg.IndexOf('+') + 1);

                        Trace.TraceInformation(string.Format("Dequeued '{0}'", queueMsg));

                        CloudBlockBlob exeContent = container.GetBlockBlobReference(exeName);
                        CloudBlockBlob hdfContent = container.GetBlockBlobReference(hdfName);

                        CloudBlockBlob outputContent = container.GetBlockBlobReference("output.txt");
                        CloudBlockBlob uploadDownloadContent = downloadContainer.GetBlockBlobReference("downloadedFile.jpg");

                        string exePath = Path.Combine(RoleEnvironment.GetLocalResource("LocalStorage1").RootPath, exeName);
                        string hdfPath = Path.Combine(RoleEnvironment.GetLocalResource("LocalStorage1").RootPath, hdfName);

                        //exe_content.DownloadToFile(exe_path);
                        //hdf_content.DownloadToFile(hdf_path);

                        #region run process
                        try
                        {
                            Program HDFParser = new Program();

                            //StreamReader stream = HDFParser.parseHDF(exe_path, hdf_path);
                            //String line = stream.ReadToEnd();

                            uploadDownloadContent.UploadByteArray(DownloadFTP.getDataFromFTP());

                            //output_content.UploadText(line);
                        }
                        catch (Exception ex)
                        {
                            throw;
                            //Note: Exception.Message returns a detailed message that describes the current exception. 
                            //For security reasons, we do not recommend that you return Exception.Message to end users in 
                            //production environments. It would be better to return a generic error message. 
                        }
                        #endregion

                        Trace.TraceInformation(string.Format("Done with '{0}'", queueMsg));

                        queue.DeleteMessage(msg);
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(1000);
                    }
                }
                catch (Exception e)
                {
                    // Explicitly catch all exceptions of type StorageException here because we should be able to 
                    // recover from these exceptions next time the queue message, which caused this exception,
                    // becomes visible again.

                    System.Threading.Thread.Sleep(5000);
                    Trace.TraceError(string.Format("Exception when processing queue item. Message: '{0}'", e.Message));
                }
            }
        }

        public override bool OnStart()
        {
            //DiagnosticMonitor.AllowInsecureRemoteConnections = true;
            DiagnosticMonitor.Start("DiagnosticsConnectionString");

            #region Setup CloudStorageAccount Configuration Setting Publisher

            // This code sets up a handler to update CloudStorageAccount instances when their corresponding
            // configuration settings change in the service configuration file.
            CloudStorageAccount.SetConfigurationSettingPublisher((configName, configSetter) =>
            {
                // Provide the configSetter with the initial value
                configSetter(RoleEnvironment.GetConfigurationSettingValue(configName));

                RoleEnvironment.Changed += (sender, arg) =>
                {
                    if (arg.Changes.OfType<RoleEnvironmentConfigurationSettingChange>()
                        .Any((change) => (change.ConfigurationSettingName == configName)))
                    {
                        // The corresponding configuration setting has changed, propagate the value
                        if (!configSetter(RoleEnvironment.GetConfigurationSettingValue(configName)))
                        {
                            // In this case, the change to the storage account credentials in the
                            // service configuration is significant enough that the role needs to be
                            // recycled in order to use the latest settings. (for example, the 
                            // endpoint has changed)
                            RoleEnvironment.RequestRecycle();
                        }
                    }
                };
            });
            #endregion

            return base.OnStart();
        }

    }
}
