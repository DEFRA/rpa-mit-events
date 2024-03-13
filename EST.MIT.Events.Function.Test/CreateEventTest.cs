// using System;
// using System.Threading;
// using System.Threading.Tasks;
// using Azure.Messaging.ServiceBus;
// using FluentAssertions;
// using Microsoft.AspNetCore.Http;
// using Microsoft.Azure.Functions.Worker;
// using Microsoft.Extensions.Logging;
// using MIT.Events.Function;
// using Moq;
// using Xunit;

// namespace EST.MIT.Events.Function.Test
// {
//     public class CreateEventTest
//     {
//         [Fact]
//         public async Task Run_ValidEventRequest_AddsEventEntityToTable()
//         {
//             var mockCollector = new Mock<IAsyncCollector<EventEntity>>();
//             var mockContext = new Mock<FunctionContext>();
//             var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger("CreateEvent");
//             mockContext.Setup(p => p.GetLogger("CreateEvent")).Returns(logger);

//             var eventItemBody = BinaryData.FromString("{\"name\":\"CreateInvoice\",\"properties\":{\"id\":\"1234567890\",\"checkpoint\":\"est.invoice.web\",\"status\":\"ApprovalRequired\",\"action\":{\"type\":\"approval\",\"message\":\"Invoicerequiresapproval\",\"timestamp\":\"2023-02-14T15:00:00.000Z\",\"data\":\"{}\"}}}");
//             var eventItem = ServiceBusModelFactory.ServiceBusReceivedMessage(body: eventItemBody);
            
//             await CreateEvent.Run(eventItem, logger, mockCollector.Object);

//             mockCollector.Verify(p => p.AddAsync(It.IsAny<EventEntity>(), It.IsAny<CancellationToken>()), Times.Once);
//         }

//         [Fact]
//         public async Task CreateEvent_CreatesNewEventEntityWithEmptyMessageFails()
//         {
//             var mockCollector = new Mock<IAsyncCollector<EventEntity>>();
//             var mockContext = new Mock<FunctionContext>();
//             var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger("CreateEvent");
//             mockContext.Setup(p => p.GetLogger("CreateEvent")).Returns(logger);

//             var eventItemBody = BinaryData.FromString("{}");
//             var eventItem = ServiceBusModelFactory.ServiceBusReceivedMessage(body: eventItemBody);

//             await CreateEvent.Run(eventItem, logger, mockCollector.Object);

//             mockCollector.Should().BeNull();
//         }

//         [Fact]
//         public async Task CreateEvent_CreatesNewEventEntityWithIncorrectPropertiesMessageFails()
//         {
//             var mockCollector = new Mock<IAsyncCollector<EventEntity>>();
//             var mockContext = new Mock<FunctionContext>();
//             var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger("CreateEvent");
//             mockContext.Setup(p => p.GetLogger("CreateEvent")).Returns(logger);

//             var eventItemBody = BinaryData.FromString("{ 'name':'testPartitionKey' }");
//             var eventItem = ServiceBusModelFactory.ServiceBusReceivedMessage(body: eventItemBody);

//             await CreateEvent.Run(eventItem, logger, mockCollector.Object);

//             mockCollector.Should().BeNull();
//         }
//     }
// }