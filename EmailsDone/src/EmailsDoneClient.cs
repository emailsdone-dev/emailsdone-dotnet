
#nullable enable


using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EmailsDone
{
    public sealed class EmailsDoneClient : IDisposable
    {
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        private readonly string _apiKey;
        private readonly HttpClient _httpClient;
        private readonly bool _disposeHttpClient;
        private readonly Uri _baseUri;
        private readonly AuthenticationTemplates _authentication;
        private readonly BillingTemplates _billing;
        private readonly DeveloperTemplates _developer;
        private readonly NotificationTemplates _notification;
        private readonly TeamTemplates _team;

        private EmailsDoneClient(string apiKey, HttpClient? httpClient, EmailsDoneClientOptions? options)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("An EmailsDone API key is required. Store it in server-side configuration, not frontend code.", nameof(apiKey));
            }

            _apiKey = apiKey;
            _httpClient = httpClient ?? new HttpClient();
            _disposeHttpClient = httpClient == null;
            _baseUri = normaliseBaseUri(options?.ApiBaseUrl ?? "https://api.emailsdone.dev");
            _authentication = new AuthenticationTemplates(this);
            _billing = new BillingTemplates(this);
            _developer = new DeveloperTemplates(this);
            _notification = new NotificationTemplates(this);
            _team = new TeamTemplates(this);
        }

        public AuthenticationTemplates Authentication()
        {
            return _authentication;
        }

        public BillingTemplates Billing()
        {
            return _billing;
        }

        public DeveloperTemplates Developer()
        {
            return _developer;
        }

        public NotificationTemplates Notification()
        {
            return _notification;
        }

        public TeamTemplates Team()
        {
            return _team;
        }


        public Task<GetQuotaResponse> GetQuota(CancellationToken cancellationToken = default)
        {
            return RequestJsonAsync<GetQuotaResponse>(HttpMethod.Get, "v1/quota", null, null, cancellationToken);
        }

        public RecipientClient Recipient(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Recipient email address is required.", nameof(email));
            }

            return new RecipientClient(this, email);
        }

        public static EmailsDoneClient FromApiKey(string apiKey)
        {
            return new EmailsDoneClient(apiKey, null, null);
        }

        public static EmailsDoneClient FromApiKey(string apiKey, EmailsDoneClientOptions options)
        {
            return new EmailsDoneClient(apiKey, null, options);
        }

        public static EmailsDoneClient FromApiKey(string apiKey, HttpClient httpClient, EmailsDoneClientOptions? options = null)
        {
            return new EmailsDoneClient(apiKey, httpClient ?? throw new ArgumentNullException(nameof(httpClient)), options);
        }

        public Task<GetQuotaResponse> GetQuotaAsync(CancellationToken cancellationToken = default)
        {
            return GetQuota(cancellationToken);
        }

        public Task<ResubscribeRecipientResponse> ResubscribeRecipientAsync(string email, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Recipient email address is required.", nameof(email));
            }

            var payload = new Dictionary<string, object>
            {
                ["email"] = email,
                ["scope"] = "app_notifications"
            };

            return RequestJsonAsync<ResubscribeRecipientResponse>(HttpMethod.Post, "v1/recipients/resubscribe", payload, null, cancellationToken);
        }

        public Task<GetRecipientStatusResponse> GetRecipientStatusAsync(string email, int? limit = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Recipient email address is required.", nameof(email));
            }

            var payload = new Dictionary<string, object>
            {
                ["email"] = email
            };

            if (limit.HasValue)
            {
                payload["limit"] = limit.Value;
            }

            return RequestJsonAsync<GetRecipientStatusResponse>(HttpMethod.Post, "v1/recipients/status", payload, null, cancellationToken);
        }

        internal async Task<SendEmailResponse> SendTemplateAsync(
            string templateId,
            string templateVersion,
            string to,
            IDictionary<string, object> data,
            SendEmailOptions? options,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(templateId))
            {
                throw new ArgumentException("Template id is required.", nameof(templateId));
            }

            if (string.IsNullOrWhiteSpace(templateVersion))
            {
                throw new ArgumentException("Template version is required.", nameof(templateVersion));
            }

            if (string.IsNullOrWhiteSpace(to))
            {
                throw new ArgumentException("Recipient email address is required.", nameof(to));
            }

            var payload = new Dictionary<string, object>
            {
                ["templateId"] = templateId,
                ["templateVersion"] = templateVersion,
                ["to"] = to,
                ["data"] = data ?? throw new ArgumentNullException(nameof(data))
            };

            AddIfSet(payload, "from", options?.From);
            AddIfSet(payload, "fromName", options?.FromName);
            AddIfSet(payload, "replyTo", options?.ReplyTo);

            var headers = new Dictionary<string, string>();

            if (!string.IsNullOrWhiteSpace(options?.IdempotencyKey))
            {
                headers["Idempotency-Key"] = options!.IdempotencyKey!;
            }

            return await RequestJsonAsync<SendEmailResponse>(HttpMethod.Post, "v1/send", payload, headers, cancellationToken).ConfigureAwait(false);
        }

        internal async Task<TResponse> RequestJsonAsync<TResponse>(
            HttpMethod method,
            string path,
            IDictionary<string, object>? payload,
            IDictionary<string, string>? headers,
            CancellationToken cancellationToken)
        {
            using (var request = new HttpRequestMessage(method, new Uri(_baseUri, path)))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        request.Headers.Add(header.Key, header.Value);
                    }
                }

                if (payload != null)
                {
                    var json = JsonSerializer.Serialize(payload, JsonOptions);
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                }

                using (var response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false))
                {
                    var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw BuildException(response.StatusCode, responseBody);
                    }

                    var parsed = JsonSerializer.Deserialize<TResponse>(responseBody, JsonOptions);

                    if (parsed == null)
                    {
                        throw new EmailsDoneException(response.StatusCode, "invalid_response", "EmailsDone returned an empty or invalid response.", responseBody);
                    }

                    return parsed;
                }
            }
        }

        public void Dispose()
        {
            if (_disposeHttpClient)
            {
                _httpClient.Dispose();
            }
        }

        private static Uri normaliseBaseUri(string value)
        {
            if (!Uri.TryCreate(value, UriKind.Absolute, out var uri))
            {
                throw new ArgumentException("EmailsDone API base URL must be an absolute URL.", nameof(value));
            }

            var text = uri.ToString();
            return new Uri(text.EndsWith("/", StringComparison.Ordinal) ? text : text + "/");
        }

        private static void AddIfSet(IDictionary<string, object> payload, string key, string? value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                payload[key] = value!;
            }
        }

        private static EmailsDoneException BuildException(System.Net.HttpStatusCode statusCode, string responseBody)
        {
            try
            {
                var error = JsonSerializer.Deserialize<EmailsDoneErrorResponse>(responseBody, JsonOptions);
                var code = string.IsNullOrWhiteSpace(error?.Error) ? "api_error" : error!.Error!;
                var message = string.IsNullOrWhiteSpace(error?.Message) ? code : error!.Message!;
                return new EmailsDoneException(statusCode, code, message, responseBody);
            }
            catch (JsonException)
            {
                return new EmailsDoneException(statusCode, "api_error", "EmailsDone returned an error response.", responseBody);
            }
        }
    }
}
