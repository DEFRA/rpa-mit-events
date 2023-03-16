using MIT.Events.Function;

namespace EST.MIT.Events.Function.Test
{
    public class MockEventEntity : MitEvent
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string Data { get; set; }
        public string EventType { get; set; }
    }
}
