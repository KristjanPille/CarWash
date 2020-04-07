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
        private readonly IAppUnitOfWork _uow;

        public IsInWashsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: IsInWashs
        public async Task<IActionResult> Index()
        {
            var isInWashes = await _uow.IsInWashes.AllAsync(User.UserGuidId());
            return View(isInWashes);
        }

        // GET: IsInWashs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isInWashes = await _uow.IsInWashes.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (isInWashes == null)
            {
                return NotFound();
            }

            return View(isInWashes);
        }

        // GET: IsInWashs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IsInWashs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IsInWash isInWash)
        {
            if (ModelState.IsValid)
            {
                _uow.IsInWashes.Add(isInWash);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(isInWash);
        }

        // GET: IsInWashs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isInWash = await _uow.IsInWashes.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (isInWash == null)
            {
                return NotFound();
            }
            return View(isInWash);
        }

        // POST: IsInWashs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, IsInWash isInWash)
        {
            if (id != isInWash.Id)
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
                    if (!await _uow.IsInWashes.ExistsAsync(id, User.UserGuidId()))
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
            return View(isInWash);
        }

        // GET: IsInWashs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isInWash = await _uow.IsInWashes.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (isInWash == null)
            {
                return NotFound();
            }

            return View(isInWash);
        }

        // POST: IsInWashs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.IsInWashes.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
