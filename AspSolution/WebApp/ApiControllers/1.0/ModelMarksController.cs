using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1.Mappers;
using ModelMark = Domain.App.ModelMark;
using V1DTO=PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
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
        private readonly MarkMapper _markMapper = new MarkMapper();
        private readonly ModelMapper _modelMapper = new ModelMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public ModelMarksController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ModelMarks
        /// <summary>
        /// Get all modelmarks
        /// </summary>
        /// <returns>returns model marks from db</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<ModelMark>>> GetModelMarks()
        {
            return Ok((await _bll.ModelMarks.GetAllAsync()).Select(e => _mapper.Map(e)));
        }
        
        // GET: api/Marks
        /// <summary>
        /// Returns all Marks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Marks")]
        [AllowAnonymous]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<V1DTO.MarkDTO>>> GetMarks()
        {
            var marks = (await _bll.ModelMarks.GetMarks());
            return Ok(marks.Select(e => _markMapper.Map(e)));
        }
        
        // GET: api/Models/{mark}
        /// <summary>
        /// Finds Specific Model from given mark
        /// </summary>
        /// <param name="mark"></param>
        /// <returns>returns Model</returns>
        [HttpGet]
        [Route("Models/{mark}")]
        [AllowAnonymous]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<V1DTO.ModelDTO>>> GetMarkSpecificModels(string mark)
        {
            var models = await _bll.ModelMarks.GetMarkModels(mark);
            
            return Ok( models.Select(e => _modelMapper.Map(e)) );
        }
        
        
        // get first marks getMarks than from id get models
        // GET: api/ModelMarks/5
        /// <summary>
        /// Gets modelmark
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<ModelMark>> GetModelMark(Guid id)
        {
            var modelMark= await _bll.ModelMarks.FirstOrDefaultAsync(id);

            if (modelMark== null)
            {
                return NotFound(new V1DTO.MessageDTO("ModelMark not found"));
            }

            return Ok(_mapper.Map(modelMark));
        }

        // PUT: api/ModelMarks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Updates ModelMark
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modelMark"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
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
        /// <summary>
        /// Creates new ModelMark
        /// </summary>
        /// <param name="modelMark"></param>
        /// <returns>Returns created</returns>
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
        /// <summary>
        /// Deletes ModelMark
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
