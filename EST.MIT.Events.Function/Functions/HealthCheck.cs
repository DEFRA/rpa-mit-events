using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace MIT.Events.Function;

public class HealthCheck
{
    private readonly ILogger<HealthCheck> _logger;
    public HealthCheck(ILogger<HealthCheck> logger)
    {
        _logger = logger;
    }

    [Function("healthy")]
    public HttpResponseData RunHealthy(
        [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
    {
        _logger.LogInformation("Healthy check.");

        return req.CreateResponse(HttpStatusCode.OK);
    }

    [Function("healthz")]
    public HttpResponseData RunHealthz(
        [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
    {
        _logger.LogInformation("Healthz check");

        return req.CreateResponse(HttpStatusCode.OK);
    }
}
