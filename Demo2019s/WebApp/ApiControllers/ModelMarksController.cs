using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelMarksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ModelMarksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ModelMarks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModelMark>>> GetModelMarks()
        {
            return await _context.ModelMarks.ToListAsync();
        }

        // GET: api/ModelMarks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ModelMark>> GetModelMark(int id)
        {
            var modelMark = await _context.ModelMarks.FindAsync(id);

            if (modelMark == null)
            {
                return NotFound();
            }

            return modelMark;
        }

        // PUT: api/ModelMarks/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModelMark(Guid id, ModelMark modelMark)
        {
            if (id != modelMark.Id)
            {
                return BadRequest();
            }

            _context.Entry(modelMark).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelMarkExists(id))
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

        // POST: api/ModelMarks
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ModelMark>> PostModelMark(ModelMark modelMark)
        {
            _context.ModelMarks.Add(modelMark);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModelMark", new { id = modelMark.Id }, modelMark);
        }

        // DELETE: api/ModelMarks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ModelMark>> DeleteModelMark(int id)
        {
            var modelMark = await _context.ModelMarks.FindAsync(id);
            if (modelMark == null)
            {
                return NotFound();
            }

            _context.ModelMarks.Remove(modelMark);
            await _context.SaveChangesAsync();

            return modelMark;
        }

        private bool ModelMarkExists(Guid id)
        {
            return _context.ModelMarks.Any(e => e.Id == id);
        }
    }
}
