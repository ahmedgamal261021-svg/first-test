using first_test.Data;
using first_test.Migrations;
using first_test.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace first_test.Controllers
{
    public class adminController : Controller
    { 
        protected readonly AppDbContext _context ;
        public adminController(AppDbContext context)
        {
            _context = context;

        }
        [HttpPost]
        public async Task<IActionResult> Rigesteradmin(Rigesteradmin model)
        {
            if(!ModelState.IsValid) return View(model);
            bool Phone = await _context.Rigesteradmin.AnyAsync(u => u.PhoneNum == model.PhoneNum);
            if (Phone)
            {
                ModelState.AddModelError(nameof(model.PhoneNum),
                    "This phone number is already registered.");
                return View(model);
            }
                _context.Rigesteradmin.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("homeadmin");
        }
        [HttpGet]
        public async Task<IActionResult> Rigesteradmin()
        {
         
            return View();
        }
        public async Task<IActionResult> homeadmin()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> adminpriceplan(PricePlanViewModel model)
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

                model.AllPlans = null; 
                return View(model);
            }
            _context.PricPlan.Add(model.NewPlan);
            await _context.SaveChangesAsync();  

            return RedirectToAction("homeadmin");
        }
        [HttpGet]
        public async Task<IActionResult> adminpriceplan()
        {
            var price = await _context.PricPlan.ToListAsync();
            if (price == null) { return NotFound(); }

            var mod = new PricePlanViewModel
            {
                AllPlans = price,
                NewPlan = null 
            };
            return View(mod);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePricePlan(int id)
        {
            var plan = await _context.PricPlan.FindAsync(id);
            if (plan == null) return NotFound();

            _context.PricPlan.Remove(plan);
            await _context.SaveChangesAsync();

            return RedirectToAction("adminpriceplan");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPricePlan(PricPlan model)
        {
            if (!ModelState.IsValid)
            {
                // رجع نفس الصفحة لو فيه أخطاء
                var vm = new PricePlanViewModel
                {
                    AllPlans = await _context.PricPlan.ToListAsync(),
                    NewPlan = new PricPlan()
                };
                return View("adminpriceplan", vm);
            }

            _context.PricPlan.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("adminpriceplan");
        }
        [HttpGet]
        public async Task<IActionResult> admincompan()
        {
            var com = await _context.Rigesterempo.ToListAsync();
            if (com == null) return NotFound();
             

            return View(com);
        }
        [HttpGet]
        public async Task<IActionResult> ContuctUsUser()
        {
            var com = await _context.ContactUs.ToListAsync();
            if (com == null) return NotFound();


            return View(com);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id )
        {
            var com = await _context.ContactUs.FindAsync(id);
            if (com == null) return NotFound();


            return View(com);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deleteadmincompan(int id)
        {
            var company = await _context.Rigesterempo.FindAsync(id);
            if (company == null) return NotFound();

            _context.Rigesterempo.Remove(company);
            await _context.SaveChangesAsync();

            return RedirectToAction("admincompan");
        }
        [HttpGet]
        public async Task<IActionResult> adminuser()
        {
            var use = await _context.Rigsteruser.ToListAsync();
            if (use == null) return NotFound();


            return View(use);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deleteadminuser(int id)
        {
            var user = await _context.Rigsteruser.FindAsync(id);
            if (user == null) return NotFound();

            _context.Rigsteruser.Remove(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("adminuser");
        }
        public async Task<IActionResult> CategoryCategory()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryCategory(Models.CreateCategory model)
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
            }

                _context.CreateCategory.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("homeadmin");
        }
    }
}
