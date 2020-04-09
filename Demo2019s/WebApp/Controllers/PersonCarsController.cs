﻿using System;
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
            vm.CarSelectList = new SelectList(await _uow.Cars.AllAsync(User.UserGuidId()), nameof(Car.Id),
                nameof(Car.LicenceNr));
            vm.PersonSelectList = new SelectList(await _uow.Persons.AllAsync(User.UserGuidId()), nameof(Person.Id),
                nameof(Person.FirstLastName));
            return View(vm);
        }

        // POST: personCars/Create
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
            vm.CarSelectList = new SelectList(await _uow.Cars.AllAsync(User.UserGuidId()), nameof(Car.Id),
                nameof(Car.LicenceNr), vm.PersonCar.CarId);
            vm.PersonSelectList = new SelectList(await _uow.Persons.AllAsync(User.UserGuidId()), nameof(Person.Id),
                nameof(Person.FirstLastName), vm.PersonCar.PersonId);
            
            return View(vm);
        }

        // GET: personTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personCar = await _uow.PersonCars.FindAsync(id);
            if (personCar == null)
            {
                return NotFound();
            }
            return View(personCar);

        }

        // POST: personTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PersonCar personCar)
        {
            if (id != personCar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.PersonCars.Update(personCar);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.PersonCars.ExistsAsync(id))
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
            return View(personCar);

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