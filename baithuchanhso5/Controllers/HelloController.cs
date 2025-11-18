using Microsoft.AspNetCore.Mvc;
using System;

namespace baithuchanhso5.Controllers
{
    public class HelloController : Controller
    {
        // View nhập tên và năm sinh
        public IActionResult Index()
        {
            return View();
        }

        // Nhận dữ liệu POST và xử lý
        [HttpPost]
        public IActionResult Result(string ten, int namSinh)
        {
            int tuoi = DateTime.Now.Year - namSinh;
            ViewBag.Ten = ten;
            ViewBag.Tuoi = tuoi;
            return View();
        }
    }
}
