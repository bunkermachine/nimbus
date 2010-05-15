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
            private List<Task> tasksList;

            public Project(string projectName)
            {
                setProjectName(projectName);
            }

            protected void setProjectDummyFile(string projectDummyFile)
            {
                this.projectDummyFile = projectDummyFile;
            }

            protected string getProjectDummyFile()
            {
                return this.projectDummyFile;
            }

            protected string getProjectName()
            {
                return this.projectName;
            }

            protected void setProjectName(string projectName)
            {
                this.projectName = projectName;
            }

            protected List<Task> getTasksList()
            {
                return this.tasksList;
            }

            protected void setTasksList(List<Task> tasksList)
            {
                this.tasksList = tasksList;
            }

            protected void addTaskToCurrentProject(Task newTask)
            {
                List<Task> tasksList = getTasksList();
                tasksList.Add(newTask);
                setTasksList(tasksList);
            }



        }
        
        protected CloudBlobContainer userContainer;
        private List<Project> projectsList;
        private Project currentProject;
        private Dictionary<string, string> metadata;

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
        
        protected List<Project> getProjectsList()
        {
            return this.projectsList;
        }

        protected void setProjectsList(List<Project> projectsList)
        {
            this.projectsList = projectsList;
        }

        protected void setCurrentProject(Project currentProject)
        {
            this.currentProject = currentProject;
        }
        
        protected Project getCurrentProject()
        {
            return this.currentProject;
        }

        protected void addMetadata(string propertyName, string propertyValue)
        {
            metadata.Add(propertyName, propertyValue);
        }

        protected void commitMetadata()
        {
            NameValueCollection containerMetaData = userContainer.Metadata;
            foreach (string property in containerMetaData.Keys)
            {
                userContainer.Metadata.Add(property, containerMetaData[property]);
            }
            foreach (string property in metadata.Keys)
            {
                userContainer.Metadata.Add(property, metadata[property]);
            }
            userContainer.SetMetadata();
            foreach (string property in metadata.Keys)
            {
                this.metadata.Remove(property);
            }
        }
        
    }
}
