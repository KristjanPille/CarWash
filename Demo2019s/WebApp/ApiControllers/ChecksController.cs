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
    /// Checks
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ChecksController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly CheckMapper _mapper = new CheckMapper();
        
        /// <summary>
        /// Constructor
        /// </summary>
        public ChecksController(IAppBLL bll)
        {
            _bll = bll;
        }
        /// <summary>
        /// get all the Checks
        /// </summary>
        /// <returns>Array of Checks</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Check>))]
        public async Task<ActionResult<IEnumerable<V1DTO.Check>>> GetChecks()
        {
            return Ok((await _bll.Checks.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get a single check
        /// </summary>
        /// <param name="id">check Id</param>
        /// <returns>check object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Check))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<Check>> Getcheck(Guid id)
        {
            var check = await _bll.Checks.FirstOrDefaultAsync(id);

            if (check == null)
            {
                return NotFound(new V1DTO.MessageDTO($"check with id {id} not found"));
            }

            return Ok(_mapper.Map( check));
        }
        
        /// <summary>
        /// Update the GpsSession
        /// </summary>
        /// <param name="id">Session Id</param>
        /// <param name="check">check object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> Putcheck(Guid id, V1DTO.Check check)
        {
            if (id != check.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("Id and check.id do not match"));
            }

            if (!await _bll.Checks.ExistsAsync(check.Id, User.UserId()))
            {
                return NotFound(new V1DTO.MessageDTO($"Current user does not have check with this id {id}"));
            }

            check.AppUserId = User.UserId();
            await _bll.Checks.UpdateAsync(_mapper.Map(check));
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }
        
        /// <summary>
        /// Post the new check
        /// </summary>
        /// <param name="check"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Check))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<Check>> Postcheck(V1DTO.Check check)
        {
            check.AppUserId = User.UserId();
            var bllEntity = _mapper.Map(check);
            
            _bll.Checks.Add(bllEntity);
            await _bll.SaveChangesAsync();
            check.Id = bllEntity.Id;

            return CreatedAtAction("Getcheck",
                new {id = check.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                check);
        }
        
        /// <summary>
        /// Delete the check
        /// </summary>
        /// <param name="id">Session Id to delete.</param>
        /// <returns>GpSession just deleted</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Check))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<Check>> Deletecheck(Guid id)
        {
            var userIdTKey = User.IsInRole("admin") ? null : (Guid?) User.UserId();

            var check =
                await _bll.Checks.FirstOrDefaultAsync(id, userIdTKey);
            
            if (check == null)
            {
                return NotFound(new V1DTO.MessageDTO($"check with id {id} not found!"));
            }

            await _bll.Checks.RemoveAsync(check, userIdTKey);
            await _bll.SaveChangesAsync();

            return Ok(check);
        }
    }
}
