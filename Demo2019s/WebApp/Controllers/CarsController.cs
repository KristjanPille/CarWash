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
    public class CarsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAppUnitOfWork _uow;
        public CarsController(AppDbContext context, IAppUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            //var AppDbContext = _context.Cars.Include(c => c.CarType).Include(c => c.Person);
            return View(await _uow.Cars.AllAsync());
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.CarType)
                .Include(c => c.Person)
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            var vm = new CarCreateEditViewModel();
            vm.CarTypeSelectList = new SelectList(
                _context.Set<CarType>(),
                nameof(CarType.CarTypeId), nameof(CarType.Name));
            vm.PersonSelectList = new SelectList(
                _context.Set<Person>(),
                nameof(Person.PersonId), nameof(Person.AppUserId)
            );
            return View(vm);
        }

        // POST: Cars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.Cars.Add(vm.Car);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarTypeId"] = new SelectList(_context.Set<CarType>(), "CarTypeId", "Name", vm.Car.CarTypeId);
            ViewData["PersonId"] = new SelectList(_context.Set<Person>(), "Id", "AppUserId", vm.Car.PersonId);
            return View(vm);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _uow.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["CarTypeId"] = new SelectList(_context.Set<CarType>(), "CarTypeId", "Name", car.CarTypeId);
            ViewData["PersonId"] = new SelectList(_context.Set<Person>(), "Id", "AppUserId", car.PersonId);
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarId,CarTypeId,PersonId,LicenceNr")] Car car)
        {
            if (id != car.CarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Cars.Update(car);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.CarId))
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
            ViewData["CarTypeId"] = new SelectList(_context.Set<CarType>(), "CarTypeId", "Name", car.CarTypeId);
            ViewData["PersonId"] = new SelectList(_context.Set<Person>(), "Id", "AppUserId", car.PersonId);
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.CarType)
                .Include(c => c.Person)
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _uow.Cars.FindAsync(id);
            _uow.Cars.Remove(car);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.CarId == id);
        }
    }
}
