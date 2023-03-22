
namespace MIT.Events.Function;
public class EventEntity
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public string Data { get; set; }
    public string EventType { get; set; }
}