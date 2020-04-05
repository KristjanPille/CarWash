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
            var cars = await _uow.Cars.AllAsync(User.UserGuidId());

            return View(cars);
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(Guid? id)
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

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            var vm = new CarCreateEditViewModel();
            vm.CarTypeSelectList = new SelectList(
                _context.Set<CarType>(),
                nameof(CarType.CarTypeId), nameof(CarType.Name));
            vm.ModelMarkSelectList = new SelectList(
                    _context.Set<ModelMark>(),
                    nameof(ModelMark.ModelMarkId), nameof(ModelMark.Mark));
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
            return View(vm);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vm = new CarCreateEditViewModel();

            vm.Car = await _uow.Cars.FindAsync(id);
            if (vm.Car == null)
            {
                return NotFound();
            }
            vm.CarTypeSelectList = new SelectList(
                _context.Set<CarType>(),
                nameof(CarType.CarTypeId), nameof(CarType.Name));
            vm.ModelMarkSelectList = new SelectList(
                _context.Set<ModelMark>(),
                nameof(ModelMark.ModelMarkId), nameof(ModelMark.Mark));

            return View(vm);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, CarCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Cars.Update(vm.Car);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(vm.Car.Id))
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
            vm.CarTypeSelectList = new SelectList(
                _context.Set<CarType>(),
                nameof(CarType.CarTypeId), nameof(CarType.Name));
            vm.ModelMarkSelectList = new SelectList(
                _context.Set<ModelMark>(),
                nameof(ModelMark.ModelMarkId), nameof(ModelMark.Mark));
            return View(vm);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
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

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _uow.Cars.Remove(id);
            await _uow.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(Guid id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
