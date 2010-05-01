using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace WebRole1
{
    public class ContactDataServiceContext : TableServiceContext
    {
        public ContactDataServiceContext(string baseAddress, StorageCredentials credentials)
            : base(baseAddress, credentials)
        {
        }

        public const string ContactTableName = "ContactTable";

        public IQueryable<ContactDataModel> ContactTable
        {
            get
            {
                return this.CreateQuery<ContactDataModel>(ContactTableName);
            }
        }

    }
}