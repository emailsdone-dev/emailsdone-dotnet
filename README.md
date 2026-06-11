# EmailsDone for .NET

Template-first transactional email for developers who do not care about email.

This SDK is generated from the EmailsDone OpenAPI contract.

## Install

```bash
dotnet add package EmailsDone
```

## API key

Store your EmailsDone API key in server-side configuration. Environment variables are the simplest starting point:

```bash
EMAILSDONE_API_KEY=ed_...
```

Do not put this key in browser JavaScript, mobile apps, public frontend configuration, source control, or client-side logs.

## Send an email

```csharp
using EmailsDone;

var emailsDone = EmailsDoneClient.FromApiKey(
    Environment.GetEnvironmentVariable("EMAILSDONE_API_KEY")
);

await emailsDone.Authentication().Welcome("https://app.example.com/action").Send("user@example.com");
```

Templates with required fields expose those fields as typed parameters:

```csharp
await emailsDone
    .Authentication()
    .LoginCode(
        "123456"
    )
    .Send("user@example.com");
```

Optional template fields and send controls use options objects:

```csharp
await emailsDone
    .Authentication()
    .LoginCode(
        new LoginCodeOptions
        {
            Code = "123456",
            FooterNote = "If you did not request this code, you can safely ignore this email.",
            FromName = "Acme App",
            IdempotencyKey = "email-user-123-v1"
        }
    )
    .Send("user@example.com");
```

## Recipient status

```csharp
var recipientStatus = await emailsDone
    .Recipient("user@example.com")
    .GetStatus();

if (recipientStatus.Recipient?.Subscription?.Status != "subscribed")
{
    await emailsDone
        .Recipient("user@example.com")
        .Resubscribe();
}
```

## Quota

```csharp
var quota = await emailsDone.GetQuota();
```

## Idempotency

Use an idempotency key for password resets, billing emails, and other flows where your app or worker may retry the same send.

```csharp
await emailsDone
    .Billing()
    .PaymentFailed(
        new PaymentFailedOptions
        {
            ActionButtonUrl = billingUrl,
            IdempotencyKey = $"payment-failed-{invoiceId}"
        }
    )
    .Send("user@example.com");
```

## Fluent template groups

The generated client mirrors EmailsDone template categories and recipient resource actions:

- `await emailsDone.GetQuota()`
- `emailsDone.Recipient(email).GetStatus()`
- `emailsDone.Recipient(email).Resubscribe()`
- `emailsDone.Authentication()`
- `emailsDone.Billing()`
- `emailsDone.Developer()`
- `emailsDone.Notification()`
- `emailsDone.Team()`

Each method sends a named EmailsDone template through `/v1/send`. 
