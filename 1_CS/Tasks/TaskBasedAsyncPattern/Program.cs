string uri = (args.Length >= 1) ? args[0] : string.Empty;
if (string.IsNullOrEmpty(uri))
{
    Console.Write("enter an URL (e.g. https://csharp.christiannagel.com): ");
    uri = Console.ReadLine() ?? throw new InvalidOperationException();
}
using HttpClient httpClient = new();
try
{
    using HttpResponseMessage response = await httpClient.GetAsync(new Uri(uri));
    if (response.IsSuccessStatusCode)
    {
        string html = await response.Content.ReadAsStringAsync();
        Console.WriteLine(html[..200]);
    }
    else
    {
        Console.WriteLine($"Status code: {response.StatusCode}");
    }
}
catch (UriFormatException ex)
{
    Console.WriteLine($"Error parsing the Uri {ex.Message}");
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"HTTP request exception: {ex.Message}");
}
catch (TaskCanceledException ex)
{
    Console.WriteLine($"Task canceled: {ex.Message}");
}