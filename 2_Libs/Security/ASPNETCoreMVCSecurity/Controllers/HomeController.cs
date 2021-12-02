using ASPNETCoreMVCSecurity.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

using System.Diagnostics;
using System.Text;

namespace ASPNETCoreMVCSecurity.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _settings;

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _settings = configuration;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public string Echo(string x) => x;

    public IActionResult EchoUnencoded(string x) => Content(x, "text/html");

    public IActionResult EchoWithView(string x)
    {
        ViewBag.SampleData = x;
        return View();
    }

    public IActionResult SqlSample(string id)
    {
        string connectionString = _settings.GetConnectionString("InjectionConnection");
        SqlConnection sqlConnection = new(connectionString);
        SqlCommand command = sqlConnection.CreateCommand();

        // don't do this - string concatenation for SQL commands!
        command.CommandText = "SELECT * FROM Customers WHERE City = " + id;
        sqlConnection.Open();
        using SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

        StringBuilder sb = new();
        while (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                sb.Append(reader[i]);
            }
            sb.AppendLine();
        }
        ViewBag.Data = sb.ToString();

        return View();
    }

    public IActionResult EditBook() => View();

    [HttpPost]
    public IActionResult EditBook(Book book) => View("EditBookResult", book);

    public IActionResult EditBookSecure() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditBookSecure(Book book) => View("EditBookResult", book);


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
