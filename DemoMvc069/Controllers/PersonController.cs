using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DemoMvc069.Models;

namespace DemoMvc069.Controllers;
public class PersonController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Index(Person ps)
    {
        string strOutput = $"Xin chào {ps.PersonID} - {ps.FullName} - {ps.Address}";

        ViewBag.infoPerson = strOutput;
        return View();
    }
}