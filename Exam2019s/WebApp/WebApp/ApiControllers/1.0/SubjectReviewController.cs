using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1.Mappers;
using V1DTO=PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// SubjectReviews Api Controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class SubjectReviewsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly SubjectReviewMapper _mapper = new SubjectReviewMapper();

        public SubjectReviewsController(AppDbContext context, IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/SubjectReviews
        /// <summary>
        /// Get all SubjectReviews
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.SubjectReview>>> GetSubjectReviews()
        {
            return Ok((await _bll.SubjectReviews.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/SubjectReview/5
        /// <summary>
        /// Get Specific SubjectReview
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<PublicApi.DTO.v1.SubjectReview>> GetSubjectReview(Guid id)
        {
            var subjectReview= await _bll.SubjectReviews.FirstOrDefaultAsync(id);

            if (subjectReview== null)
            {
                return NotFound(new V1DTO.MessageDTO("SubjectReview not found"));
            }

            return Ok(_mapper.Map(subjectReview));
        }

        // PUT: api/SubjectReviews/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update Camapign, only for admins
        /// </summary>
        /// <param name="id"></param>
        /// <param name="subjectReview"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutSubjectReview(Guid id, V1DTO.SubjectReview subjectReview)
        {
            if (id != subjectReview.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and subjectReview.id do not match"));
            }

            await _bll.SubjectReviews.UpdateAsync(_mapper.Map(subjectReview));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/SubjectReviews
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create new SubjectReview. only for admins
        /// </summary>
        /// <param name="subjectReview"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.SubjectReview))]
        public async Task<ActionResult<SubjectReview>> PostSubjectReview(V1DTO.SubjectReview subjectReview)
        {
            var bllEntity = _mapper.Map(subjectReview);
            _bll.SubjectReviews.Add(bllEntity);
            await _bll.SaveChangesAsync();
            subjectReview.Id = bllEntity.Id;

            return CreatedAtAction("GetsubjectReviews",
                new {id = subjectReview.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                subjectReview);
        }

        // DELETE: api/subjectReviews/5m,
        /// <summary>
        /// Delete SubjectReview, only for admins
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<SubjectReview>> DeleteSubjectReview(Guid id)
        {
            var subjectReview= await _bll.SubjectReviews.FirstOrDefaultAsync(id);
            if (subjectReview== null)
            {
                return NotFound(new V1DTO.MessageDTO("subjectReview not found"));
            }

            await _bll.SubjectReviews.RemoveAsync(subjectReview);
            await _bll.SaveChangesAsync();

            return StatusCode(204);
        }
    }
}
