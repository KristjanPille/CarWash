using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize(Roles = "User")]
    public class PersonCarsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public PersonCarsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: personTypes
        public async Task<IActionResult> Index()
        {
            var personCars = await _uow.PersonCars.AllAsync(User.UserGuidId());
            return View(personCars);

        }

        // GET: personTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personCar = await _uow.PersonCars.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (personCar == null)
            {
                return NotFound();
            }

            return View(personCar);
        }

        // GET: personTypes/Create
        public async Task<IActionResult> Create()
        {
            var vm = new PersonCarCreateEditViewModel();
            
            vm.Car = new SelectList(await _uow.Cars.AllAsync(User.UserGuidId()), nameof(Car.Id),
                nameof(Car.LicenceNr));

            return View(vm);

        }

        // POST: personTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonCarCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.PersonCars.Add(vm.PersonCar);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.Car = new SelectList(await _uow.Cars.AllAsync(User.UserGuidId()), nameof(Car.Id),
                nameof(Car.LicenceNr));
            return View(vm);
        }

        // GET: personTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vm = new PersonCarCreateEditViewModel();

            vm.PersonCar = await _uow.PersonCars.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (vm.PersonCar == null)
            {
                return NotFound();
            }
            vm.Car = new SelectList(await _uow.Cars.AllAsync(User.UserGuidId()), nameof(Car.Id),
                nameof(Car.LicenceNr));
            return View(vm);
        }

        // POST: personTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PersonCarCreateEditViewModel vm)
        {
            if (id != vm.PersonCar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.PersonCars.Update(vm.PersonCar);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.PersonCars.ExistsAsync(id, User.UserGuidId()))
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
            vm.Car = new SelectList(await _uow.Cars.AllAsync(User.UserGuidId()), nameof(Car.Id),
                nameof(Car.LicenceNr));
            return View(vm);
        }

        // GET: personTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personCar = await _uow.PersonCars.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (personCar == null)
            {
                return NotFound();
            }

            return View(personCar);
        }

        // POST: personTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.PersonCars.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
