using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.StorageClient;

namespace WebRole
{
    public class UserLoginDataModel : TableServiceEntity
    {
        public UserLoginDataModel(string partitionKey, string rowKey)
            : base(partitionKey, rowKey)
        {
        }

        public UserLoginDataModel()
            : this(Guid.NewGuid().ToString(), String.Empty)
        {
        }

        public string Userid { get; set; }
        public string Password { get; set; }
        //public string projectName

    }
}