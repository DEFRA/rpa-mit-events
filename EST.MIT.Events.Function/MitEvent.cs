using Microsoft.Identity.Client;
using Microsoft.Net.Http.Headers;
using Microsoft.VisualBasic;

namespace MIT.Events.Function;

public class MitEvent
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public string Data { get; set; }
    public string EventType { get; set;}
}