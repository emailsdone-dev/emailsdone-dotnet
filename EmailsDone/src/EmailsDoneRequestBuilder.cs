
#nullable enable


using System;
using System.Collections.Generic;

namespace EmailsDone
{
    internal static class EmailsDoneRequestBuilder
    {
        internal static void Set(IDictionary<string, object> target, string path, object? value)
        {
            if (value == null)
            {
                return;
            }

            var parts = path.Split('.');
            IDictionary<string, object> current = target;

            for (var index = 0; index < parts.Length; index += 1)
            {
                var part = parts[index];

                if (string.IsNullOrWhiteSpace(part))
                {
                    throw new ArgumentException("Template data path cannot contain empty segments.", nameof(path));
                }

                if (index == parts.Length - 1)
                {
                    current[part] = value;
                    return;
                }

                if (!current.TryGetValue(part, out var existing) || !(existing is IDictionary<string, object> next))
                {
                    next = new Dictionary<string, object>();
                    current[part] = next;
                }

                current = next;
            }
        }
    }
}
