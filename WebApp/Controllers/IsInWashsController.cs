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
    public class IsInWashsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IsInWashsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IsInWashs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.IsInWashes.Include(i => i.Car).Include(i => i.Person).Include(i => i.Wash);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: IsInWashs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isInWash = await _context.IsInWashes
                .Include(i => i.Car)
                .Include(i => i.Person)
                .Include(i => i.Wash)
                .FirstOrDefaultAsync(m => m.isInWashID == id);
            if (isInWash == null)
            {
                return NotFound();
            }

            return View(isInWash);
        }

        // GET: IsInWashs/Create
        public IActionResult Create()
        {
            ViewData["carID"] = new SelectList(_context.Cars, "carID", "carID");
            ViewData["personID"] = new SelectList(_context.Persons, "personID", "personID");
            ViewData["washID"] = new SelectList(_context.Washes, "washID", "washID");
            return View();
        }

        // POST: IsInWashs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("isInWashID,carID,personID,washID,from,to")] IsInWash isInWash)
        {
            if (ModelState.IsValid)
            {
                _context.Add(isInWash);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["carID"] = new SelectList(_context.Cars, "carID", "carID", isInWash.carID);
            ViewData["personID"] = new SelectList(_context.Persons, "personID", "personID", isInWash.personID);
            ViewData["washID"] = new SelectList(_context.Washes, "washID", "washID", isInWash.washID);
            return View(isInWash);
        }

        // GET: IsInWashs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isInWash = await _context.IsInWashes.FindAsync(id);
            if (isInWash == null)
            {
                return NotFound();
            }
            ViewData["carID"] = new SelectList(_context.Cars, "carID", "carID", isInWash.carID);
            ViewData["personID"] = new SelectList(_context.Persons, "personID", "personID", isInWash.personID);
            ViewData["washID"] = new SelectList(_context.Washes, "washID", "washID", isInWash.washID);
            return View(isInWash);
        }

        // POST: IsInWashs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("isInWashID,carID,personID,washID,from,to")] IsInWash isInWash)
        {
            if (id != isInWash.isInWashID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(isInWash);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IsInWashExists(isInWash.isInWashID))
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
            ViewData["carID"] = new SelectList(_context.Cars, "carID", "carID", isInWash.carID);
            ViewData["personID"] = new SelectList(_context.Persons, "personID", "personID", isInWash.personID);
            ViewData["washID"] = new SelectList(_context.Washes, "washID", "washID", isInWash.washID);
            return View(isInWash);
        }

        // GET: IsInWashs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isInWash = await _context.IsInWashes
                .Include(i => i.Car)
                .Include(i => i.Person)
                .Include(i => i.Wash)
                .FirstOrDefaultAsync(m => m.isInWashID == id);
            if (isInWash == null)
            {
                return NotFound();
            }

            return View(isInWash);
        }

        // POST: IsInWashs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isInWash = await _context.IsInWashes.FindAsync(id);
            _context.IsInWashes.Remove(isInWash);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IsInWashExists(int id)
        {
            return _context.IsInWashes.Any(e => e.isInWashID == id);
        }
    }
}
