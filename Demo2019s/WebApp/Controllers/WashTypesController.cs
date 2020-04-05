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
using Extensions;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class WashTypesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAppUnitOfWork _uow;

        public WashTypesController(AppDbContext context, IAppUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        // GET: WashTypes
        public async Task<IActionResult> Index()
        {
            var washTypes = await _uow.WashTypes.AllAsync(User.UserGuidId());

            return View(washTypes);
        }

        // GET: WashTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var washType = await _uow.WashTypes.FindAsync(id);
            if (washType == null)
            {
                return NotFound();
            }

            return View(washType);
        }
        // GET: WashTypes/Create
        public IActionResult Create()
        {
            var vm = new WashTypeCreateEditViewModel();
            return View(vm);
        }

        // POST: WashTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WashTypeCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.WashTypes.Add(vm.WashType);
                await _uow.SaveChangesAsync();
            
                return RedirectToAction(nameof(Index));
            }
     
            return View(vm);
        }

        // GET: WashTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vm = new WashTypeCreateEditViewModel();

            vm.WashType = await _uow.WashTypes.FindAsync(id);
            if (vm.WashType == null)
            {
                return NotFound();
            }
            return View(vm);
        }

        // POST: WashTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, WashTypeCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.WashTypes.Update(vm.WashType);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WashTypeExists(vm.WashType.Id))
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
            return View(vm);
        }

        // GET: WashTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                
                return NotFound();
            }

            var washType = await _uow.WashTypes.FindAsync(id);
            if (washType == null)
            {
                return NotFound();
            }

            return View(washType);
        }

        // POST: WashTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _uow.WashTypes.Remove(id);
            await _uow.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool WashTypeExists(Guid id)
        {
            return _context.WashTypes.Any(e => e.Id == id);
        }
    }
}
