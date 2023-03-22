using Events.Function.Models;
using Events.Function.Validation;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;

namespace MIT.Events.Function
{
    public static class CreateEvent
    {
        [FunctionName("CreateEvent")]
        public static void Run(
        [QueueTrigger("event", Connection = "QueueConnectionString")] string eventRequest,
        [Table("event", Connection = "TableConnectionString")] out EventEntity eventEntity,
         ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {eventRequest}");
            var isValid = ValidateEventRequest.IsValid(eventRequest);

            if (!isValid)
            {
                log.LogError("No import request received.");
                eventEntity = null;
            }
            else
            {
                var eventData = JsonSerializer.Deserialize<EventRequest>(eventRequest);
                var partitionKey = eventData.Properties.Id;
                var rowKey = $"{partitionKey}_{DateTime.UtcNow:yyyyMMddHHmmss}";
                var data = JsonSerializer.Serialize(eventData.Properties.Action.Data);
                var eventType = eventData.Properties.Action.Type;

                eventEntity = new EventEntity
                {
                    PartitionKey = partitionKey,
                    RowKey = rowKey,
                    Data = data,
                    EventType = eventType
                };
            }
        }
    }
}
