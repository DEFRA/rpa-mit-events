using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Events.Function.Models;

public class EventResponse
{
    [JsonProperty("odata.etag")]
    public string Etag { get; set; }
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public Dictionary<string, string> Data { get; set; }
    public DateTime Timestamp { get; set; }
}
