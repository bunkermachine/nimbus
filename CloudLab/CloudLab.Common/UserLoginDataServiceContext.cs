using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace CloudLab.Common
{
    public class UserLoginDataServiceContext : TableServiceContext
    {
        public UserLoginDataServiceContext(string baseAddress, StorageCredentials credentials)
            : base(baseAddress, credentials)
        {
        }

        public const string UserLoginTableName = "UserLoginTable";

        public IQueryable<UserLoginDataModel> UserLoginTable
        {
            get
            {
                return this.CreateQuery<UserLoginDataModel>(UserLoginTableName);
            }
        }

    }
}