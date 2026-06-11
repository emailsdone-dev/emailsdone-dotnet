
#nullable enable


using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EmailsDone
{
    public sealed class RecipientClient
    {
        private readonly EmailsDoneClient _client;
        private readonly string _email;

        internal RecipientClient(EmailsDoneClient client, string email)
        {
            _client = client;
            _email = email;
        }

        public Task<GetRecipientStatusResponse> GetStatus(int? limit = null, CancellationToken cancellationToken = default)
        {
            return _client.GetRecipientStatusAsync(_email, limit, cancellationToken);
        }

        public Task<ResubscribeRecipientResponse> Resubscribe(CancellationToken cancellationToken = default)
        {
            return _client.ResubscribeRecipientAsync(_email, cancellationToken);
        }
    }

    public sealed class AuthenticationTemplates
    {
        private readonly EmailsDoneClient _client;

        internal AuthenticationTemplates(EmailsDoneClient client)
        {
            _client = client;
        }

        public PendingTemplateSend AccountLocked(AccountLockedOptions? options = null)
        {
            var data = new Dictionary<string, object>();
            options?.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "account-locked", "v1", data, options);
        }

        public PendingTemplateSend EmailChanged(EmailChangedOptions? options = null)
        {
            var data = new Dictionary<string, object>();
            options?.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "email-changed", "v1", data, options);
        }

        public PendingTemplateSend LoginCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException("code is required.", nameof(code));
            }

            return LoginCode(new LoginCodeOptions
            {
                Code = code
            });
        }

        public PendingTemplateSend LoginCode(LoginCodeOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.Code))
            {
                throw new ArgumentException("code is required.", nameof(options));
            }

            var data = new Dictionary<string, object>();
            options.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "login-code", "v1", data, options);
        }

        public PendingTemplateSend MagicLink(string actionButtonUrl)
        {
            if (string.IsNullOrWhiteSpace(actionButtonUrl))
            {
                throw new ArgumentException("actionButton.url is required.", nameof(actionButtonUrl));
            }

            return MagicLink(new MagicLinkOptions
            {
                ActionButtonUrl = actionButtonUrl
            });
        }

        public PendingTemplateSend MagicLink(MagicLinkOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.ActionButtonUrl))
            {
                throw new ArgumentException("actionButton.url is required.", nameof(options));
            }

            var data = new Dictionary<string, object>();
            options.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "magic-link", "v1", data, options);
        }

        public PendingTemplateSend MfaDisabled(MfaDisabledOptions? options = null)
        {
            var data = new Dictionary<string, object>();
            options?.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "mfa-disabled", "v1", data, options);
        }

        public PendingTemplateSend MfaEnabled(MfaEnabledOptions? options = null)
        {
            var data = new Dictionary<string, object>();
            options?.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "mfa-enabled", "v1", data, options);
        }

        public PendingTemplateSend NewDeviceLogin(NewDeviceLoginOptions? options = null)
        {
            var data = new Dictionary<string, object>();
            options?.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "new-device-login", "v1", data, options);
        }

        public PendingTemplateSend PasswordChanged(PasswordChangedOptions? options = null)
        {
            var data = new Dictionary<string, object>();
            options?.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "password-changed", "v1", data, options);
        }

        public PendingTemplateSend PasswordReset(string actionButtonUrl)
        {
            if (string.IsNullOrWhiteSpace(actionButtonUrl))
            {
                throw new ArgumentException("actionButton.url is required.", nameof(actionButtonUrl));
            }

            return PasswordReset(new PasswordResetOptions
            {
                ActionButtonUrl = actionButtonUrl
            });
        }

        public PendingTemplateSend PasswordReset(PasswordResetOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.ActionButtonUrl))
            {
                throw new ArgumentException("actionButton.url is required.", nameof(options));
            }

            var data = new Dictionary<string, object>();
            options.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "password-reset", "v1", data, options);
        }

        public PendingTemplateSend SuspiciousLogin(SuspiciousLoginOptions? options = null)
        {
            var data = new Dictionary<string, object>();
            options?.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "suspicious-login", "v1", data, options);
        }

        public PendingTemplateSend VerifyEmail(string actionButtonUrl)
        {
            if (string.IsNullOrWhiteSpace(actionButtonUrl))
            {
                throw new ArgumentException("actionButton.url is required.", nameof(actionButtonUrl));
            }

            return VerifyEmail(new VerifyEmailOptions
            {
                ActionButtonUrl = actionButtonUrl
            });
        }

        public PendingTemplateSend VerifyEmail(VerifyEmailOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.ActionButtonUrl))
            {
                throw new ArgumentException("actionButton.url is required.", nameof(options));
            }

            var data = new Dictionary<string, object>();
            options.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "verify-email", "v1", data, options);
        }

        public PendingTemplateSend Welcome(string actionButtonUrl)
        {
            if (string.IsNullOrWhiteSpace(actionButtonUrl))
            {
                throw new ArgumentException("actionButton.url is required.", nameof(actionButtonUrl));
            }

            return Welcome(new WelcomeOptions
            {
                ActionButtonUrl = actionButtonUrl
            });
        }

        public PendingTemplateSend Welcome(WelcomeOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.ActionButtonUrl))
            {
                throw new ArgumentException("actionButton.url is required.", nameof(options));
            }

            var data = new Dictionary<string, object>();
            options.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "welcome", "v1", data, options);
        }
    }

    public sealed class BillingTemplates
    {
        private readonly EmailsDoneClient _client;

        internal BillingTemplates(EmailsDoneClient client)
        {
            _client = client;
        }

        public PendingTemplateSend Invoice(object invoice, string invoiceNumber, InvoiceOptions? options = null)
        {
            if (invoice == null)
            {
                throw new ArgumentNullException(nameof(invoice));
            }

            if (string.IsNullOrWhiteSpace(invoiceNumber))
            {
                throw new ArgumentException("invoiceNumber is required.", nameof(invoiceNumber));
            }

            var data = new Dictionary<string, object>();
            EmailsDoneRequestBuilder.Set(data, "invoice", invoice);
            EmailsDoneRequestBuilder.Set(data, "invoiceNumber", invoiceNumber);
            options?.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "invoice", "v1", data, options);
        }

        public PendingTemplateSend InvoiceOverdue(string actionButtonUrl)
        {
            if (string.IsNullOrWhiteSpace(actionButtonUrl))
            {
                throw new ArgumentException("actionButton.url is required.", nameof(actionButtonUrl));
            }

            return InvoiceOverdue(new InvoiceOverdueOptions
            {
                ActionButtonUrl = actionButtonUrl
            });
        }

        public PendingTemplateSend InvoiceOverdue(InvoiceOverdueOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.ActionButtonUrl))
            {
                throw new ArgumentException("actionButton.url is required.", nameof(options));
            }

            var data = new Dictionary<string, object>();
            options.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "invoice-overdue", "v1", data, options);
        }

        public PendingTemplateSend PaymentFailed(string actionButtonUrl)
        {
            if (string.IsNullOrWhiteSpace(actionButtonUrl))
            {
                throw new ArgumentException("actionButton.url is required.", nameof(actionButtonUrl));
            }

            return PaymentFailed(new PaymentFailedOptions
            {
                ActionButtonUrl = actionButtonUrl
            });
        }

        public PendingTemplateSend PaymentFailed(PaymentFailedOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.ActionButtonUrl))
            {
                throw new ArgumentException("actionButton.url is required.", nameof(options));
            }

            var data = new Dictionary<string, object>();
            options.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "payment-failed", "v1", data, options);
        }

        public PendingTemplateSend PaymentSucceeded(PaymentSucceededOptions? options = null)
        {
            var data = new Dictionary<string, object>();
            options?.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "payment-succeeded", "v1", data, options);
        }

        public PendingTemplateSend RefundIssued(RefundIssuedOptions? options = null)
        {
            var data = new Dictionary<string, object>();
            options?.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "refund-issued", "v1", data, options);
        }

        public PendingTemplateSend SubscriptionCancelled(SubscriptionCancelledOptions? options = null)
        {
            var data = new Dictionary<string, object>();
            options?.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "subscription-cancelled", "v1", data, options);
        }

        public PendingTemplateSend SubscriptionPaused(SubscriptionPausedOptions? options = null)
        {
            var data = new Dictionary<string, object>();
            options?.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "subscription-paused", "v1", data, options);
        }

        public PendingTemplateSend SubscriptionRenewed(SubscriptionRenewedOptions? options = null)
        {
            var data = new Dictionary<string, object>();
            options?.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "subscription-renewed", "v1", data, options);
        }

        public PendingTemplateSend SubscriptionStarted(SubscriptionStartedOptions? options = null)
        {
            var data = new Dictionary<string, object>();
            options?.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "subscription-started", "v1", data, options);
        }

        public PendingTemplateSend TrialEnding(string actionButtonUrl, string trialEndDate, TrialEndingOptions? options = null)
        {
            if (string.IsNullOrWhiteSpace(actionButtonUrl))
            {
                throw new ArgumentException("actionButton.url is required.", nameof(actionButtonUrl));
            }

            if (string.IsNullOrWhiteSpace(trialEndDate))
            {
                throw new ArgumentException("trialEndDate is required.", nameof(trialEndDate));
            }

            var data = new Dictionary<string, object>();
            EmailsDoneRequestBuilder.Set(data, "actionButton.url", actionButtonUrl);
            EmailsDoneRequestBuilder.Set(data, "trialEndDate", trialEndDate);
            options?.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "trial-ending", "v1", data, options);
        }

        public PendingTemplateSend TrialStarted(TrialStartedOptions? options = null)
        {
            var data = new Dictionary<string, object>();
            options?.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "trial-started", "v1", data, options);
        }

        public PendingTemplateSend UsageThreshold(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("message is required.", nameof(message));
            }

            return UsageThreshold(new UsageThresholdOptions
            {
                Message = message
            });
        }

        public PendingTemplateSend UsageThreshold(UsageThresholdOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.Message))
            {
                throw new ArgumentException("message is required.", nameof(options));
            }

            var data = new Dictionary<string, object>();
            options.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "usage-threshold", "v1", data, options);
        }
    }

    public sealed class DeveloperTemplates
    {
        private readonly EmailsDoneClient _client;

        internal DeveloperTemplates(EmailsDoneClient client)
        {
            _client = client;
        }

        public PendingTemplateSend ApiKeyCreated(ApiKeyCreatedOptions? options = null)
        {
            var data = new Dictionary<string, object>();
            options?.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "api-key-created", "v1", data, options);
        }

        public PendingTemplateSend ApiKeyRotated(ApiKeyRotatedOptions? options = null)
        {
            var data = new Dictionary<string, object>();
            options?.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "api-key-rotated", "v1", data, options);
        }

        public PendingTemplateSend CreditsExhausted(string actionButtonUrl)
        {
            if (string.IsNullOrWhiteSpace(actionButtonUrl))
            {
                throw new ArgumentException("actionButton.url is required.", nameof(actionButtonUrl));
            }

            return CreditsExhausted(new CreditsExhaustedOptions
            {
                ActionButtonUrl = actionButtonUrl
            });
        }

        public PendingTemplateSend CreditsExhausted(CreditsExhaustedOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.ActionButtonUrl))
            {
                throw new ArgumentException("actionButton.url is required.", nameof(options));
            }

            var data = new Dictionary<string, object>();
            options.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "credits-exhausted", "v1", data, options);
        }

        public PendingTemplateSend CreditsLow(CreditsLowOptions? options = null)
        {
            var data = new Dictionary<string, object>();
            options?.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "credits-low", "v1", data, options);
        }
    }

    public sealed class NotificationTemplates
    {
        private readonly EmailsDoneClient _client;

        internal NotificationTemplates(EmailsDoneClient client)
        {
            _client = client;
        }

        public PendingTemplateSend Announcement(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("message is required.", nameof(message));
            }

            return Announcement(new AnnouncementOptions
            {
                Message = message
            });
        }

        public PendingTemplateSend Announcement(AnnouncementOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.Message))
            {
                throw new ArgumentException("message is required.", nameof(options));
            }

            var data = new Dictionary<string, object>();
            options.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "announcement", "v1", data, options);
        }

        public PendingTemplateSend ApprovalApproved(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("message is required.", nameof(message));
            }

            return ApprovalApproved(new ApprovalApprovedOptions
            {
                Message = message
            });
        }

        public PendingTemplateSend ApprovalApproved(ApprovalApprovedOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.Message))
            {
                throw new ArgumentException("message is required.", nameof(options));
            }

            var data = new Dictionary<string, object>();
            options.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "approval-approved", "v1", data, options);
        }

        public PendingTemplateSend ApprovalNeeded(string actionButtonUrl)
        {
            if (string.IsNullOrWhiteSpace(actionButtonUrl))
            {
                throw new ArgumentException("actionButton.url is required.", nameof(actionButtonUrl));
            }

            return ApprovalNeeded(new ApprovalNeededOptions
            {
                ActionButtonUrl = actionButtonUrl
            });
        }

        public PendingTemplateSend ApprovalNeeded(ApprovalNeededOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.ActionButtonUrl))
            {
                throw new ArgumentException("actionButton.url is required.", nameof(options));
            }

            var data = new Dictionary<string, object>();
            options.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "approval-needed", "v1", data, options);
        }

        public PendingTemplateSend ApprovalRejected(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("message is required.", nameof(message));
            }

            return ApprovalRejected(new ApprovalRejectedOptions
            {
                Message = message
            });
        }

        public PendingTemplateSend ApprovalRejected(ApprovalRejectedOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.Message))
            {
                throw new ArgumentException("message is required.", nameof(options));
            }

            var data = new Dictionary<string, object>();
            options.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "approval-rejected", "v1", data, options);
        }

        public PendingTemplateSend Digest(DigestOptions? options = null)
        {
            var data = new Dictionary<string, object>();
            options?.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "digest", "v1", data, options);
        }

        public PendingTemplateSend ExportReady(string actionButtonUrl)
        {
            if (string.IsNullOrWhiteSpace(actionButtonUrl))
            {
                throw new ArgumentException("actionButton.url is required.", nameof(actionButtonUrl));
            }

            return ExportReady(new ExportReadyOptions
            {
                ActionButtonUrl = actionButtonUrl
            });
        }

        public PendingTemplateSend ExportReady(ExportReadyOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.ActionButtonUrl))
            {
                throw new ArgumentException("actionButton.url is required.", nameof(options));
            }

            var data = new Dictionary<string, object>();
            options.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "export-ready", "v1", data, options);
        }

        public PendingTemplateSend GenerationComplete(string actionButtonUrl)
        {
            if (string.IsNullOrWhiteSpace(actionButtonUrl))
            {
                throw new ArgumentException("actionButton.url is required.", nameof(actionButtonUrl));
            }

            return GenerationComplete(new GenerationCompleteOptions
            {
                ActionButtonUrl = actionButtonUrl
            });
        }

        public PendingTemplateSend GenerationComplete(GenerationCompleteOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.ActionButtonUrl))
            {
                throw new ArgumentException("actionButton.url is required.", nameof(options));
            }

            var data = new Dictionary<string, object>();
            options.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "generation-complete", "v1", data, options);
        }

        public PendingTemplateSend ImportComplete(ImportCompleteOptions? options = null)
        {
            var data = new Dictionary<string, object>();
            options?.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "import-complete", "v1", data, options);
        }

        public PendingTemplateSend JobComplete(JobCompleteOptions? options = null)
        {
            var data = new Dictionary<string, object>();
            options?.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "job-complete", "v1", data, options);
        }

        public PendingTemplateSend NotificationAlert(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("message is required.", nameof(message));
            }

            return NotificationAlert(new NotificationAlertOptions
            {
                Message = message
            });
        }

        public PendingTemplateSend NotificationAlert(NotificationAlertOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.Message))
            {
                throw new ArgumentException("message is required.", nameof(options));
            }

            var data = new Dictionary<string, object>();
            options.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "notification-alert", "v1", data, options);
        }

        public PendingTemplateSend NotificationInfo(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("message is required.", nameof(message));
            }

            return NotificationInfo(new NotificationInfoOptions
            {
                Message = message
            });
        }

        public PendingTemplateSend NotificationInfo(NotificationInfoOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.Message))
            {
                throw new ArgumentException("message is required.", nameof(options));
            }

            var data = new Dictionary<string, object>();
            options.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "notification-info", "v1", data, options);
        }

        public PendingTemplateSend NotificationSuccess(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("message is required.", nameof(message));
            }

            return NotificationSuccess(new NotificationSuccessOptions
            {
                Message = message
            });
        }

        public PendingTemplateSend NotificationSuccess(NotificationSuccessOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.Message))
            {
                throw new ArgumentException("message is required.", nameof(options));
            }

            var data = new Dictionary<string, object>();
            options.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "notification-success", "v1", data, options);
        }

        public PendingTemplateSend NotificationWarning(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("message is required.", nameof(message));
            }

            return NotificationWarning(new NotificationWarningOptions
            {
                Message = message
            });
        }

        public PendingTemplateSend NotificationWarning(NotificationWarningOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.Message))
            {
                throw new ArgumentException("message is required.", nameof(options));
            }

            var data = new Dictionary<string, object>();
            options.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "notification-warning", "v1", data, options);
        }

        public PendingTemplateSend ProcessingFailed(ProcessingFailedOptions? options = null)
        {
            var data = new Dictionary<string, object>();
            options?.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "processing-failed", "v1", data, options);
        }

        public PendingTemplateSend QueuedRequestReady(string actionButtonUrl)
        {
            if (string.IsNullOrWhiteSpace(actionButtonUrl))
            {
                throw new ArgumentException("actionButton.url is required.", nameof(actionButtonUrl));
            }

            return QueuedRequestReady(new QueuedRequestReadyOptions
            {
                ActionButtonUrl = actionButtonUrl
            });
        }

        public PendingTemplateSend QueuedRequestReady(QueuedRequestReadyOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.ActionButtonUrl))
            {
                throw new ArgumentException("actionButton.url is required.", nameof(options));
            }

            var data = new Dictionary<string, object>();
            options.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "queued-request-ready", "v1", data, options);
        }

        public PendingTemplateSend Reminder(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("message is required.", nameof(message));
            }

            return Reminder(new ReminderOptions
            {
                Message = message
            });
        }

        public PendingTemplateSend Reminder(ReminderOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.Message))
            {
                throw new ArgumentException("message is required.", nameof(options));
            }

            var data = new Dictionary<string, object>();
            options.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "reminder", "v1", data, options);
        }

        public PendingTemplateSend ReportReady(string actionButtonUrl)
        {
            if (string.IsNullOrWhiteSpace(actionButtonUrl))
            {
                throw new ArgumentException("actionButton.url is required.", nameof(actionButtonUrl));
            }

            return ReportReady(new ReportReadyOptions
            {
                ActionButtonUrl = actionButtonUrl
            });
        }

        public PendingTemplateSend ReportReady(ReportReadyOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.ActionButtonUrl))
            {
                throw new ArgumentException("actionButton.url is required.", nameof(options));
            }

            var data = new Dictionary<string, object>();
            options.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "report-ready", "v1", data, options);
        }

        public PendingTemplateSend UploadComplete(UploadCompleteOptions? options = null)
        {
            var data = new Dictionary<string, object>();
            options?.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "upload-complete", "v1", data, options);
        }
    }

    public sealed class TeamTemplates
    {
        private readonly EmailsDoneClient _client;

        internal TeamTemplates(EmailsDoneClient client)
        {
            _client = client;
        }

        public PendingTemplateSend InvitationAccepted(InvitationAcceptedOptions? options = null)
        {
            var data = new Dictionary<string, object>();
            options?.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "invitation-accepted", "v1", data, options);
        }

        public PendingTemplateSend InvitedToWorkspace(string actionButtonUrl)
        {
            if (string.IsNullOrWhiteSpace(actionButtonUrl))
            {
                throw new ArgumentException("actionButton.url is required.", nameof(actionButtonUrl));
            }

            return InvitedToWorkspace(new InvitedToWorkspaceOptions
            {
                ActionButtonUrl = actionButtonUrl
            });
        }

        public PendingTemplateSend InvitedToWorkspace(InvitedToWorkspaceOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.ActionButtonUrl))
            {
                throw new ArgumentException("actionButton.url is required.", nameof(options));
            }

            var data = new Dictionary<string, object>();
            options.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "invited-to-workspace", "v1", data, options);
        }

        public PendingTemplateSend RoleChanged(RoleChangedOptions? options = null)
        {
            var data = new Dictionary<string, object>();
            options?.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "role-changed", "v1", data, options);
        }

        public PendingTemplateSend TeamMemberAdded(TeamMemberAddedOptions? options = null)
        {
            var data = new Dictionary<string, object>();
            options?.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "team-member-added", "v1", data, options);
        }

        public PendingTemplateSend TeamMemberRemoved(TeamMemberRemovedOptions? options = null)
        {
            var data = new Dictionary<string, object>();
            options?.ApplyTemplateData(data);

            return new PendingTemplateSend(_client, "team-member-removed", "v1", data, options);
        }
    }

}
