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
using Wash = DAL.App.DTO.Wash;

namespace WebApp.Controllers
{
    public class WashsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public WashsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Washes
          public async Task<IActionResult> Index()
          {
              var washes = await _uow.Washes.AllAsync(User.UserGuidId());
              return View(washes);
          }

        // GET: Washs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wash = await _uow.Washes.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (wash == null)
            {
                return NotFound();
            }

            return View(wash);
        }

        // GET: Washs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Washs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DAL.App.DTO.Wash wash)
        {
            if (ModelState.IsValid)
            {
                _uow.Washes.Add(wash);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wash);
        }

        // GET: Washs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wash = await _uow.Washes.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (wash == null)
            {
                return NotFound();
            }
            return View(wash);
        }

        // POST: Washs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,  Wash wash)
        {
            if (id != wash.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Washes.Update(wash);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.Washes.ExistsAsync(id, User.UserGuidId()))
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
            return View(wash);
        }

        // GET: Washs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wash = await _uow.Washes.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (wash == null)
            {
                return NotFound();
            }

            return View(wash);
        }

        // POST: Washs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Washes.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
