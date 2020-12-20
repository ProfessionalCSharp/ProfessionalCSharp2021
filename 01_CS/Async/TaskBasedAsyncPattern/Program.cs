using System;
using System.Net.Http;
using System.Threading.Tasks;

bool exit = false;

do
{
    using HttpClient httpClient = new();

    Console.WriteLine("enter an URL or exit to stop: ");
    string? uri = Console.ReadLine();
    if (uri != null && uri.ToLower() != "exit")
    {
        try
        {
            using HttpResponseMessage response = await httpClient.GetAsync(new Uri(uri));
            if (response.IsSuccessStatusCode)
            {
                string html = await response.Content.ReadAsStringAsync();
                Console.WriteLine(html[..200]);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"status code: {response.StatusCode}");
            }
        }
        catch (UriFormatException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (TaskCanceledException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    else
    {
        exit = true;
    }

} while (!exit);
