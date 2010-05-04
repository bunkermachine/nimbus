using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System.Data.Services.Client;

namespace WebRole1
{
    public class ContactDataSource
    {
        private ContactDataServiceContext _ServiceContext = null;

        public ContactDataSource()
        {
            var storageAccount = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");
            _ServiceContext = new ContactDataServiceContext(storageAccount.TableEndpoint.ToString(), storageAccount.Credentials);

            // Create the tables
            // In this case, just a single table.  
            storageAccount.CreateCloudTableClient().CreateTableIfNotExist(ContactDataServiceContext.ContactTableName);

        }

        public IEnumerable<ContactDataModel> Select()
        {
            var results = from c in _ServiceContext.ContactTable
                          select c;

            var query = results.AsTableServiceQuery<ContactDataModel>();
            var queryResults = query.Execute();

            return queryResults;
        }

        public void Delete(ContactDataModel itemToDelete)
        {
            _ServiceContext.AttachTo(ContactDataServiceContext.ContactTableName, itemToDelete, "*");
            _ServiceContext.DeleteObject(itemToDelete);
            _ServiceContext.SaveChanges();
        }

        public void Insert(ContactDataModel newItem)
        {
            _ServiceContext.AddObject(ContactDataServiceContext.ContactTableName, newItem);
            _ServiceContext.SaveChanges();
        }
    }
}