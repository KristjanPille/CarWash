using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    public class ModelMarksController : Controller
    {
        private readonly AppDbContext _context;

        public ModelMarksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ModelMarks
          [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.ModelMarks.ToListAsync());
        }

        // GET: ModelMarks/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelMark = await _context.ModelMarks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modelMark == null)
            {
                return NotFound();
            }

            return View(modelMark);
        }

        // GET: ModelMarks/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ModelMarks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("Mark,Model,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] ModelMark modelMark)
        {
            if (ModelState.IsValid)
            {
                modelMark.Id = Guid.NewGuid();
                _context.Add(modelMark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(modelMark);
        }

        // GET: ModelMarks/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(Guid? id)
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Mark,Model,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] ModelMark modelMark)
        {
            if (id != modelMark.Id)
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
                    if (!ModelMarkExists(modelMark.Id))
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelMark = await _context.ModelMarks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modelMark == null)
            {
                return NotFound();
            }

            return View(modelMark);
        }

        // POST: ModelMarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var modelMark = await _context.ModelMarks.FindAsync(id);
            _context.ModelMarks.Remove(modelMark);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "admin")]
        private bool ModelMarkExists(Guid id)
        {
            return _context.ModelMarks.Any(e => e.Id == id);
        }
    }
}
