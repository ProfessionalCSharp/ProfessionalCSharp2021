using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length < 1)
            {
                ShowUsage();
                return;
            }
            await StartServerAsync(args);
            Console.ReadLine();
        }

        private static void ShowUsage() =>
            Console.WriteLine("Usage: HttpServer Prefix [Prefix2] [Prefix3] [Prefix4]");

        private static string s_htmlFormat =
            "<!DOCTYPE html><html><head><title>{0}</title></head>" +
            "<body>{1}</body></html>";

        public static async Task StartServerAsync(params string[] prefixes)
        {
            try
            {
                Console.WriteLine($"server starting at");
                var listener = new HttpListener();
                
                foreach (var prefix in prefixes)
                {
                    listener.Prefixes.Add(prefix);
                    Console.WriteLine($"\t{prefix}");
                }

                listener.Start();

                do
                {
                    HttpListenerContext context = await listener.GetContextAsync();
                    context.Response.Headers.Add(HttpResponseHeader.ContentType, "text/html");
                    context.Response.StatusCode = (int)HttpStatusCode.OK;

                    byte[] buffer = GetHtmlContent(context.Request);
                    await context.Response.OutputStream.WriteAsync(buffer, 0, buffer.Length);

                } while (true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static byte[] GetHtmlContent(HttpListenerRequest request)
        {
            string title = "Sample WebListener";

            string content = $"<h1>Hello from the server</h1>" +
                $"<h2>Header Info</h2>" +
                $"{string.Join(' ', GetHeaderInfo(request.Headers))}" +
                $"<h2>Request Object Information</h2>" +
                $"{string.Join(' ', GetRequestInfo(request))}";
        
            string html = string.Format(s_htmlFormat, title, content);
            return Encoding.UTF8.GetBytes(html);
        }

        private static IEnumerable<string> GetRequestInfo(HttpListenerRequest request)
        {
            var properties = request.GetType().GetProperties();
            var values = new List<(string Key, string Value)>();
            foreach (var property in properties)
            {
                try
                {
                    string value = property.GetValue(request)?.ToString() ?? string.Empty;
                    values.Add((property.Name, value));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{property.Name}: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"\t{ex.InnerException.Message}");
                    }
                }
            }
            return values.Select(v => $"<div>{v.Key}: {v.Value}</div>");
        }

        private static IEnumerable<string> GetHeaderInfo(NameValueCollection headers)
        {           
            return headers.Keys.Cast<string>().Select(key => $"<div>{key}: {string.Join(",", headers.GetValues(key) ?? new string[] { })}</div>");
        }
    }
}
