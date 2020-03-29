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
    public class PersonTypesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PersonTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PersonTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonType>>> GetPersonTypes()
        {
            return await _context.PersonTypes.ToListAsync();
        }

        // GET: api/PersonTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonType>> GetPersonType(int id)
        {
            var personType = await _context.PersonTypes.FindAsync(id);

            if (personType == null)
            {
                return NotFound();
            }

            return personType;
        }

        // PUT: api/PersonTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonType(Guid id, PersonType personType)
        {
            if (id != personType.Id)
            {
                return BadRequest();
            }

            _context.Entry(personType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonTypeExists(id))
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

        // POST: api/PersonTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PersonType>> PostPersonType(PersonType personType)
        {
            _context.PersonTypes.Add(personType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonType", new { id = personType.Id }, personType);
        }

        // DELETE: api/PersonTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonType>> DeletePersonType(int id)
        {
            var personType = await _context.PersonTypes.FindAsync(id);
            if (personType == null)
            {
                return NotFound();
            }

            _context.PersonTypes.Remove(personType);
            await _context.SaveChangesAsync();

            return personType;
        }

        private bool PersonTypeExists(Guid id)
        {
            return _context.PersonTypes.Any(e => e.Id == id);
        }
    }
}
