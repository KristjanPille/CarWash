using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IsInWashsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IsInWashsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/IsInWashs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IsInWash>>> GetIsInWashes()
        {
            return await _context.IsInWashes.ToListAsync();
        }

        // GET: api/IsInWashs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IsInWash>> GetIsInWash(int id)
        {
            var isInWash = await _context.IsInWashes.FindAsync(id);

            if (isInWash == null)
            {
                return NotFound();
            }

            return isInWash;
        }

        // PUT: api/IsInWashs/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIsInWash(int id, IsInWash isInWash)
        {
            if (id != isInWash.Id)
            {
                return BadRequest();
            }

            _context.Entry(isInWash).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsInWashExists(id))
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

        // POST: api/IsInWashs
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<IsInWash>> PostIsInWash(IsInWash isInWash)
        {
            _context.IsInWashes.Add(isInWash);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIsInWash", new { id = isInWash.Id }, isInWash);
        }

        // DELETE: api/IsInWashs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IsInWash>> DeleteIsInWash(int id)
        {
            var isInWash = await _context.IsInWashes.FindAsync(id);
            if (isInWash == null)
            {
                return NotFound();
            }

            _context.IsInWashes.Remove(isInWash);
            await _context.SaveChangesAsync();

            return isInWash;
        }

        private bool IsInWashExists(int id)
        {
            return _context.IsInWashes.Any(e => e.Id == id);
        }
    }
}
