
#nullable enable


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailsDone.Examples
{
    internal static class Program
    {
        private static async Task Main()
        {
            Console.WriteLine("EmailsDone .NET console example");
            Console.WriteLine();

            var apiKey = PromptForApiKey();
            var emailsDone = EmailsDoneClient.FromApiKey(apiKey);

            try
            {
                var quota = await emailsDone.GetQuota();

                if (!quota.Ok)
                {
                    Console.WriteLine("The API key could not be validated.");
                    return;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Could not validate the API key: {exception.Message}");
                return;
            }

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1. Send Template");
                Console.WriteLine("2. Check Recipient Status");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();

                if (choice == "0")
                {
                    return;
                }

                if (choice == "1")
                {
                    await SendTemplateMenu(emailsDone);
                    continue;
                }

                if (choice == "2")
                {
                    await RecipientStatusMenu(emailsDone);
                    continue;
                }

                Console.WriteLine("Invalid option.");
            }
        }

        private static string PromptForApiKey()
        {
            while (true)
            {
                Console.Write("EmailsDone API key: ");
                var apiKey = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(apiKey))
                {
                    return apiKey;
                }

                Console.WriteLine("API key is required.");
            }
        }

        private static async Task SendTemplateMenu(EmailsDoneClient emailsDone)
        {
            var groups = DemoTemplateRegistry.All
                .Select((template) => template.GroupName)
                .Distinct(StringComparer.Ordinal)
                .OrderBy((name) => name, StringComparer.Ordinal)
                .ToList();

            if (groups.Count == 0)
            {
                Console.WriteLine("No demo templates are available.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Template groups:");

            for (var index = 0; index < groups.Count; index += 1)
            {
                Console.WriteLine($"{index + 1}. {groups[index]}");
            }

            var groupIndex = PromptForSelection(groups.Count, "Choose a group");
            var selectedGroup = groups[groupIndex];
            var templates = DemoTemplateRegistry.All
                .Where((template) => string.Equals(template.GroupName, selectedGroup, StringComparison.Ordinal))
                .OrderBy((template) => template.TemplateName, StringComparer.Ordinal)
                .ToList();

            Console.WriteLine();
            Console.WriteLine($"{selectedGroup} templates:");

            for (var index = 0; index < templates.Count; index += 1)
            {
                Console.WriteLine($"{index + 1}. {templates[index].TemplateName}");
            }

            var templateIndex = PromptForSelection(templates.Count, "Choose a template");
            var selectedTemplate = templates[templateIndex];
            var values = new Dictionary<string, string>(StringComparer.Ordinal);

            Console.WriteLine();

            foreach (var parameter in selectedTemplate.Parameters)
            {
                values[parameter.Key] = PromptForValue(parameter.Label);
            }

            try
            {
                var response = await selectedTemplate.ExecuteAsync(emailsDone, values);
                Console.WriteLine($"Send result: ok={response.Ok}, status={response.Status ?? "unknown"}, messageId={response.MessageId ?? "n/a"}, idempotent={response.Idempotent}");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Send failed: {exception.Message}");
            }
        }

        private static async Task RecipientStatusMenu(EmailsDoneClient emailsDone)
        {
            Console.WriteLine();
            var email = PromptForValue("Recipient email");

            try
            {
                var status = await emailsDone
                    .Recipient(email)
                    .GetStatus();

                var recipientStatus = status.Recipient?.Subscription?.Status;

                Console.WriteLine($"Recipient status: {recipientStatus ?? "unknown"}");

                if (!string.Equals(recipientStatus, "subscribed", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Write("Recipient is not subscribed. Resubscribe? y/n: ");
                    var choice = Console.ReadLine();

                    if (string.Equals(choice, "y", StringComparison.OrdinalIgnoreCase))
                    {
                        var response = await emailsDone
                            .Recipient(email)
                            .Resubscribe();

                        Console.WriteLine($"Resubscribe result: ok={response.Ok}");
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Recipient lookup failed: {exception.Message}");
            }
        }

        private static int PromptForSelection(int count, string label)
        {
            while (true)
            {
                Console.Write($"{label} (1-{count}): ");
                var raw = Console.ReadLine();

                if (int.TryParse(raw, out var selected) && selected >= 1 && selected <= count)
                {
                    return selected - 1;
                }

                Console.WriteLine("Invalid selection.");
            }
        }

        private static string PromptForValue(string label)
        {
            while (true)
            {
                Console.Write($"{label}: ");
                var value = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }

                Console.WriteLine($"{label} is required.");
            }
        }
    }
}
