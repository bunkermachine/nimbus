using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Specialized;
using Microsoft.WindowsAzure.StorageClient;

namespace CloudLab.Common
{
    class UserStatus
    {
        class Project
        {
            class Task
            {
                private string taskName;
                private string downloadedDummyFile;
                private List<string> exes;
                private List<string> dataFiles;

                public Task(string taskName)
                {
                    setTaskName(taskName);
                }

                protected string getDownloadedDummyFile()
                {
                    return downloadedDummyFile;
                }

                protected void setDownloadedDummyFile(string downloadedDummyFile)
                {
                    this.downloadedDummyFile = downloadedDummyFile;
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
            }

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

            protected void commitContainerMetadata()
            {
                NameValueCollection containerMetaData = this.projectDummyFileBlob.Metadata;

                foreach (string property in containerMetaData.Keys)
                {
                    this.projectDummyFileBlob.Metadata.Add(property, containerMetaData[property]);
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
        
        protected CloudBlobContainer userContainer;
        private Dictionary<string, Project> projectsMap;
        private Project currentProject;
        private Dictionary<string, string> containerMetadata;

        public UserStatus(CloudBlobContainer userContainer)
        {
            setUserContainer(userContainer);
        }

        protected void setUserContainer(CloudBlobContainer userContainer)
        {
            this.userContainer = userContainer;
        }

        protected CloudBlobContainer getUserContainer()
        {
            return this.userContainer;
        }
        
        protected Dictionary<string, Project>.ValueCollection getProjectsList()
        {
            return this.projectsMap.Values;
        }

        protected void addProjectToCurrentUser(string projectName, Project project)
        {
            this.projectsMap.Add(projectName, project);
        }

        protected bool isThisProjectUnderCurrentUser(string projectName)
        {
            return this.projectsMap.ContainsKey(projectName);
        }

        protected Project getProjectFromCurrentUser(string projectName)
        {
            return this.projectsMap[projectName];
        }

        protected void setCurrentProject(Project currentProject)
        {
            this.currentProject = currentProject;
        }
        
        protected Project getCurrentProject()
        {
            return this.currentProject;
        }

        protected void addContainerMetadata(string propertyName, string propertyValue)
        {
            this.containerMetadata.Add(propertyName, propertyValue);
        }

        protected string getContainerMetadataValue(string propertyName)
        {
            return this.userContainer.Metadata[propertyName];
        }

        protected bool isThisPropertySetInContainerMetadata(string propertyName)
        {
            return this.containerMetadata.ContainsKey(propertyName);
        }

        protected void commitContainerMetadata()
        {
            NameValueCollection containerMetaData = this.userContainer.Metadata;
            
            foreach (string property in containerMetaData.Keys)
            {
                this.userContainer.Metadata.Add(property, containerMetaData[property]);
            }
            
            foreach (string property in this.containerMetadata.Keys)
            {
                this.userContainer.Metadata.Add(property, this.containerMetadata[property]);
            }
            
            this.userContainer.SetMetadata();
            
            foreach (string property in this.containerMetadata.Keys)
            {
                this.containerMetadata.Remove(property);
            }
        }
        
    }
}
