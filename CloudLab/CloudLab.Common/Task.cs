using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using Microsoft.WindowsAzure.StorageClient;

namespace CloudLab.Common
{
    class Task
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
        }

        protected string getTaskDummyFile()
        {
            return taskDummyFile;
        }

        protected void setTaskDummyFile(string taskDummyFile)
        {
            this.taskDummyFile = taskDummyFile;
        }

        protected void setTaskDummyFileBlob(CloudBlob taskDummyFileBlob)
        {
            this.taskDummyFileBlob = taskDummyFileBlob;
        }

        protected CloudBlob getTaskDummyFileBlob()
        {
            return this.taskDummyFileBlob;
        }

        protected string getTaskName()
        {
            return this.taskName;
        }

        protected void setTaskName(string taskName)
        {
            this.taskName = taskName;
        }

        protected List<string> getExes()
        {
            return this.exes;
        }

        protected List<string> getDataFiles()
        {
            return this.dataFiles;
        }

        protected void addTaskMetadata(string propertyName, string propertyValue)
        {
            this.taskMetadata.Add(propertyName, propertyValue);
        }

        protected bool isThisPropertySetInTaskMetadata(string propertyName)
        {
            return this.taskMetadata.ContainsKey(propertyName);
        }

        protected string getTaskMetadataValue(string propertyName)
        {
            return this.taskDummyFileBlob.Metadata[propertyName];
        }

        protected void commitTaskMetadata()
        {
            NameValueCollection prevTaskMetaData = this.taskDummyFileBlob.Metadata;

            foreach (string property in prevTaskMetaData.Keys)
            {
                this.taskDummyFileBlob.Metadata.Add(property, prevTaskMetaData[property]);
            }

            foreach (string property in this.taskMetadata.Keys)
            {
                this.taskDummyFileBlob.Metadata.Add(property, this.taskMetadata[property]);
            }

            this.taskDummyFileBlob.SetMetadata();

            foreach (string property in this.taskMetadata.Keys)
            {
                this.taskMetadata.Remove(property);
            }
        }
    }
}
