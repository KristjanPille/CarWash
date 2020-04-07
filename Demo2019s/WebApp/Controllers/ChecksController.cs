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
    public class ChecksController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ChecksController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: checks
        public async Task<IActionResult> Index()
        {
            var checks = await _uow.Checks.AllAsync(User.UserGuidId());
            return View(checks);

        }

        // GET: checks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var check = await _uow.Checks.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (check == null)
            {
                return NotFound();
            }

            return View(check);
        }

        // GET: checks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: checks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Check check)
        {

            if (ModelState.IsValid)
            {
                _uow.Checks.Add(check);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(check);

        }

        // GET: checks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var check = await _uow.Checks.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (check == null)
            {
                return NotFound();
            }
            return View(check);

        }

        // POST: checks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Check check)
        {
            if (id != check.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Checks.Update(check);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.Checks.ExistsAsync(id, User.UserGuidId()))
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
            return View(check);

        }

        // GET: checks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var check = await _uow.Checks.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (check == null)
            {
                return NotFound();
            }

            return View(check);

        }

        // POST: checks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Checks.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
