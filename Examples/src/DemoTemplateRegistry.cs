
#nullable enable


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace EmailsDone.Examples
{
    internal sealed class DemoParameter
    {
        public DemoParameter(string key, string label)
        {
            Key = key;
            Label = label;
        }

        public string Key { get; }

        public string Label { get; }
    }

    internal sealed class DemoTemplate
    {
        public DemoTemplate(
            string groupName,
            string templateName,
            IReadOnlyList<DemoParameter> parameters,
            Func<EmailsDoneClient, IReadOnlyDictionary<string, string>, Task<SendEmailResponse>> executeAsync)
        {
            GroupName = groupName;
            TemplateName = templateName;
            Parameters = parameters;
            ExecuteAsync = executeAsync;
        }

        public string GroupName { get; }

        public string TemplateName { get; }

        public IReadOnlyList<DemoParameter> Parameters { get; }

        public Func<EmailsDoneClient, IReadOnlyDictionary<string, string>, Task<SendEmailResponse>> ExecuteAsync { get; }
    }

    internal static class DemoTemplateRegistry
    {
        public static readonly IReadOnlyList<DemoTemplate> All = new ReadOnlyCollection<DemoTemplate>(new[]
        {
            new DemoTemplate(
                "Authentication",
                "Account Locked",
                new[]
                {
                    new DemoParameter("to", "Recipient email")
                },
                (emailsDone, values) => emailsDone
                    .Authentication()
                    .AccountLocked()
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Authentication",
                "Email Changed",
                new[]
                {
                    new DemoParameter("to", "Recipient email")
                },
                (emailsDone, values) => emailsDone
                    .Authentication()
                    .EmailChanged()
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Authentication",
                "Login Code",
                new[]
                {
                    new DemoParameter("to", "Recipient email"),
                    new DemoParameter("code", "Code")
                },
                (emailsDone, values) => emailsDone
                    .Authentication()
                    .LoginCode(values["code"])
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Authentication",
                "Magic Link",
                new[]
                {
                    new DemoParameter("to", "Recipient email"),
                    new DemoParameter("actionButtonUrl", "Url")
                },
                (emailsDone, values) => emailsDone
                    .Authentication()
                    .MagicLink(values["actionButtonUrl"])
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Authentication",
                "Mfa Disabled",
                new[]
                {
                    new DemoParameter("to", "Recipient email")
                },
                (emailsDone, values) => emailsDone
                    .Authentication()
                    .MfaDisabled()
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Authentication",
                "Mfa Enabled",
                new[]
                {
                    new DemoParameter("to", "Recipient email")
                },
                (emailsDone, values) => emailsDone
                    .Authentication()
                    .MfaEnabled()
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Authentication",
                "New Device Login",
                new[]
                {
                    new DemoParameter("to", "Recipient email")
                },
                (emailsDone, values) => emailsDone
                    .Authentication()
                    .NewDeviceLogin()
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Authentication",
                "Password Changed",
                new[]
                {
                    new DemoParameter("to", "Recipient email")
                },
                (emailsDone, values) => emailsDone
                    .Authentication()
                    .PasswordChanged()
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Authentication",
                "Password Reset",
                new[]
                {
                    new DemoParameter("to", "Recipient email"),
                    new DemoParameter("actionButtonUrl", "Url")
                },
                (emailsDone, values) => emailsDone
                    .Authentication()
                    .PasswordReset(values["actionButtonUrl"])
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Authentication",
                "Suspicious Login",
                new[]
                {
                    new DemoParameter("to", "Recipient email")
                },
                (emailsDone, values) => emailsDone
                    .Authentication()
                    .SuspiciousLogin()
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Authentication",
                "Verify Email",
                new[]
                {
                    new DemoParameter("to", "Recipient email"),
                    new DemoParameter("actionButtonUrl", "Url")
                },
                (emailsDone, values) => emailsDone
                    .Authentication()
                    .VerifyEmail(values["actionButtonUrl"])
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Authentication",
                "Welcome",
                new[]
                {
                    new DemoParameter("to", "Recipient email"),
                    new DemoParameter("actionButtonUrl", "Url")
                },
                (emailsDone, values) => emailsDone
                    .Authentication()
                    .Welcome(values["actionButtonUrl"])
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Billing",
                "Invoice Overdue",
                new[]
                {
                    new DemoParameter("to", "Recipient email"),
                    new DemoParameter("actionButtonUrl", "Url")
                },
                (emailsDone, values) => emailsDone
                    .Billing()
                    .InvoiceOverdue(values["actionButtonUrl"])
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Billing",
                "Payment Failed",
                new[]
                {
                    new DemoParameter("to", "Recipient email"),
                    new DemoParameter("actionButtonUrl", "Url")
                },
                (emailsDone, values) => emailsDone
                    .Billing()
                    .PaymentFailed(values["actionButtonUrl"])
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Billing",
                "Payment Succeeded",
                new[]
                {
                    new DemoParameter("to", "Recipient email")
                },
                (emailsDone, values) => emailsDone
                    .Billing()
                    .PaymentSucceeded()
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Billing",
                "Refund Issued",
                new[]
                {
                    new DemoParameter("to", "Recipient email")
                },
                (emailsDone, values) => emailsDone
                    .Billing()
                    .RefundIssued()
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Billing",
                "Subscription Cancelled",
                new[]
                {
                    new DemoParameter("to", "Recipient email")
                },
                (emailsDone, values) => emailsDone
                    .Billing()
                    .SubscriptionCancelled()
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Billing",
                "Subscription Paused",
                new[]
                {
                    new DemoParameter("to", "Recipient email")
                },
                (emailsDone, values) => emailsDone
                    .Billing()
                    .SubscriptionPaused()
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Billing",
                "Subscription Renewed",
                new[]
                {
                    new DemoParameter("to", "Recipient email")
                },
                (emailsDone, values) => emailsDone
                    .Billing()
                    .SubscriptionRenewed()
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Billing",
                "Subscription Started",
                new[]
                {
                    new DemoParameter("to", "Recipient email")
                },
                (emailsDone, values) => emailsDone
                    .Billing()
                    .SubscriptionStarted()
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Billing",
                "Trial Ending",
                new[]
                {
                    new DemoParameter("to", "Recipient email"),
                    new DemoParameter("actionButtonUrl", "Url"),
                    new DemoParameter("trialEndDate", "Trial End Date")
                },
                (emailsDone, values) => emailsDone
                    .Billing()
                    .TrialEnding(values["actionButtonUrl"], values["trialEndDate"])
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Billing",
                "Trial Started",
                new[]
                {
                    new DemoParameter("to", "Recipient email")
                },
                (emailsDone, values) => emailsDone
                    .Billing()
                    .TrialStarted()
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Billing",
                "Usage Threshold",
                new[]
                {
                    new DemoParameter("to", "Recipient email"),
                    new DemoParameter("message", "Message")
                },
                (emailsDone, values) => emailsDone
                    .Billing()
                    .UsageThreshold(values["message"])
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Developer",
                "Api Key Created",
                new[]
                {
                    new DemoParameter("to", "Recipient email")
                },
                (emailsDone, values) => emailsDone
                    .Developer()
                    .ApiKeyCreated()
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Developer",
                "Api Key Rotated",
                new[]
                {
                    new DemoParameter("to", "Recipient email")
                },
                (emailsDone, values) => emailsDone
                    .Developer()
                    .ApiKeyRotated()
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Developer",
                "Credits Exhausted",
                new[]
                {
                    new DemoParameter("to", "Recipient email"),
                    new DemoParameter("actionButtonUrl", "Url")
                },
                (emailsDone, values) => emailsDone
                    .Developer()
                    .CreditsExhausted(values["actionButtonUrl"])
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Developer",
                "Credits Low",
                new[]
                {
                    new DemoParameter("to", "Recipient email")
                },
                (emailsDone, values) => emailsDone
                    .Developer()
                    .CreditsLow()
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Notification",
                "Announcement",
                new[]
                {
                    new DemoParameter("to", "Recipient email"),
                    new DemoParameter("message", "Message")
                },
                (emailsDone, values) => emailsDone
                    .Notification()
                    .Announcement(values["message"])
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Notification",
                "Approval Approved",
                new[]
                {
                    new DemoParameter("to", "Recipient email"),
                    new DemoParameter("message", "Message")
                },
                (emailsDone, values) => emailsDone
                    .Notification()
                    .ApprovalApproved(values["message"])
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Notification",
                "Approval Needed",
                new[]
                {
                    new DemoParameter("to", "Recipient email"),
                    new DemoParameter("actionButtonUrl", "Url")
                },
                (emailsDone, values) => emailsDone
                    .Notification()
                    .ApprovalNeeded(values["actionButtonUrl"])
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Notification",
                "Approval Rejected",
                new[]
                {
                    new DemoParameter("to", "Recipient email"),
                    new DemoParameter("message", "Message")
                },
                (emailsDone, values) => emailsDone
                    .Notification()
                    .ApprovalRejected(values["message"])
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Notification",
                "Digest",
                new[]
                {
                    new DemoParameter("to", "Recipient email")
                },
                (emailsDone, values) => emailsDone
                    .Notification()
                    .Digest()
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Notification",
                "Export Ready",
                new[]
                {
                    new DemoParameter("to", "Recipient email"),
                    new DemoParameter("actionButtonUrl", "Url")
                },
                (emailsDone, values) => emailsDone
                    .Notification()
                    .ExportReady(values["actionButtonUrl"])
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Notification",
                "Generation Complete",
                new[]
                {
                    new DemoParameter("to", "Recipient email"),
                    new DemoParameter("actionButtonUrl", "Url")
                },
                (emailsDone, values) => emailsDone
                    .Notification()
                    .GenerationComplete(values["actionButtonUrl"])
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Notification",
                "Import Complete",
                new[]
                {
                    new DemoParameter("to", "Recipient email")
                },
                (emailsDone, values) => emailsDone
                    .Notification()
                    .ImportComplete()
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Notification",
                "Job Complete",
                new[]
                {
                    new DemoParameter("to", "Recipient email")
                },
                (emailsDone, values) => emailsDone
                    .Notification()
                    .JobComplete()
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Notification",
                "Notification Alert",
                new[]
                {
                    new DemoParameter("to", "Recipient email"),
                    new DemoParameter("message", "Message")
                },
                (emailsDone, values) => emailsDone
                    .Notification()
                    .NotificationAlert(values["message"])
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Notification",
                "Notification Info",
                new[]
                {
                    new DemoParameter("to", "Recipient email"),
                    new DemoParameter("message", "Message")
                },
                (emailsDone, values) => emailsDone
                    .Notification()
                    .NotificationInfo(values["message"])
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Notification",
                "Notification Success",
                new[]
                {
                    new DemoParameter("to", "Recipient email"),
                    new DemoParameter("message", "Message")
                },
                (emailsDone, values) => emailsDone
                    .Notification()
                    .NotificationSuccess(values["message"])
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Notification",
                "Notification Warning",
                new[]
                {
                    new DemoParameter("to", "Recipient email"),
                    new DemoParameter("message", "Message")
                },
                (emailsDone, values) => emailsDone
                    .Notification()
                    .NotificationWarning(values["message"])
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Notification",
                "Processing Failed",
                new[]
                {
                    new DemoParameter("to", "Recipient email")
                },
                (emailsDone, values) => emailsDone
                    .Notification()
                    .ProcessingFailed()
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Notification",
                "Queued Request Ready",
                new[]
                {
                    new DemoParameter("to", "Recipient email"),
                    new DemoParameter("actionButtonUrl", "Url")
                },
                (emailsDone, values) => emailsDone
                    .Notification()
                    .QueuedRequestReady(values["actionButtonUrl"])
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Notification",
                "Reminder",
                new[]
                {
                    new DemoParameter("to", "Recipient email"),
                    new DemoParameter("message", "Message")
                },
                (emailsDone, values) => emailsDone
                    .Notification()
                    .Reminder(values["message"])
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Notification",
                "Report Ready",
                new[]
                {
                    new DemoParameter("to", "Recipient email"),
                    new DemoParameter("actionButtonUrl", "Url")
                },
                (emailsDone, values) => emailsDone
                    .Notification()
                    .ReportReady(values["actionButtonUrl"])
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Notification",
                "Upload Complete",
                new[]
                {
                    new DemoParameter("to", "Recipient email")
                },
                (emailsDone, values) => emailsDone
                    .Notification()
                    .UploadComplete()
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Team",
                "Invitation Accepted",
                new[]
                {
                    new DemoParameter("to", "Recipient email")
                },
                (emailsDone, values) => emailsDone
                    .Team()
                    .InvitationAccepted()
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Team",
                "Invited To Workspace",
                new[]
                {
                    new DemoParameter("to", "Recipient email"),
                    new DemoParameter("actionButtonUrl", "Url")
                },
                (emailsDone, values) => emailsDone
                    .Team()
                    .InvitedToWorkspace(values["actionButtonUrl"])
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Team",
                "Role Changed",
                new[]
                {
                    new DemoParameter("to", "Recipient email")
                },
                (emailsDone, values) => emailsDone
                    .Team()
                    .RoleChanged()
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Team",
                "Team Member Added",
                new[]
                {
                    new DemoParameter("to", "Recipient email")
                },
                (emailsDone, values) => emailsDone
                    .Team()
                    .TeamMemberAdded()
                    .Send(values["to"])
            ),
            new DemoTemplate(
                "Team",
                "Team Member Removed",
                new[]
                {
                    new DemoParameter("to", "Recipient email")
                },
                (emailsDone, values) => emailsDone
                    .Team()
                    .TeamMemberRemoved()
                    .Send(values["to"])
            )
        });
    }
}
