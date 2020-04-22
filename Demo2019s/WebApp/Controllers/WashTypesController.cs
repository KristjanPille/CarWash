using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WashType = DAL.App.DTO.WashType;

namespace WebApp.Controllers
{
    [Authorize(Roles = "User")]
    public class WashTypesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public WashTypesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: washTypes
        public async Task<IActionResult> Index()
        {
            var washTypes = await _uow.WashTypes.AllAsync(User.UserGuidId());
            return View(washTypes);

        }

        // GET: washTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var washType = await _uow.WashTypes.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (washType == null)
            {
                return NotFound();
            }

            return View(washType);
        }

        // GET: washTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: washTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DAL.App.DTO.WashType washType)
        {

            if (ModelState.IsValid)
            {
                _uow.WashTypes.Add(washType);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(washType);

        }

        // GET: washTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var washType = await _uow.WashTypes.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (washType == null)
            {
                return NotFound();
            }
            return View(washType);

        }

        // POST: washTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, WashType washType)
        {
            if (id != washType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.WashTypes.Update(washType);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.WashTypes.ExistsAsync(id, User.UserGuidId()))
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
            return View(washType);
        }

        // GET: washTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var washType = await _uow.WashTypes.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (washType == null)
            {
                return NotFound();
            }

            return View(washType);

        }

        // POST: washTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.WashTypes.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
