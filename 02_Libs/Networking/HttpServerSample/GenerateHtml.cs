using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HttpServerSample
{
    public class GenerateHtml
    {
        private readonly ILogger _logger;
        public GenerateHtml(ILogger<GenerateHtml> logger)
        {
            _logger = logger;
        }

        private static string s_htmlFormat =
            "<!DOCTYPE html><html><head><title>{0}</title></head>" +
            "<body>{1}</body></html>";

        public byte[] GetHtmlContent(HttpRequest request)
        {
            string title = "Sample Listener using Kestrel";

            string content = $"<h1>Hello from the server</h1>" +
                $"<h2>Header Info</h2>" +
                $"{string.Join(' ', GetHeaderInfo(request.Headers))}" +
                $"<h2>Request Object Information</h2>" +
                $"{string.Join(' ', GetRequestInfo(request))}";

            string html = string.Format(s_htmlFormat, title, content);
            return Encoding.UTF8.GetBytes(html);
        }

        private IEnumerable<string> GetRequestInfo(HttpRequest request)
        {
            var properties = request.GetType().GetProperties();
            List<(string Key, string Value)> values = new();
            foreach (var property in properties)
            {
                try
                {
                    string? value = property.GetValue(request)?.ToString();
                    if (value != null)
                    {
                        values.Add((property.Name, value));
                    }
                }
                catch (TargetInvocationException ex)
                {
                    _logger.LogInformation("{0}: {1}", property.Name, ex.Message);
                    if (ex.InnerException != null)
                    {
                        _logger.LogInformation("\t{0}", ex.InnerException.Message);
                    }
                }
            }
            return values.Select(v => $"<div>{v.Key}: {v.Value}</div>");
        }

        private IEnumerable<string> GetHeaderInfo(IHeaderDictionary headers)
        {
            List<(string Key, string Value)> values = new();
            var keys = headers.Keys;
            foreach (var key in keys)
            {
                if (headers.TryGetValue(key, out var value))
                {
                    values.Add((key, value));
                }
            }
            return values.Select(v => $"<div>{v.Key}: {v.Value}</div>");
        }
    }
}
