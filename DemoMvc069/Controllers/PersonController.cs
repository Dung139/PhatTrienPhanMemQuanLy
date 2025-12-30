using DemoMvc069.Models;
using Microsoft.AspNetCore.Mvc;
using DemoMvc069.Data;
using Microsoft.EntityFrameworkCore;
using DemoMvc069.Models.Process;    

namespace DemoMvc069.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly ExcelProcess _excelProcess = new ExcelProcess();

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var model = await _context.Person.ToListAsync();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,FullName,Address,Email")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }   

            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PersonId,FullName,Address,Email")] Person person)
        {
            if (id != person.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PersonId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Person == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Person'  is null.");
            }
            var person = await _context.Person.FindAsync(id);
            if (person != null)
            {
                _context.Person.Remove(person);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(string id)
        {
            return (_context.Person?.Any(e => e.PersonId == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension != ".xls" && fileExtension != ".xlsx" && fileExtension != ".csv")
                {
                    ModelState.AddModelError("", "Please choose a Excel file to upload!");
                }
                else
                {
                    var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Uploads/Excels", fileName);
                    var fileLocation = new FileInfo(filePath).ToString();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        // Đọc dữ liệu từ file Excel và lưu vào DataTable
                        var dt = _excelProcess.ExcelToDataTable(fileLocation);
                        // Duyệt qua từng dòng trong DataTable
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            // Tạo đối tượng Person mới
                            var ps = new Person();

                            // Gán giá trị cho các thuộc tính từ dữ liệu Excel
                            ps.PersonId = dt.Rows[i][0].ToString();   // Cột 0: Mã người
                            ps.FullName = dt.Rows[i][1].ToString();   // Cột 1: Họ tên
                            ps.Address = dt.Rows[i][2].ToString();    // Cột 2: Địa chỉ
                            ps.Email = dt.Rows[i][3].ToString();      // Cột 3: Email

                            // Thêm đối tượng vào context để chuẩn bị lưu vào cơ sở dữ liệu
                            _context.Add(ps);
                        }
                        // Lưu thay đổi vào cơ sở dữ liệu một cách bất đồng bộ
                        await _context.SaveChangesAsync();
                        // Chuyển hướng về trang Index sau khi hoàn tất
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View();    
        }
    }
}