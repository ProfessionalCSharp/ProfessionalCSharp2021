using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCSample.Models;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MVCSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Books()
        {
            IEnumerable<Book> books = Enumerable.Range(6, 12)
                .Select(i => new Book(i, $"Professional C# {i}", "Wrox Press")).ToArray();
            return View(books);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public record Book(int Id, string Title, string Publisher);
}
