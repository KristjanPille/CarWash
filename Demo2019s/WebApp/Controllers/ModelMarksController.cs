using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using WebApp.Data;

namespace WebApp.Controllers
{
    public class ModelMarksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ModelMarksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ModelMarks
        public async Task<IActionResult> Index()
        {
            return View(await _context.ModelMarks.ToListAsync());
        }

        // GET: ModelMarks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelMark = await _context.ModelMarks
                .FirstOrDefaultAsync(m => m.ModelMarkID == id);
            if (modelMark == null)
            {
                return NotFound();
            }

            return View(modelMark);
        }

        // GET: ModelMarks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ModelMarks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModelMarkID,mark,model")] ModelMark modelMark)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modelMark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(modelMark);
        }

        // GET: ModelMarks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelMark = await _context.ModelMarks.FindAsync(id);
            if (modelMark == null)
            {
                return NotFound();
            }
            return View(modelMark);
        }

        // POST: ModelMarks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ModelMarkID,mark,model")] ModelMark modelMark)
        {
            if (id != modelMark.ModelMarkID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modelMark);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModelMarkExists(modelMark.ModelMarkID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(modelMark);
        }

        // GET: ModelMarks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelMark = await _context.ModelMarks
                .FirstOrDefaultAsync(m => m.ModelMarkID == id);
            if (modelMark == null)
            {
                return NotFound();
            }

            return View(modelMark);
        }

        // POST: ModelMarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modelMark = await _context.ModelMarks.FindAsync(id);
            _context.ModelMarks.Remove(modelMark);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModelMarkExists(int id)
        {
            return _context.ModelMarks.Any(e => e.ModelMarkID == id);
        }
    }
}
