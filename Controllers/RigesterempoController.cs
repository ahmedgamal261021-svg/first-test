using AspNetCoreGeneratedDocument;
using first_test.Data;
using first_test.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace first_test.Controllers
{
    public class RigesterempoController : Controller
    {
       

        public RigesterempoController(AppDbContext context, IWebHostEnvironment  environment)
        {
            _context = context;
            _environment = environment;
        }
        protected readonly AppDbContext _context;
        protected readonly IWebHostEnvironment _environment;
        [HttpPost]
        public async Task<IActionResult> Rigesterempo(Rigesterempo model, IFormFile? ProfilePhoto)
        {
            if (!ModelState.IsValid)
            {
                // Inspect all errors while debugging or log them
                var errors = ModelState
                      .Where(ms => ms.Value.Errors.Any())
                      .Select(ms => new
                      {
                          Field = ms.Key,
                          Errors = ms.Value.Errors.Select(e => e.ErrorMessage).ToList()
                      }).ToList();

                // Optionally send the list to the view for display
                ViewBag.ModelErrors = errors;
                return View(model);
            }
            bool chek= await _context.Rigesterempo.AnyAsync(u=> u.emp_email ==  model.emp_email);
            if (chek)
            {
                ModelState.AddModelError(nameof(model.emp_email), "This emp_email  is already registered.");
                return View(model);
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

            _context.Rigesterempo.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("signempo"); 


            
        }
        public IActionResult signempo()
        {
            return View();
        }
        public IActionResult Rigesterempo()
        {
            return View();
        }
        
        [HttpGet]
        public async Task <IActionResult> editempo()
        {
            int? camid = HttpContext.Session.GetInt32("CompanyId");
            if (!camid.HasValue) return View("signempo");
            var infCom = await _context.Rigesterempo.FirstOrDefaultAsync(u => u.Company_id == camid);
            if (infCom == null) return View("signempo");
            else
            {
                var mod = new editcompany
                {
                    id = infCom.Company_id,
                    fname = infCom.emp_fname,
                    lname = infCom.emp_lname,
                    email =infCom.emp_email,
                    lnaphoneme = infCom.emp_phone,
                    field_of_work = infCom.field_of_work,
                    job_title = infCom.job_title,
                    companyName = infCom.companyName,

                };

                return View(mod);
            }

                //return View();
        }
        [HttpPost]
        public async Task<IActionResult> editempo(editcompany model)
        {
            if (!ModelState.IsValid) return View("signempo");
 
            var infCom = await _context.Rigesterempo.FirstOrDefaultAsync(u => u.Company_id == model.id);
            
            infCom.emp_fname = model.fname;
            infCom.emp_lname = model.lname;

            infCom.emp_phone = model.lnaphoneme;
            infCom.field_of_work = model.field_of_work;
            infCom.job_title = model.job_title;
            infCom.companyName = model.companyName;
            await _context.SaveChangesAsync();
            return RedirectToAction("homecompany");
        }


        [HttpPost]
        public async Task<IActionResult> signempo(signempo model)
        {
            if(!ModelState.IsValid) return View(model);
            var mod = await _context.Rigesterempo.FirstOrDefaultAsync(u => u.emp_email == model.email);
            if (mod == null || mod.Password != model.Password)
            {
                ModelState.AddModelError(string.Empty, "Invalid Email or password.");
                return RedirectToAction();
            }

            var Countofjop = await _context.jobadd.Where(u => u.CompanyId == mod.Company_id).CountAsync();
            var CountofApply= await _context.userwithjob.Where(u =>u.job.CompanyId== mod.Company_id).CountAsync();
            var col = new InformationCompanys
            {
                rigesterempo = mod ,
                CountofApply = CountofApply,
                CountofJop = Countofjop
            };
            HttpContext.Session.SetString("ProfilePhoto", mod.ProfilePhoto);

            HttpContext.Session.SetInt32("CompanyId", mod.Company_id);
            HttpContext.Session.SetString("companyname", mod.companyName);
            HttpContext.Session.SetString("name", mod.emp_fname+mod.emp_lname);
            return RedirectToAction("homecompany" , col);
        }
        [HttpGet]
        public async Task<IActionResult> HomeCompany()
         {
            int? id = HttpContext.Session.GetInt32("CompanyId");
    var company = await _context.Rigesterempo.FindAsync(id);
    if (company == null) return NotFound();
            var Countofjop = await _context.jobadd.Where(u => u.CompanyId == company.Company_id).CountAsync();
            var CountofApply = await _context.userwithjob.Where(u => u.job.CompanyId == company.Company_id).CountAsync();
            var col = new InformationCompanys
            {
                rigesterempo = company,
                CountofApply = CountofApply,
                CountofJop = Countofjop
            };
            return View(col);   // <‑‑ pass a NON‑NULL model
         }
       
        public IActionResult Homecompany()
        {
            return View();
        }
        
        public async  Task<IActionResult> changeempopass(ChangePasswordCampony model)
        {
            int? id = HttpContext.Session.GetInt32("CompanyId"); 
            
            if (id == null) return View("signempo");

            var empo = await _context.Rigesterempo.FirstOrDefaultAsync(u=>u.Company_id == id);
            
            if (empo == null) return View("signempo");

            if (model.password == empo.Password)
            {
              if(model.password1 != model.password2)
                {
                    ModelState.AddModelError("password2", "Password2  Must be Match With New Password ");
                    return View(model); 
                }
                if (model.password1 == model.password)
                {
                    ModelState.AddModelError("password1", "Password1  Must be Different from Old Password ");
                    return View(model);
                }
                empo.Password = model.password2; 
                await _context.SaveChangesAsync();
                return RedirectToAction("homecompany");

            }
            else
            {
                ModelState.AddModelError("password", " Old Password is InCorrect");
                return View(model);
            }


                
        }
        public async Task<IActionResult> searchempo()
        {
            var users = await _context.Rigsteruser.ToListAsync();
            if (users == null) return NoContent();

            return View(users);
        }
        public async Task<IActionResult> searchBouquet()
        {
            var Bouquets = await _context.PricPlan.ToListAsync();
            if (Bouquets  == null) return NoContent();

            return View(Bouquets);
        }
        public async Task<IActionResult> Logout()
        {
            // مسح بيانات الـ Session
            HttpContext.Session.Clear();

            // لو عايز تمسح كوكيز كمان (اختياري)
            //await HttpContext.SignOutAsync();

            // رجوع لصفحة تسجيل الدخول
            return RedirectToAction("signempo", "Rigesterempo");
        }
    }

    
    

    }
