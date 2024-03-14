using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Azure.Messaging.ServiceBus;
using EST.MIT.Events.Services;
using MIT.Events.Function;
using Moq;
using Xunit;

namespace EST.MIT.Events.Function.Test;

public class CreateEventTest
{
    private readonly Mock<EventTableService> _mockEventTableService;
    private readonly Mock<ILogger> _logger;
    
    public CreateEventTest()
    {
        _mockEventTableService = new Mock<EventTableService>();
        _logger = new Mock<ILogger>();
    }

    [Fact]
    public async Task Run_ValidEventRequest_AddsEventEntityToTable()
    {
        var eventItemBody = BinaryData.FromString("{\"name\":\"CreateInvoice\",\"properties\":{\"id\":\"1234567890\",\"checkpoint\":\"est.invoice.web\",\"status\":\"ApprovalRequired\",\"action\":{\"type\":\"approval\",\"message\":\"Invoicerequiresapproval\",\"timestamp\":\"2023-02-14T15:00:00.000Z\",\"data\":\"{}\"}}}");
        var eventItem = ServiceBusModelFactory.ServiceBusReceivedMessage(body: eventItemBody);
        
        var createEvent = new CreateEvent(_mockEventTableService.Object);
        await createEvent.Run(eventItem, _logger.Object);

        _mockEventTableService.Verify(x => x.AddEventAsync(It.IsAny<EventEntity>()), Times.Once);
    }

    [Fact]
    public async Task CreateEvent_CreatesNewEventEntityWithEmptyMessageFails()
    {
        var eventItemBody = BinaryData.FromString("{}");
        var eventItem = ServiceBusModelFactory.ServiceBusReceivedMessage(body: eventItemBody);

        var createEvent = new CreateEvent(_mockEventTableService.Object);
        await createEvent.Run(eventItem, _logger.Object);

        _mockEventTableService.Verify(x => x.AddEventAsync(It.IsAny<EventEntity>()), Times.Never);
    }

    [Fact]
    public async Task CreateEvent_CreatesNewEventEntityWithIncorrectPropertiesMessageFails()
    {
        var eventItemBody = BinaryData.FromString("{ 'name':'testPartitionKey' }");
        var eventItem = ServiceBusModelFactory.ServiceBusReceivedMessage(body: eventItemBody);

        var createEvent = new CreateEvent(_mockEventTableService.Object);
        await createEvent.Run(eventItem, _logger.Object);

        _mockEventTableService.Verify(x => x.AddEventAsync(It.IsAny<EventEntity>()), Times.Never);
    }
}
