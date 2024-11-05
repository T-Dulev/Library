using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using Microsoft.Data.Sqlite;

namespace Library.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    //public IActionResult Index()
    //{
    //    return View();
    //}
    public IActionResult Index()
    {
        var connection = new SqliteConnection("Data Source=Library.db");
        connection.Open();

        var context = new LibraryContext(connection);
        var books = context.Books.ToList();

        return View(books);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
