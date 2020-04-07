using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult Create()
        {
            return View();
        }

        // POST: cars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Car car)
        {

            if (ModelState.IsValid)
            {
                _uow.Cars.Add(car);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(car);

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
