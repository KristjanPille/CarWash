using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1.Mappers;
using V1DTO=PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{    /// <summary>
    /// ModelMarks Api Controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ModelMarksController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ModelMarkMapper _mapper = new ModelMarkMapper();
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public ModelMarksController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ModelMarks
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<ModelMark>>> GetModelMarks()
        {
            return Ok((await _bll.ModelMarks.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/ModelMarks/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<ModelMark>> GetModelMark(Guid id)
        {
            var modelMark= await _bll.ModelMarks.FirstOrDefaultAsync(id);

            if (modelMark== null)
            {
                return NotFound(new V1DTO.MessageDTO("ModelMarknot found"));
            }

            return Ok(_mapper.Map(modelMark));
        }

        // PUT: api/ModelMarks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutModelMark(Guid id, V1DTO.ModelMark modelMark)
        {
            if (id != modelMark.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and ModelMark.id do not match"));
            }

            await _bll.ModelMarks.UpdateAsync(_mapper.Map(modelMark));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ModelMarks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.ModelMark))]
        public async Task<ActionResult<ModelMark>> PostModelMark(V1DTO.ModelMark modelMark)
        {
            var bllEntity = _mapper.Map(modelMark);
            _bll.ModelMarks.Add(bllEntity);
            await _bll.SaveChangesAsync();
            modelMark.Id = bllEntity.Id;

            return CreatedAtAction("GetModelMark",
                new {id = modelMark.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                modelMark);
        }

        // DELETE: api/ModelMarks/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<ModelMark>> DeleteModelMark(Guid id)
        {
            var modelMark= await _bll.ModelMarks.FirstOrDefaultAsync(id);
            if (modelMark== null)
            {
                return NotFound(new V1DTO.MessageDTO("ModelMark not found"));
            }

            await _bll.ModelMarks.RemoveAsync(modelMark);
            await _bll.SaveChangesAsync();

            return Ok(modelMark);
        }
    }
}
