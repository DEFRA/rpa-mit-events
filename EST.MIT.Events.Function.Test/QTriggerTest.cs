using Azure.Data.Tables;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class QTriggerTest
    {
        //[Fact]
        //public async Task TestRun_OkObjectResultForInvoiceExists()
        //{
        //    string invoiceId = "existing-invoice";
        //    var reqMock = new Mock<HttpRequest>();
        //    var tableClientMock = new Mock<TableClient>();

        //    tableClientMock
        //        .Setup(tc => tc.Query<TableEntity>(It.IsAny<string>()))
        //        .Returns(new[] { new TableEntity { PartitionKey = invoiceId } }.AsQueryable());
        //    var log = NullLogger.Instance;

        //    IActionResult result = await Qtrigger.Run(reqMock.Object, tableClientMock.Object, log, invoiceId);

        //    Assert.IsType<OkObjectResult>(result);
        //    var okResult = (OkObjectResult)result;
        //    var eventsData = ((IQueryable<TableEntity>)okResult.Value).ToArray();
        //    Assert.Single(eventsData);
        //    Assert.Equal(invoiceId, eventsData[0].PartitionKey);
        //}




    }
}
//Assert.Equal("Item not found", eventEntity.PartitionKey);