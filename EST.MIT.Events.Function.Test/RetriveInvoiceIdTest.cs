using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MIT.Events.Function;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Http;

namespace EST.MIT.Events.Function.Test
{
    public class RetriveInvoiceIdTest
    {
        private readonly ILogger Logger = TestFactory.CreateLogger();

        //[Fact]
        //public async void EventTypeReturned()
        //{
        //    var request = TestFactory.CreateHttpRequest("eventType", "Sam");
        //    var response = (OkObjectResult)await RetriveInvoiceId.Run(request, Logger);

        //    response.Should().NotBeNull();
        //    response.Value.Should().Be("event type Sam has been found");
        //    response.StatusCode.Should().Be(200);
        //}

        //[Fact]
        //public async void EventTypeNotReturned()
        //{
        //    var context = new DefaultHttpContext();
        //    var request = context.Request;
        //    var response = (OkObjectResult)await RetriveInvoiceId.Run(request, Logger);

        //    response.Should().NotBeNull();
        //    response.Value.Should().Be("No event found");
        //    response.StatusCode.Should().Be(200);
        //}
    }
}
