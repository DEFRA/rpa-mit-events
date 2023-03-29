using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MIT.Events.Function;
using Moq;
using Xunit;

namespace EST.MIT.Events.Function.Test
{
    public class CreateEventTest
    {
        [Fact]
        public void CreateEvent_CreatesNewEventEntityWithCorrectProperties()
        {
            var loggerMock = new Mock<ILogger>();
            _ = new Mock<EventEntity>();
            const string queueItem = "{\"name\":\"CreateInvoice\",\"properties\":{\"id\":\"1234567890\",\"checkpoint\":\"est.invoice.web\",\"status\":\"ApprovalRequired\",\"action\":{\"type\":\"approval\",\"message\":\"Invoicerequiresapproval\",\"timestamp\":\"2023-02-14T15:00:00.000Z\",\"data\":\"{}\"}}}";

            CreateEvent.Run(queueItem, out EventEntity? eventEntity, loggerMock.Object);

            Assert.NotNull(eventEntity);
            Assert.Equal("1234567890", eventEntity.PartitionKey);
            Assert.Equal("approval", eventEntity.EventType);
        }

        [Fact]
        public void CreateEvent_CreatesNewEventEntityWithEmptyMessageFails()
        {
            var loggerMock = new Mock<ILogger>();
            const string queueItem = "{}";

            CreateEvent.Run(queueItem, out EventEntity? eventEntity, loggerMock.Object);

            Assert.Null(eventEntity);
        }

        [Fact]
        public void CreateEvent_CreatesNewEventEntityWithIncorrectPropertiesMessageFails()
        {
            var loggerMock = new Mock<ILogger>();
            const string queueItem = "{ 'name':'testPartitionKey' }";

            CreateEvent.Run(queueItem, out EventEntity? eventEntity, loggerMock.Object);

            Assert.Null(eventEntity);
        }
    }
}