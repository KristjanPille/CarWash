using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WashesController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public WashesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Washes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WashDTO>>> GetWashes()
        {
            return Ok(await _uow.Washes.DTOAllAsync(User.UserGuidId()));
        }

        // GET: api/Washes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WashDTO>> GetWash(Guid id)
        {
            var carDTO = await _uow.Washes.DTOFirstOrDefaultAsync(id, User.UserGuidId());

            if (carDTO == null)
            {
                return NotFound();
            }

            return Ok(carDTO);

        }

        // PUT: api/Washes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWash(Guid id, WashEditDTO WashEditDTO)
        {
            if (id != WashEditDTO.Id)
            {
                return BadRequest();
            }

            var Wash = await _uow.Washes.FirstOrDefaultAsync(WashEditDTO.Id, User.UserGuidId());
            if (Wash == null)
            {
                return BadRequest();
            }
            Wash.NameOfWashType = WashEditDTO.NameOfWashType;

            _uow.Washes.Update(Wash);
            
            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Washes.ExistsAsync(id, User.UserGuidId()))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();

        }

        // POST: api/Washes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Wash>> PostWash(WashCreateDTO WashCreateDTO)
        {
            var wash = new Wash();
            wash.NameOfWashType = WashCreateDTO.NameOfWashType;
            
            _uow.Washes.Add(wash);
           
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetWash", new { id = wash.Id }, wash);

        }

        // DELETE: api/Washes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Wash>> DeleteWash(Guid id)
        {
            var car = await _uow.Washes.FirstOrDefaultAsync(id, User.UserGuidId());
            if (car == null)
            {
                return NotFound();
            }

            _uow.Washes.Remove(car);
            await _uow.SaveChangesAsync();
            return Ok(car);

        }
    }
}
