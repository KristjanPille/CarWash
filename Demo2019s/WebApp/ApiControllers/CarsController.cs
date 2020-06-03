using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1.Mappers;
using V1DTO=PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{    /// <summary>
    /// Cars
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class CarsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly CarMapper _mapper = new CarMapper();
        
        /// <summary>
        /// Constructor
        /// </summary>
        public CarsController(IAppBLL bll)
        {
            _bll = bll;
        }
        /// <summary>
        /// get all the Cars
        /// </summary>
        /// <returns>Array of Cars</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Car>))]
        public async Task<ActionResult<IEnumerable<V1DTO.Car>>> GetCars()
        {
            return Ok((await _bll.Cars.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get a single Car
        /// </summary>
        /// <param name="id">Car Id</param>
        /// <returns>Car object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Car))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<Car>> GetCar(Guid id)
        {
            var car = await _bll.Cars.FirstOrDefaultAsync(id);

            if (car == null)
            {
                return NotFound(new V1DTO.MessageDTO($"car with id {id} not found"));
            }

            return Ok(_mapper.Map( car));
        }
        
        /// <summary>
        /// Update the GpsSession
        /// </summary>
        /// <param name="id">Session Id</param>
        /// <param name="car">car object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutCar(Guid id, V1DTO.Car car)
        {
            if (id != car.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("Id and car.id do not match"));
            }

            if (!await _bll.Cars.ExistsAsync(car.Id, User.UserId()))
            {
                return NotFound(new V1DTO.MessageDTO($"Current user does not have car with this id {id}"));
            }

            car.AppUserId = User.UserId();
            await _bll.Cars.UpdateAsync(_mapper.Map(car));
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }
        
        /// <summary>
        /// Post the new Car
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Car))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<Car>> PostCar(V1DTO.Car car)
        {
            car.AppUserId = User.UserId();
            var bllEntity = _mapper.Map(car);
            
            _bll.Cars.Add(bllEntity);
            await _bll.SaveChangesAsync();
            car.Id = bllEntity.Id;

            return CreatedAtAction("GetCar",
                new {id = car.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                car);
        }
        
        /// <summary>
        /// Delete the Car
        /// </summary>
        /// <param name="id">Session Id to delete.</param>
        /// <returns>GpSession just deleted</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Car))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<Car>> DeleteCar(Guid id)
        {
            var userIdTKey = User.IsInRole("admin") ? null : (Guid?) User.UserId();

            var car =
                await _bll.Cars.FirstOrDefaultAsync(id, userIdTKey);
            
            if (car == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Car with id {id} not found!"));
            }

            await _bll.Cars.RemoveAsync(car, userIdTKey);
            await _bll.SaveChangesAsync();

            return Ok(car);
        }
    }
}
