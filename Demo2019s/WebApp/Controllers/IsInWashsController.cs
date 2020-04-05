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
    public class IsInWashsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAppUnitOfWork _uow;

        public IsInWashsController(AppDbContext context, IAppUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        // GET: IsInWashs
        public async Task<IActionResult> Index()
        {
            var isInWashes = await _uow.IsInWashes.AllAsync(User.UserGuidId());

            return View(isInWashes);
        }

        // GET: IsInWashs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isInWash = await _uow.IsInWashes.FindAsync(id);
            if (isInWash == null)
            {
                return NotFound();
            }

            return View(isInWash);
        }

        // GET: IsInWashs/Create
        public IActionResult Create()
        {
            var vm = new IsInWashCreateEditViewModel();
            vm.CarSelectList = new SelectList(
                _context.Set<Car>(),
                nameof(Car.Id), nameof(Car.Id));
            // vm.PersonSelectList = new SelectList(
            //    _context.Set<Person>(),
            //    nameof(Person.Id), nameof(Person.AppUserId));
            vm.WashSelectList = new SelectList(
                _context.Set<Wash>(),
                nameof(Wash.Id), nameof(Wash.NameOfWashType));
            return View(vm);
        }

        // POST: IsInWashs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IsInWashCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "CarId", vm.IsInWash.CarId);
            ViewData["PersonId"] = new SelectList(_context.Set<Person>(), "Id", "AppUserId", vm.IsInWash.PersonId);
            ViewData["WashId"] = new SelectList(_context.Set<Wash>(), "WashId", "NameOfWashType", vm.IsInWash.WashId);
            return View(vm);
        }

        // GET: IsInWashs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isInWash = await _uow.IsInWashes.FindAsync(id);
            if (isInWash == null)
            {
                return NotFound();
            }
            //ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "CarId", isInWash.CarId);
            //ViewData["PersonId"] = new SelectList(_context.Set<Person>(), "Id", "AppUserId", isInWash.PersonId);
            //ViewData["WashId"] = new SelectList(_context.Set<Wash>(), "WashId", "NameOfWashType", isInWash.WashId);
            return View(isInWash);
        }

        // POST: IsInWashs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IsInWashId,CarId,PersonId,WashId,From,To")] IsInWash isInWash)
        {
            if (id != isInWash.IsInWashId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.IsInWashes.Update(isInWash);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IsInWashExists(isInWash.IsInWashId))
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
            //ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "CarId", isInWash.CarId);
            //ViewData["PersonId"] = new SelectList(_context.Set<Person>(), "Id", "AppUserId", isInWash.PersonId);
            //ViewData["WashId"] = new SelectList(_context.Set<Wash>(), "WashId", "NameOfWashType", isInWash.WashId);
            return View(isInWash);
        }

        // GET: IsInWashs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isInWash = await _uow.IsInWashes.FindAsync(id);
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
            var isInWash = _uow.IsInWashes.Remove(id);
            await _uow.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool IsInWashExists(int id)
        {
            return _context.IsInWashes.Any(e => e.IsInWashId == id);
        }
    }
}
