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
using WashType = Domain.WashType;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WashTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public WashTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/WashTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WashType>>> GetWashTypes()
        {
            var washTypes = (await _bll.WashTypes.AllAsync(User.UserGuidId()))
                .Select(bllEntity => new WashType()
                {
                    Id = bllEntity.Id,
                    NameOfWash = bllEntity.NameOfWash,
                });
            
            return Ok(washTypes);
        }

        // GET: api/WashTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WashType>> GetWashType(Guid id)
        {
            var washType = await _bll.WashTypes.FirstOrDefaultAsync(id, User.UserGuidId());

            if (washType == null)
            {
                return NotFound();
            }

            return Ok(washType);
        }

        // PUT: api/WashTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWashType(Guid id, WashTypeEdit washTypeEditDTO)
        {
            if (id != washTypeEditDTO.Id)
            {
                return BadRequest();
            }

            var washType = await _bll.WashTypes.FirstOrDefaultAsync(washTypeEditDTO.Id, User.UserGuidId());
            if (washType == null)
            {
                return BadRequest();
            }

            washType.NameOfWash = washTypeEditDTO.NameOfWash;

            _bll.WashTypes.Update(washType);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.WashTypes.ExistsAsync(id, User.UserGuidId()))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/WashTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<WashType>> PostWashType(WashTypeCreate washTypeCreatedto)
        {
            var washType = new BLL.App.DTO.WashType()
            {
                AppUserId = User.UserGuidId(),
                NameOfWash = washTypeCreatedto.NameOfWash,
            };

            _bll.WashTypes.Add(washType);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetWashType", new {id = washType.Id}, washType);
        }

        // DELETE: api/WashTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WashType>> DeleteWashType(Guid id)
        {
            var washType = await _bll.WashTypes.FirstOrDefaultAsync(id, User.UserGuidId());
            if (washType == null)
            {
                return NotFound();
            }

            _bll.WashTypes.Remove(washType);
            await _bll.SaveChangesAsync();

            return Ok(washType);
        }
    }
}
