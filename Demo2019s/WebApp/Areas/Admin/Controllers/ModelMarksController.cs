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

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class ModelMarksController : Controller
    {
        private readonly AppDbContext _context;

        public ModelMarksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ModelMarks
        public async Task<IActionResult> Index()
        {
            return View(await _context.ModelMarks.ToListAsync());
        }

        // GET: Admin/ModelMarks/Details/5
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

        // GET: Admin/ModelMarks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ModelMarks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: Admin/ModelMarks/Edit/5
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

        // POST: Admin/ModelMarks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: Admin/ModelMarks/Delete/5
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

        // POST: Admin/ModelMarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var modelMark = await _context.ModelMarks.FindAsync(id);
            _context.ModelMarks.Remove(modelMark);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModelMarkExists(Guid id)
        {
            return _context.ModelMarks.Any(e => e.Id == id);
        }
    }
}
