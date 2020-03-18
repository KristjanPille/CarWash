using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace WebApp.Controllers
{
    public class WashTypesController : Controller
    {
        private readonly AppDbContext _context;

        public WashTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: WashTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.WashTypes.ToListAsync());
        }

        // GET: WashTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var washType = await _context.WashTypes
                .FirstOrDefaultAsync(m => m.WashTypeId == id);
            if (washType == null)
            {
                return NotFound();
            }

            return View(washType);
        }

        // GET: WashTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WashTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WashTypeId,WashId,NameOfWash")] WashType washType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(washType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(washType);
        }

        // GET: WashTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var washType = await _context.WashTypes.FindAsync(id);
            if (washType == null)
            {
                return NotFound();
            }
            return View(washType);
        }

        // POST: WashTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WashTypeId,WashId,NameOfWash")] WashType washType)
        {
            if (id != washType.WashTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(washType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WashTypeExists(washType.WashTypeId))
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
            return View(washType);
        }

        // GET: WashTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var washType = await _context.WashTypes
                .FirstOrDefaultAsync(m => m.WashTypeId == id);
            if (washType == null)
            {
                return NotFound();
            }

            return View(washType);
        }

        // POST: WashTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var washType = await _context.WashTypes.FindAsync(id);
            _context.WashTypes.Remove(washType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WashTypeExists(int id)
        {
            return _context.WashTypes.Any(e => e.WashTypeId == id);
        }
    }
}
