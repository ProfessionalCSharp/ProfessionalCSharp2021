using System.Reflection;

namespace HttpServerSample;

public class GenerateHtml
{
    private readonly ILogger _logger;
    public GenerateHtml(ILogger<GenerateHtml> logger) => _logger = logger;

    private static string s_htmlFormat =
        "<!DOCTYPE html>\r\n<html><head><title>{0}</title></head>" +
        "<body>{1}</body></html>";

    public string GetHtmlContent(HttpRequest request)
    {
        string title = "Sample Listener using Kestrel";

        string content = $"<h1>Hello from the server</h1>" +
            $"<h2>Header Info</h2>" +
            $"{string.Join(' ', GetHeaderInfo(request.Headers))}" +
            $"<h2>Request Object Information</h2>" +
            $"{string.Join(' ', GetRequestInfo(request))}";

        return string.Format(s_htmlFormat, title, content);
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
                // the Form property throws
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                values.Add((property.Name, message));
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
