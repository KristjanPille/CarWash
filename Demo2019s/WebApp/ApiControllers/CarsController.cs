using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1.Mappers;
using ModelMark = BLL.App.DTO.ModelMark;
using V1DTO = PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Cars
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class CarsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly CarsMapper _mapper = new CarsMapper();

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
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Car>))]
        public async Task<ActionResult<IEnumerable<V1DTO.Car>>> GetCars()
        {
            //Gets User specific cars
            return Ok((await _bll.Cars.GetAllAsync()).Select(e => _mapper.Map(e)).Where(e => e.AppUserId == User.UserId()));
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
        public async Task<ActionResult<V1DTO.Car>> GetCar(Guid id)
        { 
            BLL.App.DTO.Car car = await _bll.Cars.FirstOrDefaultAsync(id);

            if (!await _bll.Cars.ExistsAsync(car.Id, User.UserId()))
            {
                return NotFound(new V1DTO.MessageDTO($"Current user does not have car with this id {id}, userId:{User.UserId()}"));
            }

            return Ok((await _bll.Cars.GetAllAsync()).Select(e => _mapper.Map(e)).Where(e => e.Id == id).Last());
        }

        /// <summary>
        /// Update Car
        /// </summary>
        /// <param name="id">car Id</param>
        /// <param name="car">car object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutCar(Guid id, V1DTO.Car car)
        {
            car.AppUserId = User.UserId();

            if (id != car.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and car.id do not match"));
            }
            
            var modelMarkId = await _bll.ModelMarks.GetModelMarkId(car);
            var modelMark = await _bll.ModelMarks.FirstOrDefaultAsync(modelMarkId);

            var bllEntity = _mapper.Map(car);
            bllEntity.ModelMarkId = modelMark.Id;
            
            await _bll.Cars.UpdateAsync(bllEntity);
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

            //modelmark cant be created has to found in db.
            var modelMarkId = await _bll.ModelMarks.GetModelMarkId(car);
            var modelMark = await _bll.ModelMarks.FirstOrDefaultAsync(modelMarkId);

            bllEntity.ModelMarkId = modelMark.Id;

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
            var car =
                await _bll.Cars.FirstOrDefaultAsync(id);

            if (!await _bll.Cars.ExistsAsync(car.Id, User.UserId()))
            {
                return NotFound(new V1DTO.MessageDTO($"Current user does not have car with this id {id}, userId:{User.UserId()}"));
            }
            
            if (car == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Car with id {id} not found!"));
            }

            await _bll.Cars.RemoveAsync(car);
            await _bll.SaveChangesAsync();

            return Ok(car);
        }
    }
}