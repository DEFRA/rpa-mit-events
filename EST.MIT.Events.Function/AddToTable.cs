using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;

using Microsoft.Extensions.Logging;
using Azure.Data.Tables;
using System.Text.Json;

namespace MIT.Events.Function
{
    public static class AddToTable
    {
       [FunctionName("CreateEvent")]
        public static void AddQueueItem(
       [QueueTrigger("event", Connection = "QueueConnectionString")] string myQueueItem,
       [Table("event", Connection = "TableConnectionString")] out MitEvent eventEntity,
        ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            var mitdata = JsonSerializer.Deserialize<MitEvent>(myQueueItem);
            eventEntity = mitdata;
        }
    }
}

