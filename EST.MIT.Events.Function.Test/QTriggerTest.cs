using Azure;
using Azure.Data.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Timers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MIT.Events.Function;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
namespace EST.MIT.Events.Function.Test
{
    public class QTriggerTest
    {
        [Fact]
        public async Task TestRun_OkObjectResultForInvoiceExists()
        {
            string invoiceId = "existing-invoice";
            var reqMock = new Mock<HttpRequest>();
            var tableClientMock = new Mock<TableClient>();
            var log = NullLogger.Instance;

            var entity = new TableEntity() { PartitionKey = invoiceId, RowKey = "rowkey" };
            var pagedValues = new[] { entity };
            var page = Page<TableEntity>.FromValues(pagedValues, default, new Mock<Response>().Object);
            var pageable = Pageable<TableEntity>.FromPages(new[] { page });

            var mockResponse = new Mock<Response<TableEntity>>();
            mockResponse.Setup(x => x.Value).Returns(entity);

            tableClientMock.Setup(x => x.Query<TableEntity>(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<IEnumerable<string>>(), default)).Returns(pageable);
            IActionResult result = await Qtrigger.Run(reqMock.Object, tableClientMock.Object, log, invoiceId);

            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            var eventsData = ((Pageable<TableEntity>)okResult.Value).ToArray();
            Assert.Single(eventsData);
            Assert.Equal(invoiceId, eventsData[0].PartitionKey);
        }

        [Fact]
        public async Task TestRun_OkObjectResultForInvoiceNotExists()
        {
            string invoiceId = "non-existing-invoice";
            var reqMock = new Mock<HttpRequest>();
            var tableClientMock = new Mock<TableClient>();
            var log = NullLogger.Instance;

            var emptyPage = Page<TableEntity>.FromValues(Array.Empty<TableEntity>(), default, new Mock<Response>().Object);
            var emptyPageable = Pageable<TableEntity>.FromPages(new[] { emptyPage });

            tableClientMock.Setup(x => x.Query<TableEntity>(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<IEnumerable<string>>(), default)).Returns(emptyPageable);
            IActionResult result = await Qtrigger.Run(reqMock.Object, tableClientMock.Object, log, invoiceId);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}