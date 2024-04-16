using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Azure;
using Azure.Data.Tables;

namespace MIT.Events.Function.Services;

public interface IEventTableService
{
    Task AddEventAsync(EventEntity eventEntity);
    AsyncPageable<TableEntity> GetEventsAsync(string invoiceId);
}

public class EventTableService : IEventTableService
{
    private readonly IConfiguration _configuration;
    private readonly TableClient _tableClient;

    public EventTableService(IConfiguration configuration)
    {
        _configuration = configuration;
        _tableClient = new TableClient(_configuration["TableConnectionString"], _configuration["EventTableName"]);
        _tableClient.CreateIfNotExists();
    }

    public async Task AddEventAsync(EventEntity eventEntity) => await _tableClient.AddEntityAsync(eventEntity);
    public AsyncPageable<TableEntity> GetEventsAsync(string invoiceId) => _tableClient.QueryAsync<TableEntity>(filter: $"PartitionKey eq '{invoiceId}'");
}
