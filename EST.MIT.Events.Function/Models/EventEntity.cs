using System;
using Azure;
using Azure.Data.Tables;

namespace MIT.Events.Function;
public class EventEntity : ITableEntity
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public string Data { get; set; }
    public string EventType { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}