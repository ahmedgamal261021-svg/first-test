using first_test.Data;
using first_test.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.ComponentModel.Design;
using System.Threading.Tasks;

namespace first_test.Controllers
{
    public class jobaddController : Controller
    {
        public jobaddController(AppDbContext context) {

            _context = context;

        }
        protected readonly AppDbContext _context;

        [HttpGet]
        public IActionResult jobadd()          // <-- id comes from route
        {
            // you can verify the employer exists, but at minimum:
            int? id = HttpContext.Session.GetInt32("CompanyId");

            var job = _context.jobadd.Where(j => j.CompanyId == id).ToList();
            var categories = _context.CreateCategory
                               .Select(c => new SelectListItem
                               {
                                   Value = c.CategoryId.ToString(),
                                   Text = c.CategoryName
                               }).ToList();

            //  if (job == null) return NotFound();
            var vm = new JobAddViewModel
            {
                Jobs = job,
                NewJob = new Jop { CompanyId = (int)id },
                CategoryList = categories
            };
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> jobadd(JobAddViewModel model)
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
                // Repopulate the Jobs list before returning the view
                model.Jobs = _context.jobadd
                    .Where(j => j.CompanyId == model.NewJob.CompanyId)
                    .ToList();
                model.CategoryList = _context.CreateCategory
                                      .Select(c => new SelectListItem
                                      {
                                          Value = c.CategoryId.ToString(),
                                          Text = c.CategoryName
                                      }).ToList();
                Console.WriteLine("Selected CategoryId: " + model.NewJob.CategoryId);
                Console.WriteLine($"Selected CategoryId: {model.NewJob.CategoryId}");
                Console.WriteLine($"Category Object Null? {model.NewJob.Category == null}");
                return View(model);
            }

            _context.jobadd.Add(model.NewJob);
            await _context.SaveChangesAsync();

            return RedirectToAction("HomeCompany", "Rigesterempo",
                                    new { id = model.NewJob.CompanyId });
        }
        
        [HttpGet]
        public async Task<IActionResult> jopdetails(int id)
        {
            /*   int? companyId = HttpContext.Session.GetInt32("CompanyId");

               if (companyId == null)
               {
                   return RedirectToAction("sginempo", "Rigesterempo");
               }*/
            var jobs = await _context.jobadd.FirstOrDefaultAsync(j => j.job_id == id);

            if (jobs == null)
            {
                return NotFound("No jobs found for this company.");
            }
            return View(jobs);


        }
        public async Task<IActionResult> ShowApplicant(int id)
        {

            var applicant = await _context.userwithjob.Where(uwj => uwj.jobid == id).Include(uwj => uwj.User)
                .Select(uwj => uwj.User).ToListAsync();
            if (applicant == null) { return NoContent(); }

            var pp = new ShowApplicantsViewModel()
            {
                applicant = applicant
            };
            return View("ShowApplicant", pp);
        }

        public async Task<IActionResult> profilperson(int id)
        {
            var user = await _context.Rigsteruser.FindAsync(id);
            if (user == null) return NoContent();

            var viewModel = new interwithuser
            {
                user = user,
                inter = new InterviewInformation() // you can initialize a blank one for binding
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> interviewInformation(InterviewInformation inter)
        {
            if (!ModelState.IsValid)
            {
                foreach (var entry in ModelState)
                {
                    foreach (var rr in entry.Value.Errors)
                    {
                        Console.WriteLine($"Filll25lllllllled  '{entry.Key}' : {rr.ErrorMessage}");
                    }
                }
                return RedirectToAction("profilperson", new { id = inter.userid });
            }// or handle errors as needed

            _context.InterviewInformation.Add(inter);
            await _context.SaveChangesAsync();

            // Optionally redirect or return view
            return RedirectToAction("homecompany", "Rigesterempo"); // or show a success message
        }

       

        public IActionResult interviewInformation()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRequestStatus(int UserId, string Status)

        {
            if (UserId == null) return View("homecompany", "Rigesterempo");
            var interview = await _context.InterviewInformation
                                  .FirstOrDefaultAsync(i => i.userid == UserId);
            if (interview == null) return NoContent();
            interview.interview_result = Status;
            await _context.SaveChangesAsync();
            return RedirectToAction("companeacceptedemployess");


        }

        [HttpGet]
        public async Task<IActionResult> companeacceptedemployess()
        {
            int? id = HttpContext.Session.GetInt32("CompanyId");

            if (id == null) return NoContent();

            var jobs = await _context.jobadd.Where(i => i.CompanyId == id).Select(j => j.job_id).
                ToListAsync();

            if (jobs == null || !jobs.Any()) return NoContent();


            var userid = await _context.userwithjob.Where(u => jobs.Contains(u.jobid))
                .Select(u => u.Userid).Distinct().ToListAsync();
            if (!userid.Any()) return NoContent();


            var users = await _context.Rigsteruser.Where(u => userid.Contains(u.UserId)).ToListAsync();


            var states = await _context.InterviewInformation
           .Where(i => userid.Contains(i.userid))
           .Select(i => i.interview_result)
           .ToListAsync();
            foreach (var es in states)
            {
                Console.WriteLine("asdsfdsfdsfdsfsdfsdfdsfdsfdsfsdfsdfds" + es);
            }

            var coll = new List<userwithstate>();

            foreach (var user in users)
            {
                var interview = _context.InterviewInformation.FirstOrDefault(i => i.userid == user.UserId);

                coll.Add(new userwithstate
                {
                    rigesteruser = user,
                    States = interview
                });
                
            }
            return View(coll);
        }
        [ApiController]
        [Route("api/[controller]")]
        public class PricePlansController : ControllerBase
        {
            private readonly AppDbContext _context;

            public PricePlansController(AppDbContext context)
            {
                _context = context;
            }

            // GET: api/priceplans
            [HttpGet]
            public async Task<ActionResult<IEnumerable<PricPlan>>> GetAllPricePlans()
            {
                var plans = await _context.PricPlan.ToListAsync();
                return Ok(plans);
            }

            // GET: api/priceplans/5
            [HttpGet("{id}")]
            public async Task<ActionResult<PricPlan>> GetPricePlan(int id)
            {
                var plan = await _context.PricPlan.FindAsync(id);

                if (plan == null)
                    return NotFound();

                return Ok(plan);
            }

            // POST: api/priceplans
            [HttpPost]
            public async Task<ActionResult<PricPlan>> CreatePricePlan(PricPlan model)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _context.PricPlan.Add(model);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPricePlan), new { id = model.PriceId }, model);
            }

            // PUT: api/priceplans/5
            [HttpPut("{id}")]
            public async Task<IActionResult> UpdatePricePlan(int id, PricPlan model)
            {
                if (id != model.PriceId)
                    return BadRequest();

                _context.Entry(model).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.PricPlan.Any(e => e.PriceId == id))
                        return NotFound();
                    else
                        throw;
                }

                return NoContent();
            }

            // DELETE: api/priceplans/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeletePricePlan(int id)
            {
                var plan = await _context.PricPlan.FindAsync(id);
                if (plan == null)
                    return NotFound();

                _context.PricPlan.Remove(plan);
                await _context.SaveChangesAsync();

                return NoContent();
            }
        }
    }
    
}