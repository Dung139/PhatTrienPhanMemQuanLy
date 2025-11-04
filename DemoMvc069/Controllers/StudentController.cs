using Microsoft.AspNetCore.Mvc;

namespace DemoMvc069.Controllers
{
        public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string StudentID, string FullName, string Class)
        {
            string message = $"Mã sinh viên: {StudentID}, Họ tên: {FullName}, Lớp: {CLass}";
            ViewBag.Message = message;
            return View();
        }
    }
}

