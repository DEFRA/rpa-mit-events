using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using MIT.Events.Function.Services;

namespace MIT.Events.Function;

public class GetEvents
{
    private readonly IEventTableService _eventTableService;
    private readonly ILogger<GetEvents> _logger;

    public GetEvents(IEventTableService eventTableService, ILogger<GetEvents> logger)
    {
        _eventTableService = eventTableService;
        _logger = logger;
    }

    [Function("GetEvents")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "invoice/events/{invoiceId}")] HttpRequestData req, string invoiceId)
    {
        _logger.LogInformation($"C# Queue trigger function processed: {invoiceId} ");
        var queryResultsFilter = _eventTableService.GetEventsAsync(invoiceId);

        if(queryResultsFilter == null)
            return req.CreateResponse(System.Net.HttpStatusCode.NotFound);
        
        var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
        await response.WriteAsJsonAsync(queryResultsFilter);
        
        return response;
    }
}
