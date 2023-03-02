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
    public static class RetriveInvoiceId
    {
        [FunctionName("InvoiceId")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "invoice/events/{invoiceId}")] HttpRequest req,
            [Table("event", Connection = "TableConnectionString")] TableClient tableClient,
            ILogger log, string invoiceId)
            
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var queryResultsFilter = tableClient.Query<TableEntity>(filter: $"PartitionKey eq '{invoiceId}'");

            if (queryResultsFilter == null)
            {
                log.LogInformation($"Item {invoiceId} not found");
                return new NotFoundResult();
            }

            return new OkObjectResult(queryResultsFilter);

        }

        [FunctionName("CreateEvent")]
        public static void CreateEvent(
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

