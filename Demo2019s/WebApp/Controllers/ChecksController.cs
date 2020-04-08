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
        public async Task<IActionResult> Create()
        {
            var vm = new CheckCreateEditViewModel();
            
            vm.PersonSelectList = new SelectList(await _uow.Persons.AllAsync(User.UserGuidId()), nameof(Person.Id),
                nameof(Person.FirstName));
            vm.WashSelectList = new SelectList(await _uow.Washes.AllAsync(User.UserGuidId()), nameof(Wash.Id),
                nameof(Wash.NameOfWashType));
            return View(vm);
        }

        // POST: checks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CheckCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.Checks.Add(vm.Check);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.PersonSelectList = new SelectList(await _uow.Persons.AllAsync(User.UserGuidId()), nameof(Person.Id),
                nameof(Person.FirstName));
            vm.WashSelectList = new SelectList(await _uow.Washes.AllAsync(User.UserGuidId()), nameof(Wash.Id),
                nameof(Wash.NameOfWashType));
            return View(vm);
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
