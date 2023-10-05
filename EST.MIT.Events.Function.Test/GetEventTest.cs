using Azure;
using Azure.Data.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using MIT.Events.Function;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EST.MIT.Events.Function.Test
{
    public class GetEventTest
    {
        [Fact]
        public async Task TestRun_OkObjectResultForInvoiceExists()
        {
            const string invoiceId = "existing-invoice";
            var reqMock = new Mock<HttpRequest>();
            var tableClientMock = new Mock<TableClient>();
            var log = NullLogger.Instance;

            var entity = new TableEntity() { PartitionKey = invoiceId, RowKey = "rowkey" };
            var pagedValues = new[] { entity };
            var page = Page<TableEntity>.FromValues(pagedValues, default, new Mock<Response>().Object);
            var pageable = AsyncPageable<TableEntity>.FromPages(new[] { page });

            var mockResponse = new Mock<Response<TableEntity>>();
            mockResponse.Setup(x => x.Value).Returns(entity);

            tableClientMock.Setup(x => x.QueryAsync<TableEntity>(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<IEnumerable<string>>(), default)).Returns(
                pageable
            );
            IActionResult result = await GetEvents.Run(reqMock.Object, tableClientMock.Object, log, invoiceId);

            var okResult = (OkObjectResult)result;
            var eventsData = (List<TableEntity>)okResult.Value;

            Assert.IsType<List<TableEntity>>(eventsData);
            Assert.Equal(invoiceId, eventsData[0].PartitionKey);
        }

        [Fact]
        public async Task TestRun_NotFoundResultForInvoiceNotExists()
        {
            const string invoiceId = "non-existing-invoice";
            var reqMock = new Mock<HttpRequest>();
            var tableClientMock = new Mock<TableClient>();
            var log = NullLogger.Instance;

            var emptyPage = Page<TableEntity>.FromValues(Array.Empty<TableEntity>(), default, new Mock<Response>().Object);
            var emptyPageable = AsyncPageable<TableEntity>.FromPages(new[] { emptyPage });

            tableClientMock.Setup(x => x.QueryAsync<TableEntity>(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<IEnumerable<string>>(), default)).Returns(emptyPageable);
            IActionResult result = await GetEvents.Run(reqMock.Object, tableClientMock.Object, log, invoiceId);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}