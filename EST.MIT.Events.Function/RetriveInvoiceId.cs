using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace MIT.Events.Function
{
    public static class RetriveInvoiceId
    {
        [FunctionName("InvoiceId")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "invoice/events/{invoiceId}")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string eventType = req.Query["eventType"];
            string response = eventType != null ? $"event type {eventType} has been found" : "No event found";

            return new OkObjectResult(response);

        }
    }
}

