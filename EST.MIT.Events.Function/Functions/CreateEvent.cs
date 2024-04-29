using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Azure.Messaging.ServiceBus;
using Events.Function.Models;
using Events.Function.Validation;
using MIT.Events.Function.Services;

namespace MIT.Events.Function;

public class CreateEvent
{
    private readonly IEventTableService _eventTableService;
    private readonly ILogger<CreateEvent> _logger;

    public CreateEvent(IEventTableService eventTableService, ILogger<CreateEvent> logger)
    {
        _eventTableService = eventTableService;
        _logger = logger;
    }

    [Function("CreateEvent")]
    public async Task Run([ServiceBusTrigger("%EventQueueName%", Connection = "QueueConnectionString")] ServiceBusReceivedMessage message)
    {
        var decodedMessage = message.Body.ToString().DecodeMessage();

        _logger.LogInformation($"C# Queue trigger function processed: {decodedMessage}");
        var isValid = ValidateEventRequest.IsValid(decodedMessage);

        if (!isValid)
        {
            _logger.LogError("No import request received.");
            return;
        }
        
        var eventData = JsonSerializer.Deserialize<EventRequest>(decodedMessage);
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