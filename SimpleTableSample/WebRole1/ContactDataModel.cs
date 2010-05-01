using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.StorageClient;

namespace WebRole1
{
    public class ContactDataModel : TableServiceEntity
    {
         public ContactDataModel(string partitionKey, string rowKey)
        : base(partitionKey, rowKey)
    {
    }

    public ContactDataModel(): this(Guid.NewGuid().ToString(), String.Empty)
    {
    }

    public string Name { get; set; }
    public string Address { get; set; }

    }
}