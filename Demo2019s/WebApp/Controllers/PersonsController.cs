using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using DAL.App.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain;
using Domain.Identity;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;
using Person = DAL.App.DTO.Person;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PersonsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public PersonsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Persons
        public async Task<IActionResult> Index()
        {
            
            var persons = await _uow.Persons.AllAsync(User.UserGuidId());

            return View(persons);
        }

        // GET: Persons/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _uow.Persons.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: Persons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Persons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DAL.App.DTO.Person person)
        {
            person.AppUserId = User.UserGuidId();

            if (ModelState.IsValid)
            {
                _uow.Persons.Add(person);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(person);

        }

        // GET: Persons/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _uow.Persons.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (person == null)
            {
                return NotFound();
            }

            return View(person);

        }

        // POST: Persons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,  Person person)
        {
            person.AppUserId = User.UserGuidId();

            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Persons.Update(person);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _uow.Persons.ExistsAsync(person.Id, User.UserGuidId()))
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

            return View(person);

        }

        // GET: Persons/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var person = await _uow.Persons.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Persons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Persons.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
