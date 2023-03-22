using System.Text.Json.Serialization;
namespace Events.Function.Models;

public class Data
{
    [JsonPropertyName("invoiceId")]
    public string InvoiceId { get; set; }
    [JsonPropertyName("notificationType")]
    public string NotificationType { get; set; }
    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; }
    [JsonPropertyName("requestBy")]
    public string RequestBy { get; set; }
}