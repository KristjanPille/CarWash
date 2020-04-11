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
    public class CarTypesController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public CarTypesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/CarTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarTypeDTO>>> GetCarTypes()
        {
            return Ok(await _uow.CarTypes.DTOAllAsync(User.UserGuidId()));
        }

        // GET: api/CarTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarTypeDTO>> GetCarType(Guid id)
        {
            var carDTO = await _uow.CarTypes.DTOFirstOrDefaultAsync(id, User.UserGuidId());

            if (carDTO == null)
            {
                return NotFound();
            }

            return Ok(carDTO);

        }

        // PUT: api/CarTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarType(Guid id, CarTypeEditDTO carTypeEditDTO)
        {
            if (id != carTypeEditDTO.Id)
            {
                return BadRequest();
            }

            var carType = await _uow.CarTypes.FirstOrDefaultAsync(carTypeEditDTO.Id, User.UserGuidId());
            if (carType == null)
            {
                return BadRequest();
            }
            carType.Name = carTypeEditDTO.Name;

            _uow.CarTypes.Update(carType);
            
            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.CarTypes.ExistsAsync(id, User.UserGuidId()))
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

        // POST: api/CarTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<CarType>> PostCarType(CarTypeCreateDTO carTypeCreateDTO)
        {
            var car = new CarType();
            car.Name = carTypeCreateDTO.Name;
            
            _uow.CarTypes.Add(car);
           
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetCarType", new { id = car.Id }, car);

        }

        // DELETE: api/CarTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CarType>> DeleteCarType(Guid id)
        {
            var car = await _uow.CarTypes.FirstOrDefaultAsync(id, User.UserGuidId());
            if (car == null)
            {
                return NotFound();
            }

            _uow.CarTypes.Remove(car);
            await _uow.SaveChangesAsync();
            return Ok(car);

        }
    }
}
