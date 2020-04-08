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
    public class CarsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public CarsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: cars
        public async Task<IActionResult> Index()
        {
            var cars = await _uow.Cars.AllAsync(User.UserGuidId());
            return View(cars);

        }

        // GET: cars/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _uow.Cars.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: cars/Create
        public async Task<IActionResult> Create()
        {
            var vm = new CarCreateEditViewModel();
            
            vm.CarTypeSelectList = new SelectList(await _uow.CarTypes.AllAsync(User.UserGuidId()), nameof(CarType.CarTypeId),
                nameof(CarType.Name));
                     
            vm.ModelMarkSelectList = new SelectList(await _uow.ModelMarks.AllAsync(User.UserGuidId()), nameof(ModelMark.ModelMarkId),
                nameof(ModelMark.Mark));
            return View(vm);
        }

        // POST: cars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarCreateEditViewModel vm)
        {
            vm.Car.AppUserId = User.UserGuidId();
            if (ModelState.IsValid)
            {
                _uow.Cars.Add(vm.Car);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.CarTypeSelectList = new SelectList(await _uow.CarTypes.AllAsync(User.UserGuidId()), nameof(CarType.CarTypeId),
                nameof(CarType.Name));
                                 
            vm.ModelMarkSelectList = new SelectList(await _uow.ModelMarks.AllAsync(User.UserGuidId()), nameof(ModelMark.ModelMarkId),
                nameof(ModelMark.Mark));
            return View(vm);
        }

        // GET: cars/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _uow.Cars.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (car == null)
            {
                return NotFound();
            }
            return View(car);

        }

        // POST: cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Car car)
        {
            if (id != car.Id)
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
                    if (!await _uow.Cars.ExistsAsync(id, User.UserGuidId()))
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
            return View(car);

        }

        // GET: cars/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _uow.Cars.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (car == null)
            {
                return NotFound();
            }

            return View(car);

        }

        // POST: cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Cars.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
