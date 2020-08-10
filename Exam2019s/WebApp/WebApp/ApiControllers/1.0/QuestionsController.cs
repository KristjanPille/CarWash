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
    public class QuestionsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly QuestionMapper _mapper = new QuestionMapper();

        public QuestionsController(AppDbContext context, IAppBLL bll)
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
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.Question>>> GetQuestions()
        {
            return Ok((await _bll.Questions.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/Question/5
        /// <summary>
        /// Get Specific Question
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<PublicApi.DTO.v1.Question>> GetQuestion(Guid id)
        {
            var Question= await _bll.Questions.FirstOrDefaultAsync(id);

            if (Question== null)
            {
                return NotFound(new V1DTO.MessageDTO("Question not found"));
            }

            return Ok(_mapper.Map(Question));
        }

        // PUT: api/Questions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update Camapign, only for admins
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Question"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutQuestion(Guid id, V1DTO.Question Question)
        {
            if (id != Question.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and Question.id do not match"));
            }

            await _bll.Questions.UpdateAsync(_mapper.Map(Question));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Questions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create new Question. only for admins
        /// </summary>
        /// <param name="Question"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Question))]
        public async Task<ActionResult<Question>> PostQuestion(V1DTO.Question question)
        {
            var bllEntity = _mapper.Map(question);
            _bll.Questions.Add(bllEntity);
            await _bll.SaveChangesAsync();
            question.Id = bllEntity.Id;

            return CreatedAtAction("GetQuestions",
                new {id = question.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                question);
        }

        // DELETE: api/Questions/5m,
        /// <summary>
        /// Delete Question, only for admins
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<Question>> DeleteQuestion(Guid id)
        {
            var Question= await _bll.Questions.FirstOrDefaultAsync(id);
            if (Question== null)
            {
                return NotFound(new V1DTO.MessageDTO("Question not found"));
            }

            await _bll.Questions.RemoveAsync(Question);
            await _bll.SaveChangesAsync();

            return StatusCode(204);
        }
    }
}
