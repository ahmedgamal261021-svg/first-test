using first_test.Data;
using first_test.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace first_test.Controllers
{
    public class RigesteruserController : Controller

    {
    // Injecting the DbContext
    public RigesteruserController(AppDbContext  context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public IActionResult Rigesteruser()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult>  index()
        {
            var jops = await _context.jobadd.ToListAsync();
            if (jops == null) return NoContent();
            var cat = await _context.CreateCategory.ToListAsync();
            if (cat == null) return NoContent();
            var Col = new rigewithcontact
            {
                CreateCategory = cat,
                jop = jops,
                ContactUs = null,
                rigesteruser= null

            };
            return View(Col);
        }
        [HttpPost]
        public async Task<IActionResult> index(rigewithcontact category)
        {
            if (!ModelState.IsValid) { return View(category); }

            var jops = await _context.jobadd.ToListAsync();
            if (jops == null) return NoContent();
            var cat = await _context.CreateCategory.ToListAsync();
            if (cat == null) return NoContent();
            var Col = new rigewithcontact
            {
               CreateCategory = cat,
                jop = jops,
                ContactUs = category.ContactUs,
                rigesteruser = null

            };
            _context.ContactUs.Add(category.ContactUs);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "تم إرسال الرسالة بنجاح!";
            return View(Col);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public async Task<IActionResult> Rigesteruser(Rigesteruser model, IFormFile? ProfilePhoto)
        {
            if (!ModelState.IsValid)
            {
                // استخراج الأخطاء
                var errors = ModelState
                    .Where(ms => ms.Value.Errors.Any())
                    .Select(ms => new
                    {
                        Field = ms.Key,
                        Errors = ms.Value.Errors.Select(e => e.ErrorMessage).ToList()
                    }).ToList();

                // مؤقت: عرضهم في الـ ViewBag أو اللوج
                ViewBag.ModelErrors = errors;

                return View(model);
            }
            // show errors
            bool phoneExists = await _context.Rigsteruser
            .AnyAsync(u => u.PhoneNum == model.PhoneNum);
            if (phoneExists)
            {
                ModelState.AddModelError(nameof(model.PhoneNum),
                    "This phone number is already registered.");
                return View(model);        // redisplay form with the error
            }
            if (ProfilePhoto != null && ProfilePhoto.Length > 0)
            {
                // تأكد فولدر حفظ الصور موجود
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                // اسم فريد للصورة
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfilePhoto.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ProfilePhoto.CopyToAsync(fileStream);
                }

                // حفظ اسم الملف في موديل المستخدم
                model.ProfilePhoto = uniqueFileName;
            }
          
            _context.Rigsteruser.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("signuser", "signuser");
        }
       
        public async Task<IActionResult> SearchjopBefore()
        {
            var jops = await _context.jobadd.ToListAsync(); 
            if (jops== null)  return NoContent();
          
            return View(jops);
        } 
    }
}
