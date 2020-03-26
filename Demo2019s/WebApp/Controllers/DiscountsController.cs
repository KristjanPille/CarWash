using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class DiscountsController : Controller
    {
        private readonly AppDbContext _context;

        public DiscountsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Discounts
        public async Task<IActionResult> Index()
        {
            var AppDbContext = _context.Discounts.Include(d => d.Check).Include(d => d.Wash);
            return View(await AppDbContext.ToListAsync());
        }

        // GET: Discounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discount = await _context.Discounts
                .Include(d => d.Check)
                .Include(d => d.Wash)
                .FirstOrDefaultAsync(m => m.DiscountId == id);
            if (discount == null)
            {
                return NotFound();
            }

            return View(discount);
        }

        // GET: Discounts/Create
        public IActionResult Create()
        {
            var vm = new DiscountCreateEditViewModel();
            vm.CheckSelectList = new SelectList(
                _context.Set<Check>(),
                nameof(Check.CheckId), nameof(Check.Comment));
            vm.WashSelectList = new SelectList(
                _context.Set<Wash>(),
                nameof(Wash.Id), nameof(Wash.NameOfWashType)
            );
            return View(vm);
        }

        // POST: Discounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DiscountCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CheckId"] = new SelectList(_context.Checks, "CheckId", "Comment", vm.Discount.CheckId);
            ViewData["WashId"] = new SelectList(_context.Set<Wash>(), "WashId", "NameOfWashType", vm.Discount.WashId);
            return View(vm);
        }

        // GET: Discounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discount = await _context.Discounts.FindAsync(id);
            if (discount == null)
            {
                return NotFound();
            }
            ViewData["CheckId"] = new SelectList(_context.Checks, "CheckId", "Comment", discount.CheckId);
            ViewData["WashId"] = new SelectList(_context.Set<Wash>(), "WashId", "NameOfWashType", discount.WashId);
            return View(discount);
        }

        // POST: Discounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiscountId,CheckId,WashId")] Discount discount)
        {
            if (id != discount.DiscountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscountExists(discount.DiscountId))
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
            ViewData["CheckId"] = new SelectList(_context.Checks, "CheckId", "Comment", discount.CheckId);
            ViewData["WashId"] = new SelectList(_context.Set<Wash>(), "WashId", "NameOfWashType", discount.WashId);
            return View(discount);
        }

        // GET: Discounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discount = await _context.Discounts
                .Include(d => d.Check)
                .Include(d => d.Wash)
                .FirstOrDefaultAsync(m => m.DiscountId == id);
            if (discount == null)
            {
                return NotFound();
            }

            return View(discount);
        }

        // POST: Discounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var discount = await _context.Discounts.FindAsync(id);
            _context.Discounts.Remove(discount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscountExists(int id)
        {
            return _context.Discounts.Any(e => e.DiscountId == id);
        }
    }
}
