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

var client = EmailsDoneClient.FromApiKey(
    Environment.GetEnvironmentVariable("EMAILSDONE_API_KEY")
);

await client.Send.Authentication.WelcomeAsync("user@example.com", actionButtonUrl: "https://app.example.com/action");
```

Templates with required fields expose those fields as typed parameters:

```csharp
await client.Send.Authentication.LoginCodeAsync(
    "user@example.com",
    code: "123456"
);
```

Optional template fields and send controls use options objects:

```csharp
await client.Send.Authentication.LoginCodeAsync(
    "user@example.com",
    code: "123456",
    options: new LoginCodeOptions
    {
        FooterNote = "If you did not request this code, you can safely ignore this email.",
        FromName = "Acme App",
        IdempotencyKey = "email-user-123-v1"
    }
);
```

## Idempotency

Use an idempotency key for password resets, billing emails, and other flows where your app or worker may retry the same send.

```csharp
await client.Send.Authentication.PasswordResetAsync(
    "user@example.com",
    actionButtonUrl: resetUrl,
    options: new PasswordResetOptions
    {
        IdempotencyKey = $"password-reset-{userId}-{tokenId}"
    }
);
```

## Fluent template groups

The generated client mirrors EmailsDone template categories:

- `client.Send.Authentication`
- `client.Send.Billing`
- `client.Send.Developer`
- `client.Send.Notification`
- `client.Send.Team`

Each method sends a named EmailsDone template through `/v1/send`. 
