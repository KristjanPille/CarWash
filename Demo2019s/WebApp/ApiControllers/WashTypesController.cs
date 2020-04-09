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
    public class WashTypesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WashTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/WashTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WashType>>> GetWashTypes()
        {
            return await _context.WashTypes.ToListAsync();
        }

        // GET: api/WashTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WashType>> GetWashType(int id)
        {
            var washType = await _context.WashTypes.FindAsync(id);

            if (washType == null)
            {
                return NotFound();
            }

            return washType;
        }

        // PUT: api/WashTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWashType(Guid id, WashType washType)
        {
            if (id != washType.Id)
            {
                return BadRequest();
            }

            _context.Entry(washType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WashTypeExists(id))
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

        // POST: api/WashTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<WashType>> PostWashType(WashType washType)
        {
            _context.WashTypes.Add(washType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWashType", new { id = washType.Id }, washType);
        }

        // DELETE: api/WashTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WashType>> DeleteWashType(int id)
        {
            var washType = await _context.WashTypes.FindAsync(id);
            if (washType == null)
            {
                return NotFound();
            }

            _context.WashTypes.Remove(washType);
            await _context.SaveChangesAsync();

            return washType;
        }

        private bool WashTypeExists(Guid id)
        {
            return _context.WashTypes.Any(e => e.Id == id);
        }
    }
}
