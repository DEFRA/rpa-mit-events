using System.Text.Json.Serialization;

namespace Events.Function.Models;

public class EventProperties
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("checkpoint")]
    public string Checkpoint { get; set; }
    [JsonPropertyName("status")]
    public string Status { get; set; }
    [JsonPropertyName("action")]
    public EventAction Action { get; set; }
}