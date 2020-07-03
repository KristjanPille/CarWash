using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PublicApi.DTO.v1.Mappers;
using V1DTO=PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class AppUsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppUserMapper _mapper = new AppUserMapper();

        public AppUsersController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/AppUsers/appUser
        [HttpGet("appuser")]
        public async Task<ActionResult<PublicApi.DTO.v1.Identity.AppUser>> GetAppUser()
        {
            var appUser = await _userManager.FindByEmailAsync(User.Identity.Name);

            if (appUser == null)
            {
                return NotFound();
            }
            
            return _mapper.Map(appUser);
        }

        // PUT: api/AppUsers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppUser(Guid id, PublicApi.DTO.v1.Identity.AppUser appUser)
        {
            if (id != appUser.Id)
            {
                return BadRequest();
            }

            var domainAppUser = await _context.Users.FindAsync(id);

            domainAppUser.Email = appUser.Email;
            domainAppUser.FirstName = appUser.FirstName;
            domainAppUser.LastName = appUser.LastName;
            domainAppUser.PhoneNumber = appUser.PhoneNumber;

            _context.Entry(domainAppUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppUserExists(id))
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

        // POST: api/AppUsers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AppUser>> PostAppUser(AppUser appUser)
        {
            _context.Users.Add(appUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppUser", new { id = appUser.Id }, appUser);
        }

        // DELETE: api/AppUsers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AppUser>> DeleteAppUser(Guid id)
        {
            if (!User.IsInRole("admin"))
            {
                return BadRequest(new V1DTO.MessageDTO("Not allowed!"));
            }
            
            var appUser = await _context.Users.FindAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }

            _context.Users.Remove(appUser);
            await _context.SaveChangesAsync();

            return appUser;
        }

        private bool AppUserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
