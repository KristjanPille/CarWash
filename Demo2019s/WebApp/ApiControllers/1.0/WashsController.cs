using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;
using Wash = PublicApi.DTO.v1.Wash;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WashesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public WashesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Washes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wash>>> GetWashes()
        {
            var washes = (await _bll.Washes.AllAsync(User.UserGuidId()))
                .Select(bllEntity => new Wash()
                {
                    Id = bllEntity.Id,
                    NameOfWashType = bllEntity.NameOfWashType,
                });
            
            return Ok(washes);
        }

        // GET: api/Washes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Wash>> GetWash(Guid id)
        {
            var wash = await _bll.Washes.FirstOrDefaultAsync(id, User.UserGuidId());

            if (wash == null)
            {
                return NotFound();
            }

            return Ok(wash);
        }

        // PUT: api/Washes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWash(Guid id, WashEdit WashEditDTO)
        {
            if (id != WashEditDTO.Id)
            {
                return BadRequest();
            }

            var wash = await _bll.Washes.FirstOrDefaultAsync(WashEditDTO.Id, User.UserGuidId());
            if (wash == null)
            {
                return BadRequest();
            }

            wash.NameOfWashType = WashEditDTO.NameOfWashType;

            _bll.Washes.Update(wash);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Washes.ExistsAsync(id, User.UserGuidId()))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Washes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Wash>> PostWash(WashCreate WashCreateDTO)
        {
            var wash = new BLL.App.DTO.Wash()
            {
                AppUserId = User.UserGuidId(),
                NameOfWashType = WashCreateDTO.NameOfWashType,
            };

            _bll.Washes.Add(wash);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetWash", new {id = wash.Id}, wash);

        }

        // DELETE: api/Washes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Wash>> DeleteWash(Guid id)
        {
            var wash = await _bll.Washes.FirstOrDefaultAsync(id, User.UserGuidId());
            if (wash == null)
            {
                return NotFound();
            }

            _bll.Washes.Remove(wash);
            await _bll.SaveChangesAsync();

            return Ok(wash);
        }
    }
}
