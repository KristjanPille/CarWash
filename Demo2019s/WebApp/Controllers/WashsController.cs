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
    public class WashsController : Controller
    {
        private readonly AppDbContext _context;

        public WashsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Washs
        public async Task<IActionResult> Index()
        {
            var AppDbContext = _context.Washes.Include(w => w.Order);
            return View(await AppDbContext.ToListAsync());
        }

        // GET: Washs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wash = await _context.Washes
                .Include(w => w.Order)
                .FirstOrDefaultAsync(m => m.WashId == id);
            if (wash == null)
            {
                return NotFound();
            }

            return View(wash);
        }

        // GET: Washs/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Set<Order>(), "OderId", "Comment");
            return View();
        }

        // POST: Washs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WashId,CheckId,WashTypeId,OrderId,NameOfWashType")] Wash wash)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wash);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Set<Order>(), "OderId", "Comment", wash.OrderId);
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
            ViewData["OrderId"] = new SelectList(_context.Set<Order>(), "OderId", "Comment", wash.OrderId);
            return View(wash);
        }

        // POST: Washs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WashId,CheckId,WashTypeId,OrderId,NameOfWashType")] Wash wash)
        {
            if (id != wash.WashId)
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
                    if (!WashExists(wash.WashId))
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
            ViewData["OrderId"] = new SelectList(_context.Set<Order>(), "OderId", "Comment", wash.OrderId);
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
                .Include(w => w.Order)
                .FirstOrDefaultAsync(m => m.WashId == id);
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
            return _context.Washes.Any(e => e.WashId == id);
        }
    }
}
