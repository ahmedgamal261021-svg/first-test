using first_test.Data;
using first_test.Migrations;
using first_test.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace first_test.Controllers
{
    public class signuserController : Controller
    {
        public signuserController(AppDbContext context, IWebHostEnvironment environment)
        {

            _context = context;
            _environment = environment;

        }
        protected readonly AppDbContext _context;
        protected readonly IWebHostEnvironment _environment;


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> signuser(signuser signuser)
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            if (!ModelState.IsValid) return View(signuser);

            var user = await _context.Rigsteruser
                 .SingleOrDefaultAsync(u => u.PhoneNum == signuser.PhoneNum);

            if (user == null || user.Password != signuser.Password)
            {
                ModelState.AddModelError(string.Empty, "Invalid phone number or password.");
                return View("signuser");
            }


            HttpContext.Session.SetInt32("user_id", user.UserId);

            // var logo = string.IsNullOrEmpty(user.ProfilePhoto) ? "/img/apple-touch-icon.png" : user.ProfilePhoto;
            HttpContext.Session.SetString("ProfilePhoto", user.ProfilePhoto ?? "");
            //HttpContext.Session.SetString("user_logo", logo);

            HttpContext.Session.SetString("user_Name", user.Name);
            //Console.WriteLine("From Controller user_Name: " + HttpContext.Session.GetString("user_Name"));



            return RedirectToAction("homeuser", new { id = user.UserId });

        }
        public IActionResult signuser()
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> homeuser(int id)
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";
            int? user_id = HttpContext.Session.GetInt32("user_id");

            if (user_id == null)
            {
                // session expired or wrong user
                return RedirectToAction("signuser");
            }
            var user = _context.Rigsteruser.Find(user_id);
            HttpContext.Session.SetString("user_Name", user.Name);
            var categ = await _context.CreateCategory.ToListAsync();
            //ViewBag.UserName = HttpContext.Session.GetString("user_Name");
           
            var model = new rigewithcontact
            {
                rigesteruser = user,
                ContactUs = null,
                CreateCategory = categ
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> homeuser(rigewithcontact model)
        {


            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(ms => ms.Value.Errors.Any())
                    .Select(ms => new
                    {
                        Field = ms.Key,
                        Errors = ms.Value.Errors.Select(e => e.ErrorMessage).ToList()
                    }).ToList();

                ViewBag.ModelErrors = errors;

                int? user_id2 = HttpContext.Session.GetInt32("user_id");
                var user = _context.Rigsteruser.Find(user_id2);
                var categ = await _context.CreateCategory.ToListAsync();
                var jops = await _context.jobadd.ToListAsync();
                if (jops == null) return NoContent();
                model.CreateCategory = categ;
                model.rigesteruser = user;
                return View(model);
            }
            int? user_id = HttpContext.Session.GetInt32("user_id");
            model.ContactUs.UserId = (int)user_id;
            model.ContactUs.ContuctUs_date = DateTime.Now;
            var user1 = _context.Rigsteruser.Find(user_id);
            model.rigesteruser = user1;

            _context.ContactUs.Add(model
                .ContactUs);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "تم إرسال الرسالة بنجاح!";
            return View(model);

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("signuser", "signuser");
        }
        public async Task<IActionResult> searchjop()
        {
            int? user_id = HttpContext.Session.GetInt32("user_id");
            if (user_id == null)
            {
                // session expired or wrong user
                return RedirectToAction("signuser");
            }

            var jop = await _context.jobadd.ToListAsync();
            ViewBag.UserId = user_id;
            if (ViewBag.UserId == null)
            {
                // session expired or wrong user
                return RedirectToAction("signuser");
            }
            return View(jop);
        }
        //public IActionResult Applyjop(int jop)
        //{
        //   var  user_id = HttpContext.Session.GetInt32("user_id");
        //    if (user_id == null)
        //    {
        //        return RedirectToAction("logout"); // or your login action
        //    }

        //    var user = _context.Rigsteruser.FirstOrDefault(u => u.UserId == user_id);
        //    var job = _context.jobadd.Find(jop);
        //    //if (job != null) {
        //    //    var com = _context.Rigesterempo.FirstOrDefault(j => j.Company_id == job.CompanyId); 

        //    //}
        //    var com = _context.Rigesterempo.FirstOrDefault(j => j.Company_id == job.CompanyId);
        //    var vm = new JobWithCompanyViewModel
        //    {
        //        User = user,
        //        Job = job,
        //        Company = com
        //    };

        //    bool CanApply;

        //    ViewBag.CanApply = (user.Age >= job.age &&
        //               user.EducationLevel == job.educational_level &&
        //               user.ExperienceLevel == job.years_of_experience &&
        //               user.Gender != job.Gender);

        //    return View("Applyjop", vm);
        //}

        //public IActionResult Applyjop(int? user_id, userwithjob model)
        //{
        //    if (TempData["job"] is string jobJson)
        //    {
        //        var job = JsonConvert.DeserializeObject<Jop>(jobJson);
        //        var com = _context.Rigesterempo.FirstOrDefault(j => j.Company_id == job.CompanyId);
        //        //int? user_id = HttpContext.Session.GetInt32("user_id");

        //        if (user_id == null)
        //        {
        //            user_id = HttpContext.Session.GetInt32("user_id");
        //        }
        //        if (user_id == null)
        //        {
        //            return RedirectToAction("logout"); // or your login action
        //        }
        //        var user = _context.Rigsteruser.FirstOrDefault(u => u.UserId == user_id);
        //        var vm = new JobWithCompanyViewModel
        //        {
        //            User = user,
        //            Job = job,
        //            Company = com
        //        };
        //        bool CanApply;

        //        ViewBag.CanApply = (user.Age >= job.age &&
        //                   user.EducationLevel == job.educational_level &&
        //                   user.ExperienceLevel == job.years_of_experience &&
        //                   user.Gender != job.Gender);

        //        return View("Applyjop", vm); // ✅ PASS job model to the view
        //    }

        //    return View("homeuser");
        //}


        //[HttpPost]
        //public IActionResult StoreJobData([FromBody] Jop job)
        //{
        //    TempData["job"] = JsonConvert.SerializeObject(job);
        //    return Ok();
        //}
        public IActionResult Applyjop(int jobId)
        {
            // Get user_id from session
            int? user_id = HttpContext.Session.GetInt32("user_id");
            if (user_id == null)
            {
                return RedirectToAction("logout");
            }

            // Get Job from DB using JobId
            var job = _context.jobadd.FirstOrDefault(j => j.job_id == jobId);

            if (job == null)
            {
                return RedirectToAction("homeuser");
            }

            // Get company of the job
            var com = _context.Rigesterempo.FirstOrDefault(c => c.Company_id == job.CompanyId);

            // Get user data
            var user = _context.Rigsteruser.FirstOrDefault(u => u.UserId == user_id);

            // Build ViewModel
            var vm = new JobWithCompanyViewModel
            {
                User = user,
                Job = job,
                Company = com
            };

            // Check if user can apply
            ViewBag.CanApply = (user.Age >= job.age &&
                                user.EducationLevel == job.educational_level &&
                                user.ExperienceLevel == job.years_of_experience &&
                                user.Gender != job.Gender);

            return View("Applyjop", vm);
        }

        [HttpPost]
        public async Task<IActionResult> ApplyjopWithuser(userwithjob model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var entry in ModelState)
                {
                    foreach (var error in entry.Value.Errors)
                    {
                        Console.WriteLine($"❌ Fiiiiiiiiiiiiiiiiiiiield '{entry.Key}': {error.ErrorMessage}");
                        Console.WriteLine($"jobid: {model.jobid}");
                        Console.WriteLine($"Userid: {model.Userid}");
                        Console.WriteLine($"company: {model.company}");
                        Console.WriteLine($"job_name: {model.job_name}");
                    }
                }
                var job = _context.jobadd.FirstOrDefault(j => j.job_id == model.jobid);
                var user = _context.Rigsteruser.FirstOrDefault(u => u.UserId == model.Userid);
                var company = _context.Rigesterempo.FirstOrDefault(c => c.Company_id == job.CompanyId);

                var vm = new JobWithCompanyViewModel
                {
                    Job = job,
                    User = user,
                    Company = company,
                    usjo = model
                };

                ViewBag.CanApply = (user.Age >= job.age &&
                                    user.EducationLevel == job.educational_level &&
                                    user.ExperienceLevel == job.years_of_experience &&
                                    user.Gender != job.Gender);

                return View("Applyjop", vm); // ✅ correct model type
            }



            model.request_state = "Panding";
            model.CreatedDate = DateTime.Now;
            _context.userwithjob.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("searchjop");
        }
        [HttpGet("signuser/appliinterview")]
        public async Task<IActionResult> appliinterview()
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";
            int? id = HttpContext.Session.GetInt32("user_id");
            if (id == null)
            {
                return RedirectToAction("logout"); // or your login action
            }
            var user = await _context.Rigsteruser.FindAsync(id);
            if (user == null) return NotFound();
            var inter = await _context.InterviewInformation
     .Where(iu => iu.userid == id)
     .ToListAsync();
            if (inter == null) return NoContent();

            return View(inter);
        }

        public async Task<IActionResult> applijopuser()
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";
            int? id = HttpContext.Session.GetInt32("user_id");
            if (id == null)
            {
                return RedirectToAction("logout"); // or your login action
            }
            var app = await _context.userwithjob.Where(u => u.Userid == id).ToListAsync();

            if (app == null) return NotFound();
            var result = new List<UserJobApplicationViewModel>();
            foreach (var ap in app)
            {
                var job = await _context.jobadd.FirstOrDefaultAsync(j => j.job_id == ap.jobid);
                Console.WriteLine($"❌ Fiiiiiiiiiiiiiiiiiiiield '{ap.Userid}' ");
                if (job != null)
                {
                    result.Add(new UserJobApplicationViewModel
                    {
                        Application = ap,
                        Job = job
                    });
                }
            }

            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> edituser()
        {

            int? id = HttpContext.Session.GetInt32("user_id");
            if (id == null)
            {
                return RedirectToAction("logout"); // or your login action
            }

            var user = await _context.Rigsteruser.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            var model = new edituser
            {
                id = user.UserId,
                fname = user.Name,
                lname = user.Name,
                lnaphoneme = user.PhoneNum,
                age = (int)user.Age,
                countrySelect = user.Country,
                governante = user.Government,
                education_level = user.EducationLevel,
                experience_level = user.ExperienceLevel,

            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> edituser(edituser model, IFormFile? ProfilePhoto)
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _context.Rigsteruser.FirstOrDefaultAsync(u => u.UserId == model.id);
            if (user == null)
            {
                return NotFound();
            }

            // Update fields
            user.Name = model.fname + model.lname;

            user.PhoneNum = model.lnaphoneme;
            user.Age = model.age;

            user.Country = model.countrySelect;
            user.Government = model.governante;
            user.EducationLevel = model.education_level;
            user.ExperienceLevel = model.experience_level;
            if (ProfilePhoto != null && ProfilePhoto.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfilePhoto.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ProfilePhoto.CopyToAsync(fileStream);
                }

                // حذف الصورة القديمة إذا موجودة
                if (!string.IsNullOrEmpty(user.ProfilePhoto))
                {
                    var oldFile = Path.Combine(uploadsFolder, user.ProfilePhoto);
                    if (System.IO.File.Exists(oldFile))
                    {
                        System.IO.File.Delete(oldFile);
                    }
                }

                user.ProfilePhoto = uniqueFileName;
            }

            await _context.SaveChangesAsync();


            var logo = string.IsNullOrEmpty(user.ProfilePhoto) ? "/img/apple-touch-icon.png" : user.ProfilePhoto;
            HttpContext.Session.SetString("user_logo", logo);

            TempData["Success"] = "Profile updated successfully!";
            return RedirectToAction("edituser"); // or wherever you want to redirect
        }
        public async Task<IActionResult> userFavoritejobs()
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";
            int? user_id = HttpContext.Session.GetInt32("user_id");
            if (user_id == null)
            {
                return RedirectToAction("signuser");
            }
            return View();
        }
        public async Task<IActionResult> changeuserpass(ChangePassword model)
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";
            int? userid = HttpContext.Session.GetInt32("user_id");
            if (userid == null) return View("signuser");

            var user = await _context.Rigsteruser.FirstOrDefaultAsync(u => u.UserId == userid);

            if (user == null) return View("signuser");


            if (model.password == user.Password)
            {
                if (model.password1 != model.password2)
                {
                    ModelState.AddModelError("password2", "New passwords do not match.");
                    return View(model);
                }

                if (model.password1 == model.password)
                {
                    ModelState.AddModelError("password1", "New password must be different from old password.");
                    return View(model);
                }
                user.Password = model.password2;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Password changed successfully!";

            }
            else
            {
                ModelState.AddModelError("password", "Old password is incorrect.");
                return View(model);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> viewjop(int id)
        {
            //        var category = await _context.CreateCategory
            //.Include(c => c.Jop)   // نجيب الوظائف المرتبطة
            //.FirstOrDefaultAsync(c => c.CategoryId == id);
            var jobs = await _context.jobadd
             .Where(j => j.CategoryId == id)
             .Include(j => j.Company) // لو عندك علاقة مع الشركة
             .ToListAsync();
            //if (category == null)
            //{
            //    return NotFound();
            //}
            //var jobs = category.Jop.ToList();
            return View(jobs);
        }
        public async Task<IActionResult> viewjop()
        {
            return View();
        }
    }
}
