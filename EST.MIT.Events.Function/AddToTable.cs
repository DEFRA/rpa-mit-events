using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
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

