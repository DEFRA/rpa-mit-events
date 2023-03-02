namespace MIT.Events.Function;

public class MitEvent
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public string Data { get; set; }
}