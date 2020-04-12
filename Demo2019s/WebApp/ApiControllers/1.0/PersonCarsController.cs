using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using DAL.App.EF;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PersonCarsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAppUnitOfWork _uow;

        public PersonCarsController(AppDbContext context, IAppUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        // GET: api/PersonCars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonCarDTO>>> GetPersonCars()
        {
            return Ok(await _uow.PersonCars.DTOAllAsync(User.UserGuidId()));
        }

        // GET: api/PersonCars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonCar>> GetPersonCar(Guid id)
        {
            var personCar = await _context.PersonCars.FindAsync(id);

            if (personCar == null)
            {
                return NotFound();
            }

            return personCar;
        }

        // PUT: api/PersonCars/5
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
                if (!personCarExists(id))
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

        // POST: api/PersonCars
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PersonCar>> PostPersonCar(PersonCar personCar)
        {
            _context.PersonCars.Add(personCar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetpersonCar", new { id = personCar.Id }, personCar);
        }

        // DELETE: api/PersonCars/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonCar>> DeletePersonCar(Guid id)
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

        private bool personCarExists(Guid id)
        {
            return _context.PersonCars.Any(e => e.Id == id);
        }
    }
}
