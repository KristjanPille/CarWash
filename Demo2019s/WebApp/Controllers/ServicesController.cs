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
    public class ServicesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAppUnitOfWork _uow;

        public ServicesController(AppDbContext context, IAppUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        // GET: Services
        public async Task<IActionResult> Index()
        {
            var services = await _uow.Services.AllAsync(User.UserGuidId());

            return View(services);
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _uow.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Services/Create
        public IActionResult Create()
        {
            var vm = new ServiceCreateEditViewModel();
            return View(vm);
        }

        // POST: Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.Services.Add(vm.Service);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vm = new ServiceCreateEditViewModel();

            vm.Service = await _uow.Services.FindAsync(id);
            if (vm.Service == null)
            {
                return NotFound();
            }
            return View(vm);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, ServiceCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Services.Update(vm.Service);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(vm.Service.Id))
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

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                
                return NotFound();
            }

            var service = await _uow.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _uow.Services.Remove(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(Guid id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}
