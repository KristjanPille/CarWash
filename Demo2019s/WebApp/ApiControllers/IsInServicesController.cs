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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1.Mappers;
using V1DTO = PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// GPS locations received from gps handheld-s
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class IsInServicesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IsInServiceMapper _mapper = new IsInServiceMapper();
        
        /// <summary>
        /// Constructor
        /// </summary>
        public IsInServicesController(IAppBLL bll)
        {
            _bll = bll;
        }
        

        /// <summary>
        /// Get all IsInServices
        /// </summary>
        /// <returns>List of available GpsLocationTypes</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.IsInService>))]
        public async Task<ActionResult<IEnumerable<IsInService>>> GetIsInServices()
        {
            return Ok((await _bll.IsInServices.GetAllAsync()).Select(e => _mapper.Map(e)));
        }
        
        /// <summary>
        /// Get single IsInServices
        /// </summary>
        /// <param name="id">IsInService Id</param>
        /// <returns>request IsInService</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.IsInService))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<IsInService>> GetIsInService(Guid id)
        {
            var inService = await _bll.IsInServices.FirstOrDefaultAsync(id);

            if (inService == null)
            {
                return NotFound(new V1DTO.MessageDTO("isInService not found"));
            }

            return Ok(_mapper.Map(inService));
        }

        /// <summary>
        /// Update the IsInService
        /// </summary>
        /// <param name="id">IsInService id</param>
        /// <param name="isInService">IsInService object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutIsInService(Guid id, V1DTO.IsInService isInService)
        {
            if (id != isInService.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and gpsLocationType.id do not match"));
            }

            await _bll.IsInServices.UpdateAsync(_mapper.Map(isInService));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create a new GpsLocationType
        /// </summary>
        /// <param name="isInService">IsInService object</param>
        /// <returns>created IsInService object</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.IsInService))]
        public async Task<ActionResult<IsInService>> PostIsInService(V1DTO.IsInService isInService)
        {
            var bllEntity = _mapper.Map(isInService);
            _bll.IsInServices.Add(bllEntity);
            await _bll.SaveChangesAsync();
            isInService.Id = bllEntity.Id;

            return CreatedAtAction("GetIsInService",
                new {id = isInService.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                isInService);
        }

        /// <summary>
        /// Delete the IsInService
        /// </summary>
        /// <param name="id">IsInService Id</param>
        /// <returns>deleted IsInService object</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.IsInService))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.IsInService>> DeleteIsInService(Guid id)
        {
            var isInService = await _bll.IsInServices.FirstOrDefaultAsync(id);
            if (isInService == null)
            {
                return NotFound(new V1DTO.MessageDTO("GpsLocationType not found"));
            }

            await _bll.IsInServices.RemoveAsync(isInService);
            await _bll.SaveChangesAsync();

            return Ok(isInService);
        }
    }
}
