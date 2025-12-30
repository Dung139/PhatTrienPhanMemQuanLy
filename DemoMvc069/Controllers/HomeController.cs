using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DemoMvc069.Models;

namespace DemoMvc069.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
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

    [HttpPost]
    public IActionResult Index(string FullName, string Address)
    {
        string strOutput = "Xin chào " + FullName + " đến từ " + Address;
        ViewBag.Message = strOutput;
        return View();
    }   
}
