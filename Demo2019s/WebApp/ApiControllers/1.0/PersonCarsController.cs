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

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PersonCarsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PersonCarsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PersonTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonCar>>> GetPersonTypes()
        {
            return await _context.PersonCars.ToListAsync();
        }

        // GET: api/PersonCars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonCar>> GetPersonCar(int id)
        {
            var personType = await _context.PersonCars.FindAsync(id);

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
        public async Task<IActionResult> PutPersonCar(Guid id, PersonCar personCar)
        {
            if (id != personCar.Id)
            {
                return BadRequest();
            }

            _context.Entry(personCar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonCarExists(id))
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
        public async Task<ActionResult<PersonCar>> PostPersonCar(PersonCar personCar)
        {
            _context.PersonCars.Add(personCar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonCar", new { id = personCar.Id }, personCar);
        }

        // DELETE: api/PersonTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonCar>> DeletePersonCar(int id)
        {
            var personCar = await _context.PersonCars.FindAsync(id);
            if (personCar == null)
            {
                return NotFound();
            }

            _context.PersonCars.Remove(personCar);
            await _context.SaveChangesAsync();

            return personCar;
        }

        private bool PersonCarExists(Guid id)
        {
            return _context.PersonCars.Any(e => e.Id == id);
        }
    }
}
