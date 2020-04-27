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
using ModelMark = Domain.ModelMark;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ModelMarksController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ModelMarksController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ModelMarks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModelMark>>> GetModelMarks()
        {
            var modelMarks = (await _bll.ModelMarks.AllAsync())
                .Select(bllEntity => new ModelMark()
                {
                    Id = bllEntity.Id,
                    Mark = bllEntity.Mark,
                    Model = bllEntity.Model,
                }) ;
            
            return Ok(modelMarks);
        }

        // GET: api/ModelMarks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ModelMark>> GetModelMark(Guid id)
        {
            var modelMark = await _bll.ModelMarks.FirstOrDefaultAsync(id, User.UserGuidId());

            if (modelMark == null)
            {
                return NotFound();
            }

            return Ok(modelMark);
        }

        // PUT: api/ModelMarks/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModelMark(Guid id, ModelMark modelMarkEditDTO)
        {
            if (id != modelMarkEditDTO.Id)
            {
                return BadRequest();
            }

            var modelMark = await _bll.ModelMarks.FirstOrDefaultAsync(modelMarkEditDTO.Id, User.UserGuidId());
            if (modelMark == null)
            {
                return BadRequest();
            }

            modelMark.Mark = modelMark.Mark;
            modelMark.Model = modelMark.Model;

            _bll.ModelMarks.Update(modelMark);


            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.ModelMarks.ExistsAsync(id, User.UserGuidId()))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/ModelMarks
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ModelMark>> PostModelMark(ModelMarkCreate modelMarkCreateDTO)
        {
            var modelMark = new BLL.App.DTO.ModelMark()
            {
                AppUserId = User.UserGuidId(),
                Model = modelMarkCreateDTO.Model,
                Mark = modelMarkCreateDTO.Mark,
            };

            _bll.ModelMarks.Add(modelMark);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetModelMark", new {id = modelMark.Id}, modelMark);
        }

        // DELETE: api/ModelMarks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ModelMark>> DeleteModelMark(Guid id)
        {
            var modelMark = await _bll.ModelMarks 
                .FirstOrDefaultAsync(id, User.UserGuidId());
            if (modelMark == null)
            {
                return NotFound();
            }

            _bll.ModelMarks.Remove(modelMark);
            await _bll.SaveChangesAsync();

            return Ok(modelMark);
        }
    }
}
