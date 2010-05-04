using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System.Data.Services.Client;

namespace WebRole
{
    public class UserLoginDataSource
    {
        private UserLoginDataServiceContext _ServiceContext = null;

        public UserLoginDataSource()
        {
            var storageAccount = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");
            _ServiceContext = new UserLoginDataServiceContext(storageAccount.TableEndpoint.ToString(), storageAccount.Credentials);

            // Create the tables
            // In this case, just a single table.  
            storageAccount.CreateCloudTableClient().CreateTableIfNotExist(UserLoginDataServiceContext.UserLoginTableName);

        }

        public IEnumerable<UserLoginDataModel> Select()
        {
            var results = from xp in _ServiceContext.UserLoginTable
                          select xp;

            var query = results.AsTableServiceQuery<UserLoginDataModel>();
            var queryResults = query.Execute();

            return queryResults;
        }

        public void Delete(UserLoginDataModel itemToDelete)
        {
            _ServiceContext.AttachTo(UserLoginDataServiceContext.UserLoginTableName, itemToDelete, "*");
            _ServiceContext.DeleteObject(itemToDelete);
            _ServiceContext.SaveChanges();
        }

        public void Insert(UserLoginDataModel newItem)
        {
            _ServiceContext.AddObject(UserLoginDataServiceContext.UserLoginTableName, newItem);
            _ServiceContext.SaveChanges();
        }
    }
}