
#nullable enable


using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EmailsDone
{
    public sealed class GetQuotaResponse
    {
        [JsonPropertyName("ok")]
        public bool Ok { get; set; }

        [JsonPropertyName("quota")]
        public Dictionary<string, object>? Quota { get; set; }
    }

    public sealed class GetRecipientStatusRecipientDelivery
    {
        [JsonPropertyName("complainedAt")]
        public DateTimeOffset? ComplainedAt { get; set; }

        [JsonPropertyName("complaintCount")]
        public decimal ComplaintCount { get; set; }

        [JsonPropertyName("cooldownUntil")]
        public DateTimeOffset? CooldownUntil { get; set; }

        [JsonPropertyName("hardBounceCount")]
        public decimal HardBounceCount { get; set; }
    }

    public sealed class GetRecipientStatusRecipientSubscription
    {
        [JsonPropertyName("resubscribedAt")]
        public DateTimeOffset? ResubscribedAt { get; set; }

        [JsonPropertyName("scope")]
        public string? Scope { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("unsubscribedAt")]
        public DateTimeOffset? UnsubscribedAt { get; set; }
    }

    public sealed class GetRecipientStatusRecipient
    {
        [JsonPropertyName("canSendNotifications")]
        public bool CanSendNotifications { get; set; }

        [JsonPropertyName("delivery")]
        public GetRecipientStatusRecipientDelivery? Delivery { get; set; }

        [JsonPropertyName("emailMasked")]
        public string? EmailMasked { get; set; }

        [JsonPropertyName("recipientDomain")]
        public string? RecipientDomain { get; set; }

        [JsonPropertyName("state")]
        public string? State { get; set; }

        [JsonPropertyName("subscription")]
        public GetRecipientStatusRecipientSubscription? Subscription { get; set; }
    }

    public sealed class GetRecipientStatusResponse
    {
        [JsonPropertyName("messages")]
        public List<object>? Messages { get; set; }

        [JsonPropertyName("ok")]
        public bool Ok { get; set; }

        [JsonPropertyName("recipient")]
        public GetRecipientStatusRecipient? Recipient { get; set; }
    }

    public sealed class ResubscribeRecipientResponse
    {
        [JsonPropertyName("ok")]
        public bool Ok { get; set; }
    }
}
