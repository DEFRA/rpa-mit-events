using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Data.Tables;

namespace MIT.Events.Function
{
    public static class CreateEvent
    {
        [FunctionName("CreateEvents")]
        public static void AddQueueItem(
       [QueueTrigger("event", Connection = "QueueConnectionString")] string myQueueItem,
       [Table("event", Connection = "TableConnectionString")] out MitEvent eventEntity,
        ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");

            eventEntity = new MitEvent()
            {
                PartitionKey = "test",
                RowKey = Guid.NewGuid().ToString(),
                Data = myQueueItem
            };
        }

    }
}
