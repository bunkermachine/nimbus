﻿using System;
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

namespace WorkerRole1
{
    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            var storageAccount = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");

            CloudBlobClient blobStorage = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobStorage.GetContainerReference("program");

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

                    var permissions = container.GetPermissions();

                    permissions.PublicAccess = BlobContainerPublicAccessType.Container;

                    container.SetPermissions(permissions);

                    permissions = container.GetPermissions();

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
                        string queue_msg = msg.AsString;
                        string exe_name = queue_msg.Substring(0, queue_msg.IndexOf('+'));
                        string hdf_name = queue_msg.Substring(queue_msg.IndexOf('+')+1);

                        Trace.TraceInformation(string.Format("Dequeued '{0}'", queue_msg));

                        CloudBlockBlob exe_content = container.GetBlockBlobReference(exe_name);
                        CloudBlockBlob hdf_content = container.GetBlockBlobReference(hdf_name);

                        CloudBlockBlob output_content = container.GetBlockBlobReference("output.txt");

                        string exe_path = Path.Combine(RoleEnvironment.GetLocalResource("LocalStorage1").RootPath, exe_name);
                        string hdf_path = Path.Combine(RoleEnvironment.GetLocalResource("LocalStorage1").RootPath, hdf_name);

                        exe_content.DownloadToFile(exe_path);
                        hdf_content.DownloadToFile(hdf_path);

                        #region run process
                        try
                        {                                            
                            Program HDFParser = new Program();
                             
                            StreamReader stream = HDFParser.parseHDF(exe_path, hdf_path);
                            String line = stream.ReadToEnd();

                            output_content.UploadText(line);
                        }
                        catch (Exception ex)
                        {
                            Trace.TraceError(ex.Message);
                        }
                        #endregion 

                        Trace.TraceInformation(string.Format("Done with '{0}'", queue_msg));

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