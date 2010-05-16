using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Specialized;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System.Web.Security;
using System.Web;
using System.Net;

namespace CloudLab.Common
{
    public class UserStatus
    {
        private static UserStatus singleton = null;

        public CloudBlobContainer userContainer;
        private Dictionary<string, Project> projectsMap;
        private Project currentProject;
        private Dictionary<string, string> containerMetadata;

        public static UserStatus getInstance() {
            if (singleton == null)
            {
                var storageAccount = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");

                MembershipUser currentUser = Membership.GetUser(HttpContext.Current.User.Identity.Name);
                CloudBlobClient blobStorage = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobStorage.GetContainerReference(currentUser.UserName);
                singleton = new UserStatus(container);
            }
            return singleton;
        }

        public UserStatus(CloudBlobContainer userContainer)
        {
            setUserContainer(userContainer);
            projectsMap = new Dictionary<string, Project>();
            containerMetadata = new Dictionary<string, string>();
        }

        public void setUserContainer(CloudBlobContainer userContainer)
        {
            this.userContainer = userContainer;
        }

        public CloudBlobContainer getUserContainer()
        {
            return this.userContainer;
        }
        
        public Dictionary<string, Project>.ValueCollection getProjectsList()
        {
            return this.projectsMap.Values;
        }

        public void addProjectToCurrentUser(string projectName, Project project)
        {
            this.projectsMap.Add(projectName, project);
        }

        public bool isThisProjectUnderCurrentUser(string projectName)
        {
            return this.projectsMap.ContainsKey(projectName);
        }

        public Project getProjectFromCurrentUser(string projectName)
        {
            return this.projectsMap[projectName];
        }

        public void setCurrentProject(Project currentProject)
        {
            this.currentProject = currentProject;
        }
        
        public Project getCurrentProject()
        {
            return this.currentProject;
        }

        public void addContainerMetadata(string propertyName, string propertyValue)
        {
            this.containerMetadata.Add(propertyName, propertyValue);
            this.commitContainerMetadata();
        }

        public string getContainerMetadataValue(string propertyName)
        {
            return this.userContainer.Metadata[propertyName];
        }

        public bool isThisPropertySetInContainerMetadata(string propertyName)
        {
            return this.containerMetadata.ContainsKey(propertyName);
        }

        public NameValueCollection getContainerMetadata()
        {
            return this.userContainer.Metadata;
        }

        public void commitContainerMetadata()
        {
            NameValueCollection containerMetaData = this.getContainerMetadata();
            
            foreach (string property in containerMetaData.Keys)
            {
                this.userContainer.Metadata.Add(property, containerMetaData[property]);
            }
            
            foreach (string property in this.containerMetadata.Keys)
            {
                this.userContainer.Metadata.Add(property, this.containerMetadata[property]);
            }

            try
            {
                this.userContainer.SetMetadata();
                foreach (string property in this.containerMetadata.Keys)
                {
                    this.containerMetadata.Remove(property);
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
