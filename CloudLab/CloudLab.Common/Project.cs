using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using Microsoft.WindowsAzure.StorageClient;

namespace CloudLab.Common
{
    class Project
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
        }

        protected void setProjectDummyFile(string projectDummyFile)
        {
            this.projectDummyFile = projectDummyFile;
        }

        protected string getProjectDummyFile()
        {
            return this.projectDummyFile;
        }

        protected void setProjectDummyFileBlob(CloudBlob projectDummyFileBlob)
        {
            this.projectDummyFileBlob = projectDummyFileBlob;
        }

        protected CloudBlob getProjectDummyFileBlob()
        {
            return this.projectDummyFileBlob;
        }

        protected string getProjectName()
        {
            return this.projectName;
        }

        protected void setProjectName(string projectName)
        {
            this.projectName = projectName;
        }

        protected Dictionary<string, Task>.ValueCollection getTasksList()
        {
            return this.tasksMap.Values;
        }

        protected void addTaskToCurrentProject(string taskName, Task task)
        {
            this.tasksMap.Add(taskName, task);
        }

        protected bool isThisTaskUnderCurrentProject(string taskName)
        {
            return this.tasksMap.ContainsKey(taskName);
        }

        protected Task getTaskFromCurrentProject(string taskName)
        {
            return this.tasksMap[taskName];
        }

        protected void addProjectMetadata(string propertyName, string propertyValue)
        {
            this.projectMetadata.Add(propertyName, propertyValue);
        }

        protected bool isThisPropertySetInProjectMetadata(string propertyName)
        {
            return this.projectMetadata.ContainsKey(propertyName);
        }

        protected string getContainerMetadataValue(string propertyName)
        {
            return this.projectDummyFileBlob.Metadata[propertyName];
        }

        protected void commitProjectMetadata()
        {
            NameValueCollection prevProjectMetaData = this.projectDummyFileBlob.Metadata;

            foreach (string property in prevProjectMetaData.Keys)
            {
                this.projectDummyFileBlob.Metadata.Add(property, prevProjectMetaData[property]);
            }

            foreach (string property in this.projectMetadata.Keys)
            {
                this.projectDummyFileBlob.Metadata.Add(property, this.projectMetadata[property]);
            }

            this.projectDummyFileBlob.SetMetadata();

            foreach (string property in this.projectMetadata.Keys)
            {
                this.projectMetadata.Remove(property);
            }
        }

    }
}
