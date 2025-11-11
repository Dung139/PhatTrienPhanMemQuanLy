using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DemoMvc069.Models;

namespace DemoMvc069.Controllers;

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
    [HttpPost]
    public IActionResult Index(string FullName, string Address)
    {
        string message = $"Xin chào {FullName} đến từ {Address}";
        ViewBag.Message = message;
        return View();
    }

}
// Nguyễn Đình Dũng - 2121050069