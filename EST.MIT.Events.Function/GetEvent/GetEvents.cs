using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Azure.Data.Tables;
using System.Linq;
using System.Collections.Generic;

namespace MIT.Events.Function
{
    public static class GetEvents
    {
        [FunctionName("GetEvents")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "invoice/events/{invoiceId}")] HttpRequest req,
            [Table("event", Connection = "TableConnectionString")] TableClient tableClient,
            ILogger log, string invoiceId)

        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var queryResultsFilter = tableClient.QueryAsync<TableEntity>(filter: $"PartitionKey eq '{invoiceId}'");

            var ListOfEvents = new List<TableEntity>();

            await foreach (var item in queryResultsFilter)
            {
                ListOfEvents.Add(item);
            }

            if (ListOfEvents.Count == 0)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(ListOfEvents);
        }
    }
}
