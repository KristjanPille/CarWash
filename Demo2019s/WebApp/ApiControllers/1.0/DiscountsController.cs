using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DiscountsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DiscountsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Discounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Discount>>> GetDiscounts()
        {
            return await _context.Discounts.ToListAsync();
        }

        // GET: api/Discounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Discount>> GetDiscount(int id)
        {
            var discount = await _context.Discounts.FindAsync(id);

            if (discount == null)
            {
                return NotFound();
            }

            return discount;
        }

        // PUT: api/Discounts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiscount(Guid id, Discount discount)
        {
            if (id != discount.Id)
            {
                return BadRequest();
            }

            _context.Entry(discount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Discounts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Discount>> PostDiscount(Discount discount)
        {
            _context.Discounts.Add(discount);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDiscount", new { id = discount.Id }, discount);
        }

        // DELETE: api/Discounts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Discount>> DeleteDiscount(int id)
        {
            var discount = await _context.Discounts.FindAsync(id);
            if (discount == null)
            {
                return NotFound();
            }

            _context.Discounts.Remove(discount);
            await _context.SaveChangesAsync();

            return discount;
        }

        private bool DiscountExists(Guid id)
        {
            return _context.Discounts.Any(e => e.Id == id);
        }
    }
}
