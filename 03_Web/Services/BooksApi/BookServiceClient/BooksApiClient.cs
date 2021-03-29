using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Books.Models;
using System.Net.Http.Headers;
using System.Xml.Linq;

namespace BookServiceClient
{
    public record BooksApiClientOptions
    {
        public string? BooksApiUri { get; init; }
    }

    public class BooksApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _booksApiUri;
        private Guid? _chapterId;

        public BooksApiClient(HttpClient httpClient, IOptions<BooksApiClientOptions> options)
        {
            _httpClient = httpClient;
            _booksApiUri = options.Value.BooksApiUri ?? "api/books";
        }

        public async Task ReadChaptersAsync()
        {
            Console.WriteLine(nameof(ReadChapterAsync));
            var chapters = await _httpClient.GetFromJsonAsync<IEnumerable<BookChapter>>(_booksApiUri);
            if (chapters is null) return;
            foreach (var chapter in chapters)
            {
                Console.WriteLine($"{chapter.Number} {chapter.Title}");
            }
            _chapterId = chapters.FirstOrDefault()?.Id;
            Console.WriteLine();
        }

        public async Task ReadChapterAsync()
        {
            Console.WriteLine(nameof(ReadChapterAsync));
            if (_chapterId is not null)
            {
                string uri = $"{_booksApiUri}/{_chapterId}";
                var chapter = await _httpClient.GetFromJsonAsync<BookChapter>(uri);
                if (chapter is not null)
                {
                    Console.WriteLine($"{chapter.Number} {chapter.Title}");
                }
            }
            Console.WriteLine();            
        }

        public async Task AddChapterAsync()
        {
            Console.WriteLine(nameof(AddChapterAsync));
            var chapter = new BookChapter(Guid.NewGuid(), 25, "Services", 40);
            // TODO: check - is this throwing?
            var response = await _httpClient.PostAsJsonAsync(_booksApiUri, chapter);
            Console.WriteLine();
        }

        public async Task UpdateChapterAsync()
        {
            Console.WriteLine(nameof(UpdateChapterAsync));

            var chapters = await _httpClient.GetFromJsonAsync<IEnumerable<BookChapter>>(_booksApiUri);
            if (chapters == null) return;
            var chapter = chapters.SingleOrDefault(
              c => c.Title == ".NET Application Architectures");
            if (chapter != null)
            {
                chapter = chapter with { Title = ".NET Applications and Tools" };
                var response = await _httpClient.PutAsJsonAsync(_booksApiUri, chapter);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"updated chapter {chapter.Title}");
                }
            }
            Console.WriteLine();
        }

        public async Task RemoveChapterAsync()
        {
            Console.WriteLine(nameof(RemoveChapterAsync));
            var chapters = await _httpClient.GetFromJsonAsync<IEnumerable<BookChapter>>(_booksApiUri);
            if (chapters == null) return;

            var chapter = chapters.SingleOrDefault(c => c.Title == "ADO.NET and Transactions");
            if (chapter != null)
            {
                string uri = $"{_booksApiUri}/{chapter.Id}";
                var response = await _httpClient.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"removed chapter {chapter.Title}");
                }
            }
            Console.WriteLine();
        }



        //public async Task ReadXmlChaptersAsync()
        //{
        //    Console.WriteLine(nameof(ReadXmlChaptersAsync));
        //    HttpRequestMessage requestMessage = new(HttpMethod.Get, _booksApiUri);
        //    requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
        //    var response = await _httpClient.SendAsync(requestMessage);
        //    response.EnsureSuccessStatusCode();
        //    string xml = await response.Content.ReadAsStringAsync();
        //    XElement chapters = XElement.Parse(xml);   

        //    Console.WriteLine();
        //}
    }
}
