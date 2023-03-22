using MIT.Events.Function;

namespace EST.MIT.Events.Function.Test
{
    public class MockEventEntity : EventEntity
    {
        new public string PartitionKey { get; set; } = null!;
        new public string RowKey { get; set; } = null!;
        new public string Data { get; set; } = null!;
        new public string EventType { get; set; } = null!;
    }
}
