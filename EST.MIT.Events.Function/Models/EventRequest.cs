using System.Text.Json.Serialization;

namespace Events.Function.Models;

public class EventRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("properties")]
    public EventProperties Properties { get; set; }
}