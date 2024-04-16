using System;
using System.Text.Json.Serialization;

namespace Events.Function.Models;

public class EventAction
{
    [JsonPropertyName("type")]
    public string Type { get; set; }
    [JsonPropertyName("message")]
    public string Message { get; set; }
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }
    [JsonPropertyName("data")]
    public string Data { get; set; }
}