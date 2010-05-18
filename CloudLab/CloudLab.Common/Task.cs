using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using Microsoft.WindowsAzure.StorageClient;

namespace CloudLab.Common
{
    public class Task
    {
        public string taskName { get; set; }
        public string taskDummyFile { get; set; }
        public CloudBlob taskDummyFileBlob { get; set; }
        public List<string> exes { get; private set; }
        public List<string> dataFiles { get; private set; }
        private Dictionary<string, string> taskMetadata;

        public Task(string taskName, string taskDummyFile, CloudBlob taskDummyFileBlob)
        {
            this.taskName = taskName;
            this.taskDummyFile = taskDummyFile;
            this.taskDummyFileBlob = taskDummyFileBlob;
            this.exes = new List<string>();
            this.dataFiles = new List<string>();
            taskMetadata = new Dictionary<string, string>();
        }

        public void addTaskMetadata(string propertyName, string propertyValue)
        {
            this.taskMetadata.Add(propertyName, propertyValue);
            this.commitTaskMetadata();
        }

        public bool isThisPropertySetInTaskMetadata(string propertyName)
        {
            return this.taskMetadata.ContainsKey(propertyName);
        }

        public string getTaskMetadataValue(string propertyName)
        {
            return this.taskDummyFileBlob.Metadata[propertyName];
        }

        public NameValueCollection getTaskMetadataFromBlob()
        {
            return this.taskDummyFileBlob.Metadata;
        }

        public void commitTaskMetadata()
        {
            NameValueCollection prevTaskMetaData = this.getTaskMetadataFromBlob();

            foreach (string property in prevTaskMetaData.Keys)
            {
                this.taskDummyFileBlob.Metadata.Add(property, prevTaskMetaData[property]);
            }

            foreach (string property in this.taskMetadata.Keys)
            {
                this.taskDummyFileBlob.Metadata.Add(property, this.taskMetadata[property]);
            }

            try
            {
                this.taskDummyFileBlob.SetMetadata();
                foreach (string property in this.taskMetadata.Keys)
                {
                    this.taskMetadata.Remove(property);
                }
            }
            catch (StorageClientException storageClientException)
            {
                Trace.TraceError(string.Format("Exception : '{0}'", storageClientException.Message));
            }
            catch (InvalidOperationException invalidOperationException)
            {
                Trace.TraceError(string.Format("Exception : '{0}'", invalidOperationException.Message));
            }
            
        }
    }
}
