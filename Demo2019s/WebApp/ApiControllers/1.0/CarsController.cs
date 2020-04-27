using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
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
using Car = PublicApi.DTO.v1.Car;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CarsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public CarsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            var cars = (await _bll.Cars.AllAsync())
                .Select(bllEntity => new Car()
                {
                    Id = bllEntity.Id,
                    LicenceNr = bllEntity.LiceneNr,
                }) ;
            
            return Ok(cars);
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(Guid id)
        {
            var car = await _bll.Cars.FirstOrDefaultAsync(id, User.UserGuidId());

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);

        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(Guid id, CarEdit carEditDTO)
        {
            if (id != carEditDTO.Id)
            {
                return BadRequest();
            }

            var car = await _bll.Cars.FirstOrDefaultAsync(carEditDTO.Id, User.UserGuidId());
            if (car == null)
            {
                return BadRequest();
            }

            car.LiceneNr = carEditDTO.LicenceNr;

            _bll.Cars.Update(car);


            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Cars.ExistsAsync(id, User.UserGuidId()))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Cars
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(CarCreate carCreateDTO)
        {
            var car = new BLL.App.DTO.Car()
            {
                AppUserId = User.UserGuidId(),
                LiceneNr = carCreateDTO.LicenceNr,
            };

            _bll.Cars.Add(car);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCar", new {id = car.Id}, car);

        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Car>> DeleteCar(Guid id)
        {
            var car = await _bll.Cars.FirstOrDefaultAsync(id, User.UserGuidId());
            if (car == null)
            {
                return NotFound();
            }

            _bll.Cars.Remove(car);
            await _bll.SaveChangesAsync();

            return Ok(car);

        }
    }
}
