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
using Person = Domain.Person;
using Service = Domain.Service;

namespace WebApp.ApiControllers._1._0
 {
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ServicesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ServicesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Services
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetServices()
        {
            var services = (await _bll.Services.AllAsync(User.UserGuidId()))
                .Select(bllEntity => new Person()
                {
                    Id = bllEntity.Id,
                    FirstName = bllEntity.NameOfService,
                }) ;
            
            return Ok(services);
        }

        // GET: api/Services/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetService(Guid id)
        {
            var service = await _bll.Services.FirstOrDefaultAsync(id, User.UserGuidId());

            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }

        // PUT: api/Services/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(Guid id, ServiceEdit serviceEditDTO)
        {
            if (id != serviceEditDTO.Id)
            {
                return BadRequest();
            }

            var service = await _bll.Services.FirstOrDefaultAsync(serviceEditDTO.Id, User.UserGuidId());
            if (service == null)
            {
                return BadRequest();
            }

            service.NameOfService = serviceEditDTO.NameOfService;

            _bll.Services.Update(service);


            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Services.ExistsAsync(id, User.UserGuidId()))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Services
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Service>> PostService(ServiceCreate serviceCreateDTO)
        {
            var service = new BLL.App.DTO.Service()
            {
                AppUserId = User.UserGuidId(),
                NameOfService = serviceCreateDTO.NameOfService,
            };

            _bll.Services.Add(service);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetService", new {id = service.Id}, service);
        }

        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Service>> DeleteService(Guid id)
        {
            var service = await _bll.Services.FirstOrDefaultAsync(id, User.UserGuidId());
            if (service == null)
            {
                return NotFound();
            }

            _bll.Services.Remove(service);
            await _bll.SaveChangesAsync();

            return Ok(service);
        }
    }
}
