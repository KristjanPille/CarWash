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
    public class ChecksController : Controller
    {
        private readonly AppDbContext _context;

        public ChecksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Checks
        public async Task<IActionResult> Index()
        {
            var AppDbContext = _context.Checks.Include(c => c.Person).Include(c => c.Wash);
            return View(await AppDbContext.ToListAsync());
        }

        // GET: Checks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var check = await _context.Checks
                .Include(c => c.Person)
                .Include(c => c.Wash)
                .FirstOrDefaultAsync(m => m.CheckId == id);
            if (check == null)
            {
                return NotFound();
            }

            return View(check);
        }

        // GET: Checks/Create
        public IActionResult Create()
        {
            ViewData["PersonId"] = new SelectList(_context.Set<Person>(), "Id", "AppUserId");
            ViewData["WashId"] = new SelectList(_context.Set<Wash>(), "WashId", "NameOfWashType");
            return View();
        }

        // POST: Checks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CheckId,PersonId,WashId,DateTimeCheck,AmountExcludeVat,AmountWithVat,Vat,Comment")] Check check)
        {
            if (ModelState.IsValid)
            {
                _context.Add(check);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(_context.Set<Person>(), "Id", "AppUserId", check.PersonId);
            ViewData["WashId"] = new SelectList(_context.Set<Wash>(), "WashId", "NameOfWashType", check.WashId);
            return View(check);
        }

        // GET: Checks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var check = await _context.Checks.FindAsync(id);
            if (check == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.Set<Person>(), "Id", "AppUserId", check.PersonId);
            ViewData["WashId"] = new SelectList(_context.Set<Wash>(), "WashId", "NameOfWashType", check.WashId);
            return View(check);
        }

        // POST: Checks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CheckId,PersonId,WashId,DateTimeCheck,AmountExcludeVat,AmountWithVat,Vat,Comment")] Check check)
        {
            if (id != check.CheckId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(check);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CheckExists(check.CheckId))
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
            ViewData["PersonId"] = new SelectList(_context.Set<Person>(), "Id", "AppUserId", check.PersonId);
            ViewData["WashId"] = new SelectList(_context.Set<Wash>(), "WashId", "NameOfWashType", check.WashId);
            return View(check);
        }

        // GET: Checks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var check = await _context.Checks
                .Include(c => c.Person)
                .Include(c => c.Wash)
                .FirstOrDefaultAsync(m => m.CheckId == id);
            if (check == null)
            {
                return NotFound();
            }

            return View(check);
        }

        // POST: Checks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var check = await _context.Checks.FindAsync(id);
            _context.Checks.Remove(check);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CheckExists(int id)
        {
            return _context.Checks.Any(e => e.CheckId == id);
        }
    }
}