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
    public class WashsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAppUnitOfWork _uow;

        public WashsController(AppDbContext context, IAppUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        // GET: Washs
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Washes.AllAsync());
        }

        // GET: Washs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wash = await _uow.Washes.FindAsync(id);
            if (wash == null)
            {
                return NotFound();
            }

            return View(wash);
        }

        // GET: Washs/Create
        public IActionResult Create()
        {
            var vm = new WashCreateEditViewModel();
            vm.OrderSelectList = new SelectList(
                _context.Set<Order>(),
                nameof(Order.Id), nameof(Order.Id));
            return View(vm);
        }

        // POST: Washs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WashCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.Washes.Add(vm.Wash);
                await _uow.SaveChangesAsync();
            
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Washs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vm = new WashCreateEditViewModel();

            vm.Wash = await _uow.Washes.FindAsync(id);
            if (vm.Wash == null)
            {
                return NotFound();
            }
            vm.OrderSelectList = new SelectList(
                _context.Set<Order>(),
                nameof(Order.Id), nameof(Order.Id));
            return View(vm);
        }

        // POST: Washs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id,  WashCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Washes.Update(vm.Wash);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WashExists(vm.Wash.Id))
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
            vm.OrderSelectList = new SelectList(
                _context.Set<Order>(),
                nameof(Order.Id), nameof(Order.Id));
            return View(vm);
        }

        // GET: Washs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                
                return NotFound();
            }

            var wash = await _uow.Washes.FindAsync(id);
            if (wash == null)
            {
                return NotFound();
            }

            return View(wash);
        }

        // POST: Washs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
             _uow.Washes.Remove(id);
            await _uow.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool WashExists(Guid id)
        {
            return _context.Washes.Any(e => e.Id == id);
        }
    }
}
