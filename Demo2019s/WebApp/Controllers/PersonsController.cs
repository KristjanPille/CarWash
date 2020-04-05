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

namespace WebApp.Controllers
{
    [Authorize]
    public class PersonsController : Controller
    {
        private readonly IAppUnitOfWork _uow;
        private readonly AppDbContext _context;

        public PersonsController(AppDbContext context, IAppUnitOfWork uow)
        {
            _context = context;
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

            var person = await _uow.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: Persons/Create
        public IActionResult Create()
        {
            var vm = new PersonCreateEditViewModel();
            vm.PersonTypeSelectList = new SelectList(
                _context.Set<PersonType>(),
                nameof(PersonType.PersonTypeId), nameof(PersonType.Name));
            vm.AppUserSelectList = new SelectList(
                _context.Set<AppUser>(),
                nameof(AppUser.Id), nameof(AppUser.Id));
            return View(vm);
        }

        // POST: Persons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonCreateEditViewModel vm)
        {
            vm.Person.AppUserId = User.UserGuidId();
            if (ModelState.IsValid)
            {
                _uow.Persons.Add(vm.Person);
                await _uow.SaveChangesAsync();
            
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Persons/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vm = new PersonCreateEditViewModel();

            vm.Person = await _uow.Persons.FindAsync(id);
            if (vm.Person == null)
            {
                return NotFound();
            }
            vm.PersonTypeSelectList = new SelectList(
                _context.Set<PersonType>(),
                nameof(PersonType.PersonTypeId), nameof(PersonType.Name));
            vm.AppUserSelectList = new SelectList(
                _context.Set<AppUser>(),
                nameof(AppUser.Id), nameof(AppUser.Id));
            return View(vm);
        }

        // POST: Persons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id,  PersonCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Persons.Update(vm.Person);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(vm.Person.Id))
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
            vm.PersonTypeSelectList = new SelectList(
                _context.Set<PersonType>(),
                nameof(PersonType.PersonTypeId), nameof(PersonType.Name));
            vm.AppUserSelectList = new SelectList(
                _context.Set<AppUser>(),
                nameof(AppUser.Id), nameof(AppUser.Id));
            return View(vm);
        }

        // GET: Persons/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _uow.Persons.FindAsync(id);
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
            _uow.Persons.Remove(id);
            await _uow.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(Guid id)
        {
            return _context.Persons.Any(e => e.Id == id);
        }
    }
}
