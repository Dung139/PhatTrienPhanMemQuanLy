using Microsoft.AspNetCore.Mvc;
using System;

namespace baithuchanhso5.Controllers
{
    public class GiaiPTB2Controller : Controller
    {
        // View nhập hệ số a, b, c
        public IActionResult Index()
        {
            return View();
        }

        // Nhận và xử lý kết quả
        [HttpPost]
        public IActionResult Index(double a, double b, double c)
        {
            string ketqua = "";
            if (a == 0)
            {
                if (b == 0)
                    ketqua = (c == 0) ? "Phương trình vô số nghiệm" : "Phương trình vô nghiệm";
                else
                    ketqua = $"Phương trình có nghiệm x = {-c / b}";
            }
            else
            {
                double delta = b * b - 4 * a * c;
                if (delta < 0)
                    ketqua = "Phương trình vô nghiệm";
                else if (delta == 0)
                    ketqua = $"Phương trình có nghiệm kép x1 = x2 = {-b / (2 * a)}";
                else
                {
                    double x1 = (-b + Math.Sqrt(delta)) / (2 * a);
                    double x2 = (-b - Math.Sqrt(delta)) / (2 * a);
                    ketqua = $"Phương trình có 2 nghiệm: x1 = {x1}, x2 = {x2}";
                }
            }
            ViewBag.KetQua = ketqua;
            return View();
        }
    }
}
