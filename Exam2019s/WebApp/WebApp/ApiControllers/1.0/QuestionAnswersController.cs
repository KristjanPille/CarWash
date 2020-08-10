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
    /// Questions Api Controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class QuestionAnswersController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly QuestionAnswerMapper _mapper = new QuestionAnswerMapper();

        public QuestionAnswersController(AppDbContext context, IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Questions
        /// <summary>
        /// Get all Questions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.QuestionAnswer>>> GetQuestionAnswers()
        {
            return Ok((await _bll.QuestionAnswers.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/QuestionAnswer/5
        /// <summary>
        /// Get Specific Question
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<PublicApi.DTO.v1.Question>> GetQuestionAnswer(Guid id)
        {
            var questionAnswer = await _bll.QuestionAnswers.FirstOrDefaultAsync(id);

            if (questionAnswer== null)
            {
                return NotFound(new V1DTO.MessageDTO("QuestionAnswer not found"));
            }

            return Ok(_mapper.Map(questionAnswer));
        }

        // PUT: api/Questions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update questionAnswer, only for admins
        /// </summary>
        /// <param name="id"></param>
        /// <param name="questionAnswer"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutQuestionAnswer(Guid id, V1DTO.QuestionAnswer questionAnswer)
        {
            if (id != questionAnswer.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and Question.id do not match"));
            }

            await _bll.QuestionAnswers.UpdateAsync(_mapper.Map(questionAnswer));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/QuestionAnswers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create new Question. only for admins
        /// </summary>
        /// <param name="questionAnswer"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.QuestionAnswer))]
        public async Task<ActionResult<QuestionAnswer>> PostQuestionAnswer(V1DTO.QuestionAnswer questionAnswer)
        {
            var bllEntity = _mapper.Map(questionAnswer);
            _bll.QuestionAnswers.Add(bllEntity);
            await _bll.SaveChangesAsync();
            questionAnswer.Id = bllEntity.Id;

            return CreatedAtAction("GetQuestionAnswers",
                new {id = questionAnswer.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                questionAnswer);
        }

        // DELETE: api/QuestionAnswer/5,
        /// <summary>
        /// Delete QuestionAnswer, only for admins
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<QuestionAnswer>> DeleteQuestionAnswer(Guid id)
        {
            var questionAnswer= await _bll.QuestionAnswers.FirstOrDefaultAsync(id);
            if (questionAnswer== null)
            {
                return NotFound(new V1DTO.MessageDTO("QuestionAnswer not found"));
            }

            await _bll.QuestionAnswers.RemoveAsync(questionAnswer);
            await _bll.SaveChangesAsync();

            return StatusCode(204);
        }
    }
}
