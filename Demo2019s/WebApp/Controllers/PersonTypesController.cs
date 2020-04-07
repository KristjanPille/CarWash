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
    public class PersonTypesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public PersonTypesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: personTypes
        public async Task<IActionResult> Index()
        {
            var personTypes = await _uow.PersonTypes.AllAsync(User.UserGuidId());
            return View(personTypes);

        }

        // GET: personTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personType = await _uow.PersonTypes.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (personType == null)
            {
                return NotFound();
            }

            return View(personType);
        }

        // GET: personTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: personTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonType personType)
        {
            if (ModelState.IsValid)
            {
                _uow.PersonTypes.Add(personType);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personType);

        }

        // GET: personTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personType = await _uow.PersonTypes.FindAsync(id);
            if (personType == null)
            {
                return NotFound();
            }
            return View(personType);

        }

        // POST: personTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PersonType personType)
        {
            if (id != personType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.PersonTypes.Update(personType);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.PersonTypes.ExistsAsync(id))
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
            return View(personType);

        }

        // GET: personTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personType = await _uow.PersonTypes.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (personType == null)
            {
                return NotFound();
            }

            return View(personType);

        }

        // POST: personTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.PersonTypes.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
