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
using CarType = PublicApi.DTO.v1.CarType;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CarTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public CarTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/CarTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarType>>> GetCarTypes()
        {
            var carTypes = (await _bll.CarTypes.AllAsync())
                .Select(bllEntity => new CarType()
                {
                    Id = bllEntity.Id,
                    Name= bllEntity.Name,
                }) ;
            
            return Ok(carTypes);
        }

        // GET: api/CarTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarType>> GetCarType(Guid id)
        {
            var carType = await _bll.CarTypes.FirstOrDefaultAsync(id, User.UserGuidId());

            if (carType == null)
            {
                return NotFound();
            }

            return Ok(carType);
        }

        // PUT: api/CarTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarType(Guid id, CarTypeEdit carTypeEditDTO)
        {
            if (id != carTypeEditDTO.Id)
            {
                return BadRequest();
            }

            var carType = await _bll.CarTypes.FirstOrDefaultAsync(carTypeEditDTO.Id, User.UserGuidId());
            if (carType == null)
            {
                return BadRequest();
            }

            carType.Name = carTypeEditDTO.Name;

            _bll.CarTypes.Update(carType);


            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.CarTypes.ExistsAsync(id, User.UserGuidId()))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/CarTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<CarType>> PostCarType(CarTypeCreate carTypeCreateDTO)
        {
            var carType = new BLL.App.DTO.CarType()
            {
                AppUserId = User.UserGuidId(),
                Name = carTypeCreateDTO.Name,
            };

            _bll.CarTypes.Add(carType);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetcarType", new {id = carType.Id}, carType);
        }

        // DELETE: api/CarTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CarType>> DeleteCarType(Guid id)
        {
            var carType = await _bll.CarTypes 
                .FirstOrDefaultAsync(id, User.UserGuidId());
            if (carType == null)
            {
                return NotFound();
            }

            _bll.CarTypes.Remove(carType);
            await _bll.SaveChangesAsync();

            return Ok(carType);

        }
    }
}
