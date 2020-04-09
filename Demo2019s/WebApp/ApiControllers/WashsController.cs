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
    public class WashsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WashsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Washs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wash>>> GetWashes()
        {
            return await _context.Washes.ToListAsync();
        }

        // GET: api/Washs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Wash>> GetWash(int id)
        {
            var wash = await _context.Washes.FindAsync(id);

            if (wash == null)
            {
                return NotFound();
            }

            return wash;
        }

        // PUT: api/Washs/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWash(Guid id, Wash wash)
        {
            if (id != wash.Id)
            {
                return BadRequest();
            }

            _context.Entry(wash).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WashExists(id))
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

        // POST: api/Washs
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Wash>> PostWash(Wash wash)
        {
            _context.Washes.Add(wash);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWash", new { id = wash.Id }, wash);
        }

        // DELETE: api/Washs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Wash>> DeleteWash(int id)
        {
            var wash = await _context.Washes.FindAsync(id);
            if (wash == null)
            {
                return NotFound();
            }

            _context.Washes.Remove(wash);
            await _context.SaveChangesAsync();

            return wash;
        }

        private bool WashExists(Guid id)
        {
            return _context.Washes.Any(e => e.Id == id);
        }
    }
}
