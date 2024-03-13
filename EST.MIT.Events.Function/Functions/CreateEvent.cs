using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Azure.Messaging.ServiceBus;
using Events.Function.Models;
using Events.Function.Validation;
using Azure.Data.Tables;
using EST.MIT.Events.Services;

namespace MIT.Events.Function;

public class CreateEvent
{
    private readonly EventTableService _eventTableService;

    public CreateEvent(EventTableService eventTableService)
    {
        _eventTableService = eventTableService;
    }

    [Function("CreateEvent")]
    public async Task Run([ServiceBusTrigger("%EventQueueName%", Connection = "QueueConnectionString")] ServiceBusReceivedMessage message, ILogger log)
    {
        var eventRequest = message.Body.ToString();
        log.LogInformation($"C# Queue trigger function processed: {eventRequest}");
        var isValid = ValidateEventRequest.IsValid(eventRequest);

        if (!isValid)
        {
            log.LogError("No import request received.");
            return;
        }
        
        var eventData = JsonSerializer.Deserialize<EventRequest>(eventRequest);
        var eventEntity = new EventEntity
        {
            PartitionKey = eventData.Properties.Id,
            RowKey = $"{eventData.Properties.Id}_{DateTime.UtcNow:yyyyMMddHHmmss}",
            Data = eventData.Properties.Action.Data,
            EventType = eventData.Properties.Action.Type
        };

        await _eventTableService.AddEventAsync(eventEntity);
        
    }
}