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
    public class ChecksController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAppUnitOfWork _uow;


        public ChecksController(AppDbContext context, IAppUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        // GET: Checks
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Checks.AllAsync());
        }

        // GET: Checks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var check = await _uow.Checks.FindAsync(id);
            if (check == null)
            {
                return NotFound();
            }
            return View(check);
        }

        // GET: Checks/Create
        public IActionResult Create()
        {
            var vm = new CheckCreateEditViewModel();
            vm.WashSelectList = new SelectList(
                _context.Set<Wash>(),
                nameof(Wash.Id), nameof(Wash.NameOfWashType));
            vm.PersonSelectList = new SelectList(
                _context.Set<Person>(),
                nameof(Person.Id), nameof(Person.Name));
            return View(vm);
        }

        // POST: Checks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CheckCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vm.Check);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["PersonId"] = new SelectList(_context.Set<Person>(), "Id", "AppUserId", vm.Check.PersonId);
            //ViewData["WashId"] = new SelectList(_context.Set<Wash>(), "WashId", "NameOfWashType", vm.Check.WashId);
            return View(vm);
        }

        // GET: Checks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vm = new CheckCreateEditViewModel();

            vm.Check = await _uow.Checks.FindAsync(id);
            if (vm.Check == null)
            {
                return NotFound();
            }
            vm.WashSelectList = new SelectList(
                _context.Set<Wash>(),
                nameof(Wash.Id), nameof(Wash.NameOfWashType));
            vm.PersonSelectList = new SelectList(
                _context.Set<Person>(),
                nameof(Person.Id), nameof(Person.Name));
            return View(vm);
        }

        // POST: Checks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, CheckCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Checks.Update(vm.Check);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CheckExists(vm.Check.Id))
                    {
                        return NotFound();
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            vm.WashSelectList = new SelectList(
                _context.Set<Wash>(),
                nameof(Wash.Id), nameof(Wash.NameOfWashType));
            vm.PersonSelectList = new SelectList(
                _context.Set<Person>(),
                nameof(Person.Id), nameof(Person.Name));
            return View(vm);
        }

        // GET: Checks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var check = await _uow.Checks.FindAsync(id);
            if (check == null)
            {
                return NotFound();
            }

            return View(check);
        }

        // POST: Checks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _uow.Checks.Remove(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CheckExists(Guid id)
        {
            return _context.Checks.Any(e => e.Id == id);
        }
    }
}
