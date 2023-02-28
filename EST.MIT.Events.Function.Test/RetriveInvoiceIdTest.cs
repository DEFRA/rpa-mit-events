using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MIT.Events.Function;
using Xunit;

namespace EST.MIT.Events.Function.Test
{
    public class RetriveInvoiceIdTest
    {
        private readonly ILogger Logger = TestFactory.CreateLogger();

        [Fact]
        public async void InvoiceIdIsRetrived()
        {
            var request = TestFactory.CreateHttpRequest("name", "Sam");
            var response = (OkObjectResult)await Function1.Run(request, Logger);

            Assert.Equal("Hello, Sam. This HTTP triggered function executed successfully.", response.Value);
        }


    }
}
