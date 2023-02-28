using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using EST.MIT.Events.Function;

namespace EST.MIT.Events.Function.Test
{
    public class RetriveInvoiceIdTest
    {
        [Fact]
        public void InvoiceIdIsRetrived()
        {
            int number = 5;

            var invoiceId = new invoiceId(number);
            var eventType = new req.Query["eventType"];

            Assert.Equal()

        }


    }
}
