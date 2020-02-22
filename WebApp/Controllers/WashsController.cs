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
    public class WashsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WashsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Washs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Washes.Include(w => w.order);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Washs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wash = await _context.Washes
                .Include(w => w.order)
                .FirstOrDefaultAsync(m => m.washID == id);
            if (wash == null)
            {
                return NotFound();
            }

            return View(wash);
        }

        // GET: Washs/Create
        public IActionResult Create()
        {
            ViewData["orderID"] = new SelectList(_context.Orders, "oderID", "oderID");
            return View();
        }

        // POST: Washs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("washID,checkID,washTypeID,orderID,nameOfWashType")] Wash wash)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wash);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["orderID"] = new SelectList(_context.Orders, "oderID", "oderID", wash.orderID);
            return View(wash);
        }

        // GET: Washs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wash = await _context.Washes.FindAsync(id);
            if (wash == null)
            {
                return NotFound();
            }
            ViewData["orderID"] = new SelectList(_context.Orders, "oderID", "oderID", wash.orderID);
            return View(wash);
        }

        // POST: Washs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("washID,checkID,washTypeID,orderID,nameOfWashType")] Wash wash)
        {
            if (id != wash.washID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wash);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WashExists(wash.washID))
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
            ViewData["orderID"] = new SelectList(_context.Orders, "oderID", "oderID", wash.orderID);
            return View(wash);
        }

        // GET: Washs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wash = await _context.Washes
                .Include(w => w.order)
                .FirstOrDefaultAsync(m => m.washID == id);
            if (wash == null)
            {
                return NotFound();
            }

            return View(wash);
        }

        // POST: Washs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wash = await _context.Washes.FindAsync(id);
            _context.Washes.Remove(wash);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WashExists(int id)
        {
            return _context.Washes.Any(e => e.washID == id);
        }
    }
}
