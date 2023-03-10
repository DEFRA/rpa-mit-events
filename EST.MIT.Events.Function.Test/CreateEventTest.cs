using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MIT.Events.Function;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EST.MIT.Events.Function.Test
{

    public class CreateEventTest
    {
        [Fact]
        public void CreateEvent_CreatesNewEventEntityWithCorrectProperties()
        {
            var loggerMock = new Mock<ILogger>();
            var tableEntityMock = new Mock<MitEvent>();
            var queueItem = "test item";
            MitEvent? eventEntity = null;
            Function.
            Function.CreateEvent(queueItem, out eventEntity, loggerMock.Object);

            Assert.NotNull(eventEntity);
            Assert.Equal("test", eventEntity.PartitionKey);
            Assert.Equal(queueItem, eventEntity.Data);
            Assert.NotEmpty(eventEntity.RowKey);
        }

        [Fact]
        public void QueryEventWithPartitionandRowKey_ReturnsNotFoundResult_WhenEventEntityDoesNotExist()
        {
            var loggerMock = new Mock<ILogger>();
            var httpRequestMock = new Mock<HttpRequest>();
            var tableEntityMock = default(MockEventTableEntity);
            var partitionKey = "testPartitionKey";
            var rowKey = "testRowKey";
            var result = Function.QueryEventWithPartitionandRowKey(httpRequestMock.Object, tableEntityMock, loggerMock.Object, partitionKey, rowKey);

            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}