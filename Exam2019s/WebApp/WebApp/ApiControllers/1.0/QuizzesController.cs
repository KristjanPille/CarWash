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
    /// Quizs Api Controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class QuizzesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly QuizMapper _mapper = new QuizMapper();

        public QuizzesController(AppDbContext context, IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Quizs
        /// <summary>
        /// Get all Quizs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.Quiz>>> GetQuizs()
        {
            return Ok((await _bll.Quizzes.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/Quizs/5
        /// <summary>
        /// Get Specific Quiz
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<PublicApi.DTO.v1.Quiz>> GetQuiz(Guid id)
        {
            var Quiz= await _bll.Quizzes.FirstOrDefaultAsync(id);

            if (Quiz== null)
            {
                return NotFound(new V1DTO.MessageDTO("Quiz not found"));
            }

            return Ok(_mapper.Map(Quiz));
        }

        // PUT: api/Quizs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update Camapign, only for admins
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Quiz"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutQuiz(Guid id, V1DTO.Quiz Quiz)
        {
            if (id != Quiz.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and Quiz.id do not match"));
            }

            await _bll.Quizzes.UpdateAsync(_mapper.Map(Quiz));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Quizs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create new Quiz. only for admins
        /// </summary>
        /// <param name="Quiz"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Quiz))]
        public async Task<ActionResult<Quiz>> PostQuiz(V1DTO.Quiz Quiz)
        {
            var bllEntity = _mapper.Map(Quiz);
            _bll.Quizzes.Add(bllEntity);
            await _bll.SaveChangesAsync();
            Quiz.Id = bllEntity.Id;

            return CreatedAtAction("GetQuiz",
                new {id = Quiz.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                Quiz);
        }

        // DELETE: api/Quizs/5
        /// <summary>
        /// Delete Quiz, only for admins
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<ActionResult<Quiz>> DeleteQuiz(Guid id)
        {
            var Quiz= await _bll.Quizzes.FirstOrDefaultAsync(id);
            if (Quiz== null)
            {
                return NotFound(new V1DTO.MessageDTO("Quiz not found"));
            }

            await _bll.Quizzes.RemoveAsync(Quiz);
            await _bll.SaveChangesAsync();

            return StatusCode(204);
        }
    }
}
