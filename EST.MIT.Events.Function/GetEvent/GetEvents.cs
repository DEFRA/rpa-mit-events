//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;

//using System.Collections.Generic;
//using Azure.Data.Tables;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Extensions.Http;

//namespace MIT.Events.Function
//{
//    public static class GetEvents
//    {
//        [FunctionName("GetEvents")]
//        public static async Task<IActionResult> Run(
//            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "invoice/events/{invoiceId}")] HttpRequest req,
//            [Table("event", Connection = "AzureWebJobsStorage")] TableClient tableClient,
//            ILogger log, string invoiceId)
//        {
//            var queryResultsFilter = tableClient.QueryAsync<TableEntity>(filter: $"PartitionKey eq '{invoiceId}'");

//            var ListOfEvents = new List<TableEntity>();

//            await foreach (var item in queryResultsFilter)
//            {
//                ListOfEvents.Add(item);
//            }

//            if (ListOfEvents.Count == 0)
//            {
//                return new NotFoundResult();
//            }

//            return new OkObjectResult(ListOfEvents);
//        }
//    }
//}
