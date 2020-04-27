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
using Check = PublicApi.DTO.v1.Check;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChecksController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ChecksController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Checks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Check>>> GetChecks()
        {
            var check = (await _bll.Checks.AllAsync())
                .Select(bllEntity => new Check()
                {
                    Id = bllEntity.Id,
                    Comment = bllEntity.Comment,
                    Vat = bllEntity.Vat,
                    AmountExcludeVat = bllEntity.AmountExcludeVat,
                }) ;
            
            return Ok(check);
        }

        // GET: api/Checks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Check>> GetCheck(Guid id)
        {
            var check = await _bll.Checks.FirstOrDefaultAsync(id, User.UserGuidId());

            if (check == null)
            {
                return NotFound();
            }

            return Ok(check);
        }

        // PUT: api/Checks/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCheck(Guid id, CheckEdit checkEditDTO)
        {
            if (id != checkEditDTO.Id)
            {
                return BadRequest();
            }

            var check = await _bll.Checks.FirstOrDefaultAsync(checkEditDTO.Id, User.UserGuidId());
            if (check == null)
            {
                return BadRequest();
            }

            check.Comment = checkEditDTO.Comment;

            _bll.Checks.Update(check);


            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Checks.ExistsAsync(id, User.UserGuidId()))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Checks
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Check>> PostCheck(CheckCreate checkcreateDTO)
        {
            var check = new BLL.App.DTO.Check()
            {
                AppUserId = User.UserGuidId(),
                Comment = checkcreateDTO.Comment,
                Vat = checkcreateDTO.Vat,
                AmountExcludeVat = checkcreateDTO.AmountExcludeVat,
                AmountWithVat = checkcreateDTO.AmountWithVat,
            };

            _bll.Checks.Add(check);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCheck", new {id = check.Id}, check);
        }
        
        
        // DELETE: api/Checks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Check>> DeleteCheck(Guid id)
        {
            var check = await _bll.Checks 
                .FirstOrDefaultAsync(id, User.UserGuidId());
            if (check == null)
            {
                return NotFound();
            }

            _bll.Checks.Remove(check);
            await _bll.SaveChangesAsync();

            return Ok(check);
        }
    }
    
}
