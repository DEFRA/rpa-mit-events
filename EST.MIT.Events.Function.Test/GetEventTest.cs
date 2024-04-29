//using Azure;
//using Azure.Data.Tables;
//using FluentAssertions;
//using Microsoft.Azure.Functions.Worker.Http;
//using Microsoft.Extensions.Logging;
//using MIT.Events.Function;
//using MIT.Events.Function.Services;
//using Moq;
//using System;
//using System.Net;
//using System.Threading.Tasks;
//using Xunit;

//namespace EST.MIT.Events.Function.Test;

//public class GetEventsTests
//{
//    private readonly Mock<EventTableService> _mockEventTableService;
//    private readonly Mock<HttpRequestData> _mockHttpRequest;
//    private readonly Mock<ILogger> _mockLogger;

//    public GetEventsTests()
//    {
//        _mockEventTableService = new Mock<EventTableService>();
//        _mockHttpRequest = new Mock<HttpRequestData>();
//        _mockLogger = new Mock<ILogger>();
//    }

//    [Fact]
//    public async Task Run_ShouldExecuteSuccessfully()
//    {
//        var invoiceId = "testInvoiceId";
//        var expectedEvents = AsyncPageable<TableEntity>.FromPages(new[] { Page<TableEntity>.FromValues(new[] { new TableEntity() }, null, new Mock<Response>().Object) });
//        _mockEventTableService.Setup(x => x.GetEventsAsync(invoiceId)).Returns(expectedEvents);
//        _mockHttpRequest.Setup(x => x.CreateResponse(HttpStatusCode.OK));

//        var getEvents = new GetEvents(_mockEventTableService.Object);
//        var result = await getEvents.Run(_mockHttpRequest.Object, _mockLogger.Object, invoiceId);

//        result.StatusCode.Should().Be(HttpStatusCode.OK);
//        _mockEventTableService.Verify(x => x.GetEventsAsync(invoiceId), Times.Once);
//    }

//    [Fact]
//    public async Task Run_ShouldReturnNotFound()
//    {
//        var invoiceId = "testInvoiceId";
//        var emptyEvents = AsyncPageable<TableEntity>.FromPages(new[] { Page<TableEntity>.FromValues(Array.Empty<TableEntity>(), default, new Mock<Response>().Object) });
//        _mockEventTableService.Setup(x => x.GetEventsAsync(invoiceId)).Returns(emptyEvents);
//        _mockHttpRequest.Setup(x => x.CreateResponse(HttpStatusCode.NotFound));

//        var getEvents = new GetEvents(_mockEventTableService.Object);
//        var result = await getEvents.Run(_mockHttpRequest.Object, _mockLogger.Object, invoiceId);

//        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
//        _mockEventTableService.Verify(x => x.GetEventsAsync(invoiceId), Times.Once);
//    }
//}
