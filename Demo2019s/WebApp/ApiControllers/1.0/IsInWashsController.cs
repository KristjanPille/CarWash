using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;
using IsInWash = Domain.IsInWash;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class IsInWashesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public IsInWashesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/IsInWashs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IsInWash>>> GetIsInWashes()
        {
            var isInWash = (await _bll.IsInWashes.AllAsync())
                .Select(bllEntity => new IsInWash()
                {
                    Id = bllEntity.Id,
                    From = bllEntity.From,
                    To = bllEntity.To,
                }) ;
            
            return Ok(isInWash);
        }

        // GET: api/IsInWashs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IsInWash>> GetIsInWash(Guid id)
        {
            var isInWash = await _bll.IsInWashes.FirstOrDefaultAsync(id, User.UserGuidId());

            if (isInWash == null)
            {
                return NotFound();
            }

            return Ok(isInWash);
        }

        // PUT: api/IsInWashs/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIsInWash(Guid id, IsInWash isInWashEdit)
        {
            if (id != isInWashEdit.Id)
            {
                return BadRequest();
            }

            var isInWash = await _bll.IsInWashes.FirstOrDefaultAsync(isInWashEdit.Id, User.UserGuidId());
            if (isInWash == null)
            {
                return BadRequest();
            }

            isInWash.From = isInWash.From;
            isInWash.To = isInWash.To;

            _bll.IsInWashes.Update(isInWash);


            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.IsInWashes.ExistsAsync(id, User.UserGuidId()))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/IsInWashs
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<IsInWash>> PostIsInWash(IsInWashCreate isInWashCreateDTO)
        {
            var isInWash = new BLL.App.DTO.IsInWash()
            {
                AppUserId = User.UserGuidId(),
                From = isInWashCreateDTO.From,
                To = isInWashCreateDTO.To,
            };

            _bll.IsInWashes.Add(isInWash);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetIsInWash", new {id = isInWash.Id}, isInWash);
        }

        // DELETE: api/IsInWashs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IsInWash>> DeleteIsInWash(Guid id)
        {
            var isInWash = await _bll.IsInWashes 
                .FirstOrDefaultAsync(id, User.UserGuidId());
            if (isInWash == null)
            {
                return NotFound();
            }

            _bll.IsInWashes.Remove(isInWash);
            await _bll.SaveChangesAsync();

            return Ok(isInWash);
        }
    }
}
