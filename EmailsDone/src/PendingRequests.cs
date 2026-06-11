
#nullable enable


using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EmailsDone
{
    public sealed class PendingTemplateSend
    {
        private readonly EmailsDoneClient _client;
        private readonly string _templateId;
        private readonly string _templateVersion;
        private readonly IDictionary<string, object> _data;
        private readonly SendEmailOptions? _options;

        internal PendingTemplateSend(
            EmailsDoneClient client,
            string templateId,
            string templateVersion,
            IDictionary<string, object> data,
            SendEmailOptions? options)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _templateId = templateId ?? throw new ArgumentNullException(nameof(templateId));
            _templateVersion = templateVersion ?? throw new ArgumentNullException(nameof(templateVersion));
            _data = data ?? throw new ArgumentNullException(nameof(data));
            _options = options;
        }

        public Task<SendEmailResponse> Send(string to, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(to))
            {
                throw new ArgumentException("Recipient email address is required.", nameof(to));
            }

            return _client.SendTemplateAsync(_templateId, _templateVersion, to, _data, _options, cancellationToken);
        }

        public Task<SendEmailResponse> SendAsync(string to, CancellationToken cancellationToken = default)
        {
            return Send(to, cancellationToken);
        }
    }
}
