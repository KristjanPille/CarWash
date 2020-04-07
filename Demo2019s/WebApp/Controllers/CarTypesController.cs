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
    public class CarTypesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public CarTypesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: carTypes
        public async Task<IActionResult> Index()
        {
            var carTypes = await _uow.CarTypes.AllAsync(User.UserGuidId());
            return View(carTypes);

        }

        // GET: carTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carType = await _uow.CarTypes.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (carType == null)
            {
                return NotFound();
            }

            return View(carType);
        }

        // GET: carTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: carTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarType carType)
        {

            if (ModelState.IsValid)
            {
                _uow.CarTypes.Add(carType);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carType);

        }

        // GET: carTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carType = await _uow.CarTypes.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (carType == null)
            {
                return NotFound();
            }
            return View(carType);

        }

        // POST: carTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CarType carType)
        {
            if (id != carType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.CarTypes.Update(carType);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.CarTypes.ExistsAsync(id, User.UserGuidId()))
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
            return View(carType);

        }

        // GET: carTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carType = await _uow.CarTypes.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (carType == null)
            {
                return NotFound();
            }

            return View(carType);

        }

        // POST: carTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.CarTypes.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
