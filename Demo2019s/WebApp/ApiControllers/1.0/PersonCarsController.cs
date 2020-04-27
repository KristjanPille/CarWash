using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using DAL.App.EF;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using PersonCar = PublicApi.DTO.v1.PersonCar;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PersonCarsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public PersonCarsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/PersonCars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonCar>>> GetPersonCars()
        {
            var personCar = (await _bll.PersonCars.AllAsync())
                .Select(bllEntity => new PersonCar()
                {
                    Id = bllEntity.Id,
                }) ;
            
            return Ok(personCar);
        }

        // GET: api/PersonCars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonCar>> GetPersonCar(Guid id)
        {
            var personCar = await _bll.PersonCars.FirstOrDefaultAsync(id, User.UserGuidId());

            if (personCar == null)
            {
                return NotFound();
            }

            return Ok(personCar);
        }

        // PUT: api/PersonCars/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonCar(Guid id, PersonCar personCarEditDTO)
        {
            if (id != personCarEditDTO.Id)
            {
                return BadRequest();
            }

            var personCar = await _bll.PersonCars.FirstOrDefaultAsync(personCarEditDTO.Id, User.UserGuidId());
            if (personCar == null)
            {
                return BadRequest();
            }

            _bll.PersonCars.Update(personCar);


            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.PersonCars.ExistsAsync(id, User.UserGuidId()))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/PersonCars
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PersonCar>> PostPersonCar(PersonCarCreate personCarCreate)
        {
            var personCar = new BLL.App.DTO.Payment()
            {
                AppUserId = User.UserGuidId(),
            };

            _bll.Payments.Add(personCar);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPersonCar", new {id = personCar.Id}, personCar);
        }

        // DELETE: api/PersonCars/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonCar>> DeletePersonCar(Guid id)
        {
            var personCar = await _bll.Payments 
                .FirstOrDefaultAsync(id, User.UserGuidId());
            if (personCar == null)
            {
                return NotFound();
            }

            _bll.Payments.Remove(personCar);
            await _bll.SaveChangesAsync();

            return Ok(personCar);
        }
    }
}
