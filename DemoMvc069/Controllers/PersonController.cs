using Microsoft.AspNetCore.Mvc;

namespace DemoMvc069.Controllers
{
        public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string PersonId, string FullName, string Address)
        {
            string message = $"Mã nhân viên: {PersonId}, Họ tên: {FullName}, Địa chỉ: {Address}";
            ViewBag.Message = message;
            return View();
        }
    }
}

