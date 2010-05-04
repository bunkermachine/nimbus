using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.StorageClient;

namespace WorkerRole
{
    public class DownloadTask: TableServiceEntity
    {
        // Partition Key: Year
        // Row Key: GUID
        public string productName { get; set; }
        public int year { get; set; }
        public int dayOfYear { get; set; }
        public string identifierList { get; set; }      //Either scan time list or the tile location list
        public string startTime { get; set; }
        public string finishTime { get; set; }

        public DownloadTask(string partitionKey, string rowKey)
            : base(partitionKey, rowKey)
        {
        }

        public DownloadTask()
            : base()
        {
        }
    }
}
