using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
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
        private readonly IAppUnitOfWork _uow;
        public DiscountsController(AppDbContext context, IAppUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        // GET: Discounts
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Discounts.AllAsync());
        }

        // GET: Discounts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var discount = await _uow.Discounts.FindAsync(id);
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
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vm = new DiscountCreateEditViewModel();

            vm.Discount = await _uow.Discounts.FindAsync(id);
            if (vm.Discount == null)
            {
                return NotFound();
            }
            vm.CheckSelectList = new SelectList(
                _context.Set<Check>(),
                nameof(Check.CheckId), nameof(Check.Comment));
            vm.WashSelectList = new SelectList(
                _context.Set<Wash>(),
                nameof(Wash.Id), nameof(Wash.NameOfWashType));
            return View(vm);
        }

        // POST: Discounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, DiscountCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Discounts.Update(vm.Discount);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscountExists(vm.Discount.Id))
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            vm.CheckSelectList = new SelectList(
                _context.Set<Check>(),
                nameof(Check.CheckId), nameof(Check.Comment));
            vm.WashSelectList = new SelectList(
                _context.Set<Wash>(),
                nameof(Wash.Id), nameof(Wash.NameOfWashType));
            return View(vm);
        }

        // GET: Discounts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discount = await _uow.Discounts.FindAsync(id);
            if (discount == null)
            {
                return NotFound();
            }

            return View(discount);
        }

        // POST: Discounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _uow.Discounts.Remove(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscountExists(Guid id)
        {
            return _context.Discounts.Any(e => e.Id == id);
        }
    }
}
