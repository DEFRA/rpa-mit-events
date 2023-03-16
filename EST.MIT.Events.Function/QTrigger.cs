using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Azure.Data.Tables;
using System.Linq;

namespace MIT.Events.Function
{
    public static class Qtrigger
    {
        [FunctionName("InvoiceId")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "invoice/events/{invoiceId}")] HttpRequest req,
            [Table("event", Connection = "TableConnectionString")] TableClient tableClient,
            ILogger log, string invoiceId)

        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var queryResultsFilter = tableClient.Query<TableEntity>(filter: $"PartitionKey eq '{invoiceId}'");

            var pages = queryResultsFilter.AsPages();
            var firstPage = pages.AsEnumerable().FirstOrDefault();

            if (firstPage == null || !firstPage.Values.Any())
            {
                log.LogInformation($"Item {invoiceId} not found");
                return new NotFoundResult();
            }

            return new OkObjectResult(queryResultsFilter);
        }
    }
}
