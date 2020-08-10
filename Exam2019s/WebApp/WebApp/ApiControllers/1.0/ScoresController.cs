using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL.App.EF;
using Domain.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1.Mappers;
using V1DTO=PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Scores Api Controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ScoresController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ScoreMapper _mapper = new ScoreMapper();

        public ScoresController(AppDbContext context, IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Scores
        /// <summary>
        /// Get all Scores
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.Score>>> GetScores()
        {
            return Ok((await _bll.Scores.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/scores/5
        /// <summary>
        /// Get Specific Score
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<PublicApi.DTO.v1.Score>> GetScore(Guid id)
        {
            var score= await _bll.Scores.FirstOrDefaultAsync(id);

            if (score== null)
            {
                return NotFound(new V1DTO.MessageDTO("Score not found"));
            }

            return Ok(_mapper.Map(score));
        }
        
                
        // GET: api/Score/5
        /// <summary>
        /// Get Specific Score
        /// </summary>
        /// <param name="quizId"></param>
        /// <returns></returns>
        [HttpGet("Average/{quizId}")]
        [AllowAnonymous]
        [Produces("application/json")]
        public async Task<double> GetQuizSpecificQuestions(Guid quizId)
        {
            var score = await _bll.Scores.GetAverageScore(quizId);
            
            return score;
        }

        // PUT: api/Scores/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update Camapign, only for admins
        /// </summary>
        /// <param name="id"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
           [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<IActionResult> PutScore(Guid id, V1DTO.Score score)
        {
            if (id != score.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and Score.id do not match"));
            }

            await _bll.Scores.UpdateAsync(_mapper.Map(score));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Scores
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create new Score. only for admins
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<Guid> PostScore(V1DTO.Score score)
        {
            score.AppUserId = User.UserId();
            var bllEntity = _mapper.Map(score);
            _bll.Scores.Add(bllEntity);
            await _bll.SaveChangesAsync();
            score.Id = bllEntity.Id;

            return score.Id;
        }

        // DELETE: api/Scores/5m,
        /// <summary>
        /// Delete QuesScoretion, only for admins
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<ActionResult<Score>> DeleteScore(Guid id)
        {
            var score = await _bll.Scores.FirstOrDefaultAsync(id);
            if (score== null)
            {
                return NotFound(new V1DTO.MessageDTO("Score not found"));
            }

            await _bll.Scores.RemoveAsync(score);
            await _bll.SaveChangesAsync();

            return StatusCode(204);
        }
    }
}
