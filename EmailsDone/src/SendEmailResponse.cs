
#nullable enable


using System.Text.Json.Serialization;

namespace EmailsDone
{
    public sealed class SendEmailResponse
    {
        [JsonPropertyName("ok")]
        public bool Ok { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("messageId")]
        public string? MessageId { get; set; }

        [JsonPropertyName("idempotent")]
        public bool Idempotent { get; set; }
    }

    internal sealed class EmailsDoneErrorResponse
    {
        [JsonPropertyName("ok")]
        public bool Ok { get; set; }

        [JsonPropertyName("error")]
        public string? Error { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}
