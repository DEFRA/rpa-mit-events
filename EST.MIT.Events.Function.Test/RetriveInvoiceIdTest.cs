using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using EST.MIT.Events.Function;
using Microsoft.AspNetCore.Mvc;

namespace EST.MIT.Events.Function.Test
{
    public class RetriveInvoiceIdTest
    {
        [Fact]
        public void InvoiceIdIsRetrived()
        {
            int invoiceId = 5;

            //var invoiceId = new invoiceId(number);
            //var eventType = new req.Query["eventType"];

            //Assert.Equal(invoiceId)

            var request = GenerateHttpRequest(invoiceId);
            var response = request.GetResponse(invoiceId);

            Assert.IsType<OkObjectResult>(response);
        }


    }
}
