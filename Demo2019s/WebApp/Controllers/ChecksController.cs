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
    [Authorize]
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
            var appDbContext = _context.Checks.Include(c => c.AppUser).Include(c => c.Service);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Checks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var check = await _context.Checks
                .Include(c => c.AppUser)
                .Include(c => c.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (check == null)
            {
                return NotFound();
            }

            return View(check);
        }
        [Authorize(Roles = "admin")]
        // GET: Checks/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["NameId"] = new SelectList(_context.LangStrs, "Id", "Id");
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "NameOfService");
            return View();
        }

        // POST: Checks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("NameId,ServiceId,DateTimeCheck,AmountExcludeVat,Vat,Comment,AppUserId,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] Check check)
        {
            if (ModelState.IsValid)
            {
                check.Id = Guid.NewGuid();
                _context.Add(check);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", check.AppUserId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "NameOfService", check.ServiceId);
            return View(check);
        }
[Authorize(Roles = "admin")]
        // GET: Checks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", check.AppUserId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "NameOfService", check.ServiceId);
            return View(check);
        }

        // POST: Checks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(Guid id, [Bind("NameId,ServiceId,DateTimeCheck,AmountExcludeVat,Vat,Comment,AppUserId,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] Check check)
        {
            if (id != check.Id)
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
                    if (!CheckExists(check.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", check.AppUserId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "NameOfService", check.ServiceId);
            return View(check);
        }

        // GET: Checks/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var check = await _context.Checks
                .Include(c => c.AppUser)
                .Include(c => c.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (check == null)
            {
                return NotFound();
            }

            return View(check);
        }

        // POST: Checks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var check = await _context.Checks.FindAsync(id);
            _context.Checks.Remove(check);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "admin")]
        private bool CheckExists(Guid id)
        {
            return _context.Checks.Any(e => e.Id == id);
        }
    }
}
