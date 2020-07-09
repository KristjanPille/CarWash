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
    /// Services
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ServicesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ServiceMapper _mapper = new ServiceMapper();
        
        /// <summary>
        /// Constructor
        /// </summary>
        public ServicesController(IAppBLL bll)
        {
            _bll = bll;
        }
        /// <summary>
        /// get all the Services
        /// </summary>
        /// <returns>Array of Services</returns>
        [HttpGet]
        [Produces("application/json")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Service>))]
        public async Task<ActionResult<IEnumerable<V1DTO.Service>>> GetServices()
        {
            return Ok((await _bll.Services.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get a single Service
        /// </summary>
        /// <param name="serviceId">Service Id</param>
        /// <param name="priceOfService">price of service</param>
        /// <returns>Service object</returns>
        [HttpGet("{serviceId}/{priceOfService}")]
        [Produces("application/json")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Service))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<Service>> GetService(Guid serviceId, double priceOfService)
        {
            var service = await _bll.Services.FirstOrDefaultAsync(serviceId);

            if (service == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Service with id {serviceId} not found"));
            }

            //Applies discount if available
            return Ok(_mapper.Map(await _bll.Services.ApplyDiscount(service, priceOfService)));
        }
        
        /// <summary>
        /// Get a single Service
        /// </summary>
        /// <param name="serviceId">Service Id</param>
        /// <param name="priceOfService">price of service</param>
        /// <returns>Service object</returns>
        [HttpGet("{serviceId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Service))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<Service>> GetAdminService(Guid serviceId)
        {
            var service = await _bll.Services.FirstOrDefaultAsync(serviceId);

            if (service == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Service with id {serviceId} not found"));
            }

            return Ok(_mapper.Map(service));
        }
        
        /// <summary>
        /// Get prices of services according to car size
        /// </summary>
        /// <param name="car">car</param>
        /// <param name="serviceId">car</param>
        /// <returns>Services prices</returns>
        [Route("ServicePrice/{serviceId}")]
        [Produces("application/json")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Service))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<double>> GetServicesPrices(Guid serviceId, V1DTO.Car car)
        {
            var servicePrice = await _bll.Services.GetServicePrice(car, serviceId);
            
            return Ok(servicePrice);
        }
        
        /// <summary>
        /// Update the GpsSession
        /// </summary>
        /// <param name="id">Session Id</param>
        /// <param name="service">Service object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutService(Guid id, V1DTO.Service service)
        {
            if (id != service.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("Id and Service.id do not match"));
            }

            if (!await _bll.Services.ExistsAsync(service.Id, User.UserId()))
            {
                return NotFound(new V1DTO.MessageDTO($"Current user does not have Service with this id {id}"));
            }
            
            await _bll.Services.UpdateAsync(_mapper.Map(service));
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }
        
        /// <summary>
        /// Post the new Service
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Service))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<Service>> PostService(V1DTO.Service service)
        {
            var bllEntity = _mapper.Map(service);
            
            _bll.Services.Add(bllEntity);
            await _bll.SaveChangesAsync();
            service.Id = bllEntity.Id;

            return CreatedAtAction("GetService",
                new {id = service.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                service);
        }
        
        /// <summary>
        /// Delete the Service
        /// </summary>
        /// <param name="id">Session Id to delete.</param>
        /// <returns>GpSession just deleted</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Service))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<Service>> DeleteService(Guid id)
        {
            var userIdTKey = User.IsInRole("admin") ? null : (Guid?) User.UserId();

            var service =
                await _bll.Services.FirstOrDefaultAsync(id, userIdTKey);
            
            if (service == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Service with id {id} not found!"));
            }

            await _bll.Services.RemoveAsync(service, userIdTKey);
            await _bll.SaveChangesAsync();

            return StatusCode(204);
        }
    }
}
