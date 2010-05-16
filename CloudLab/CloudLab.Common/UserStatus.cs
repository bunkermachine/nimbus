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
