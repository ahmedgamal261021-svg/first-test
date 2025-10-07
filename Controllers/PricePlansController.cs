using first_test.Data;
using first_test.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace first_test.Controllers
{
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
