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
        private string taskName;
        private string taskDummyFile;
        private List<string> exes;
        private List<string> dataFiles;
        private Dictionary<string, string> taskMetadata;
        private CloudBlob taskDummyFileBlob;

        public Task(string taskName)
        {
            setTaskName(taskName);
            exes = new List<string>();
            dataFiles = new List<string>();
            taskMetadata = new Dictionary<string, string>();
        }

        public string getTaskDummyFile()
        {
            return taskDummyFile;
        }

        public void setTaskDummyFile(string taskDummyFile)
        {
            this.taskDummyFile = taskDummyFile;
        }

        public void setTaskDummyFileBlob(CloudBlob taskDummyFileBlob)
        {
            this.taskDummyFileBlob = taskDummyFileBlob;
        }

        public CloudBlob getTaskDummyFileBlob()
        {
            return this.taskDummyFileBlob;
        }

        public string getTaskName()
        {
            return this.taskName;
        }

        public void setTaskName(string taskName)
        {
            this.taskName = taskName;
        }

        public List<string> getExes()
        {
            return this.exes;
        }

        public List<string> getDataFiles()
        {
            return this.dataFiles;
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
