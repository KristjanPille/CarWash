using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CarsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public CarsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDTO>>> GetCars()
        {
            return Ok(await _uow.Cars.DTOAllAsync(User.UserGuidId()));
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarDTO>> GetCar(Guid id)
        {
            var carDTO = await _uow.Cars.DTOFirstOrDefaultAsync(id, User.UserGuidId());

            if (carDTO == null)
            {
                return NotFound();
            }

            return Ok(carDTO);

        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(Guid id, CarEditDTO carEditDTO)
        {
            if (id != carEditDTO.Id)
            {
                return BadRequest();
            }

            var car = await _uow.Cars.FirstOrDefaultAsync(carEditDTO.Id, User.UserGuidId());
            if (car == null)
            {
                return BadRequest();
            }
            car.LicenceNr = carEditDTO.LicenceNr;

            _uow.Cars.Update(car);
            
            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Cars.ExistsAsync(id, User.UserGuidId()))
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

        // POST: api/Cars
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(CarCreateDTO carCreateDTO)
        {
            var car = new Car();
            car.LicenceNr = carCreateDTO.LicenceNr;

            _uow.Cars.Add(car);
           
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Car>> DeleteCar(Guid id)
        {
            var animal = await _uow.Cars.FirstOrDefaultAsync(id, User.UserGuidId());
            if (animal == null)
            {
                return NotFound();
            }

            _uow.Cars.Remove(animal);
            await _uow.SaveChangesAsync();
            return Ok(animal);

        }
    }
}
