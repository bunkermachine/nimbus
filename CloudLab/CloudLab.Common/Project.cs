using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using Microsoft.WindowsAzure.StorageClient;

namespace CloudLab.Common
{
    public class Project
    {
        private string projectName;
        private string projectDummyFile;
        private Dictionary<string, Task> tasksMap;
        private Dictionary<string, string> projectMetadata;
        private CloudBlob projectDummyFileBlob;

        public Project(string projectName, string projectDummyFile, CloudBlob projectDummyFileBlob)
        {
            setProjectName(projectName);
            setProjectDummyFile(projectDummyFile);
            setProjectDummyFileBlob(projectDummyFileBlob);
            tasksMap = new Dictionary<string, Task>();
            projectMetadata = new Dictionary<string, string>();
        }

        public void setProjectDummyFile(string projectDummyFile)
        {
            this.projectDummyFile = projectDummyFile;
        }

        public string getProjectDummyFile()
        {
            return this.projectDummyFile;
        }

        public void setProjectDummyFileBlob(CloudBlob projectDummyFileBlob)
        {
            this.projectDummyFileBlob = projectDummyFileBlob;
        }

        public CloudBlob getProjectDummyFileBlob()
        {
            return this.projectDummyFileBlob;
        }

        public string getProjectName()
        {
            return this.projectName;
        }

        public void setProjectName(string projectName)
        {
            this.projectName = projectName;
        }

        public Dictionary<string, Task>.ValueCollection getTasksList()
        {
            return this.tasksMap.Values;
        }

        public void addTaskToCurrentProject(string taskName, Task task)
        {
            this.tasksMap.Add(taskName, task);
        }

        public bool isThisTaskUnderCurrentProject(string taskName)
        {
            return this.tasksMap.ContainsKey(taskName);
        }

        public Task getTaskFromCurrentProject(string taskName)
        {
            return this.tasksMap[taskName];
        }

        public void addProjectMetadata(string propertyName, string propertyValue)
        {
            this.projectMetadata.Add(propertyName, propertyValue);
        }

        public bool isThisPropertySetInProjectMetadata(string propertyName)
        {
            return this.projectMetadata.ContainsKey(propertyName);
        }

        public string getProjectMetadataValue(string propertyName)
        {
            return this.projectDummyFileBlob.Metadata[propertyName];
        }

        public NameValueCollection getProjectMetadataFromBlob()
        {
            return this.projectDummyFileBlob.Metadata;
        }

        public void commitProjectMetadata()
        {
            NameValueCollection prevProjectMetaData = this.getProjectMetadataFromBlob();

            foreach (string property in prevProjectMetaData.Keys)
            {
                this.projectDummyFileBlob.Metadata.Add(property, prevProjectMetaData[property]);
            }

            foreach (string property in this.projectMetadata.Keys)
            {
                this.projectDummyFileBlob.Metadata.Add(property, this.projectMetadata[property]);
            }

            try
            {
                this.projectDummyFileBlob.SetMetadata();
                foreach (string property in this.projectMetadata.Keys)
                {
                    this.projectMetadata.Remove(property);
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
