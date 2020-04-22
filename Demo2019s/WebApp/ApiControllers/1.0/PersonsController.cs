using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;
using Person = Domain.Person;

namespace WebApp.ApiControllers._1._0
{
    
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = "Admin")]
    public class PersonsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public PersonsController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: api/Persons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
            var owners = (await _bll.Persons.AllAsync(User.UserGuidId()))
                .Select(bllEntity => new Person()
                {
                    Id = bllEntity.Id,
                    FirstName = bllEntity.FirstName,
                    LastName = bllEntity.LastName,
                }) ;
            
            return Ok(owners);

        }

        /// <summary>
        /// Find and return person from data soruce
        /// </summary>
        /// <param name="id">person id - guid</param>
        /// <returns>Person object based on id</returns>
        /// <response code="200">The person was successfully retrieved.</response>
        /// <response code="404">The person does not exist.</response>
        [ProducesResponseType( typeof( Person ), 200 )]	
        [ProducesResponseType( 404 )]
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(Guid id)
        {
            var person = await _bll.Persons.FirstOrDefaultAsync(id, User.UserGuidId());

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);

        }

        // PUT: api/Persons/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(Guid id, PersonEdit personEditDTO)
        {
            if (id != personEditDTO.Id)
            {
                return BadRequest();
            }

            var person = await _bll.Persons.FirstOrDefaultAsync(personEditDTO.Id, User.UserGuidId());
            if (person == null)
            {
                return BadRequest();
            }

            person.FirstName = personEditDTO.FirstName;
            person.LastName = personEditDTO.LastName;
            
            _bll.Persons.Update(person);


            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Persons.ExistsAsync(id, User.UserGuidId()))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();

        }

        // POST: api/Persons
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(PersonCreate personCreateDTO)
        {
            var owner = new BLL.App.DTO.Person
            {
                AppUserId = User.UserGuidId(),
                FirstName = personCreateDTO.FirstName,
                LastName = personCreateDTO.LastName
            };

            _bll.Persons.Add(owner);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new {id = owner.Id}, owner);

        }

        // DELETE: api/Persons/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> DeletePerson(Guid id)
        {
            var person = await _bll.Persons.FirstOrDefaultAsync(id, User.UserGuidId());
            if (person == null)
            {
                return NotFound();
            }

            _bll.Persons.Remove(person);
            await _bll.SaveChangesAsync();

            return Ok(person);
        }
    }
}
