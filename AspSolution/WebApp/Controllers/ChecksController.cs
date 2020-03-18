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
    public class ChecksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChecksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Checks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Checks.Include(c => c.Person).Include(c => c.Wash);
            return View(await applicationDbContext.ToListAsync());
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
                .FirstOrDefaultAsync(m => m.checkID == id);
            if (check == null)
            {
                return NotFound();
            }

            return View(check);
        }

        // GET: Checks/Create
        public IActionResult Create()
        {
            ViewData["personID"] = new SelectList(_context.Persons, "personID", "personID");
            ViewData["washID"] = new SelectList(_context.Washes, "washID", "washID");
            return View();
        }

        // POST: Checks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("checkID,personID,washID,dateTimeCheck,amountExcludeVat,amountWithVat,vat,comment")] Check check)
        {
            if (ModelState.IsValid)
            {
                _context.Add(check);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["personID"] = new SelectList(_context.Persons, "personID", "personID", check.personID);
            ViewData["washID"] = new SelectList(_context.Washes, "washID", "washID", check.washID);
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
            ViewData["personID"] = new SelectList(_context.Persons, "personID", "personID", check.personID);
            ViewData["washID"] = new SelectList(_context.Washes, "washID", "washID", check.washID);
            return View(check);
        }

        // POST: Checks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("checkID,personID,washID,dateTimeCheck,amountExcludeVat,amountWithVat,vat,comment")] Check check)
        {
            if (id != check.checkID)
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
                    if (!CheckExists(check.checkID))
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
            ViewData["personID"] = new SelectList(_context.Persons, "personID", "personID", check.personID);
            ViewData["washID"] = new SelectList(_context.Washes, "washID", "washID", check.washID);
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
                .FirstOrDefaultAsync(m => m.checkID == id);
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
            return _context.Checks.Any(e => e.checkID == id);
        }
    }
}