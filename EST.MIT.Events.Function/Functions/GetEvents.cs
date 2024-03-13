using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using EST.MIT.Events.Services;

namespace MIT.Events.Function;

public class GetEvents
{
    private readonly EventTableService _eventTableService;

    public GetEvents(EventTableService eventTableService)
    {
        _eventTableService = eventTableService;
    }

    [Function("GetEvents")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "invoice/events/{invoiceId}")] HttpRequestData req, ILogger log, string invoiceId)
    {
        log.LogInformation($"C# Queue trigger function processed: {invoiceId} ");
        var queryResultsFilter = _eventTableService.GetEventsAsync(invoiceId);

        if(queryResultsFilter == null)
            return req.CreateResponse(System.Net.HttpStatusCode.NotFound);
        
        var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
        await response.WriteAsJsonAsync(queryResultsFilter);
        
        return response;
    }
}
