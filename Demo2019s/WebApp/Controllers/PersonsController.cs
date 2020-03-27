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
            var AppDbContext =
                _context.Persons
                    .Include(p => p.AppUser)
                    .Include(p => p.PersonType)
                    .Where(a => a.AppUserId == User.UserId());
            return View(await _uow.Persons.AllAsync());
        }

        // GET: Persons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .Include(p => p.AppUser)
                .Include(p => p.PersonType)
                .FirstOrDefaultAsync(m => m.PersonId == id);
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
                nameof(AppUser.Id), nameof(PersonType.Name));
            return View(vm);
           
            //ViewData["AppUserId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id");
        }

        // POST: Persons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonCreateEditViewModel vm)
        {
            vm.Person.AppUserId = User.UserId();
            
            if (ModelState.IsValid)
            {
                _uow.Persons.Add(vm.Person);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonTypeId"] = new SelectList(_context.Set<PersonType>(), "PersonTypeId", "Name", vm.Person.PersonTypeId);
            return View(vm);
        }

        // GET: Persons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .Include(p => p.AppUser)
                .Include(p => p.PersonType)
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            ViewData["AppUserId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", person.AppUserId);
            ViewData["PersonTypeId"] = new SelectList(_context.Set<PersonType>(), "PersonTypeId", "Name", person.PersonTypeId);
            return View(person);
        }

        // POST: Persons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var person = await _context.Persons
                .Include(p => p.AppUser)
                .Include(p => p.PersonType)
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (id != person.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PersonId))
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
            ViewData["AppUserId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", person.AppUserId);
            ViewData["PersonTypeId"] = new SelectList(_context.Set<PersonType>(), "PersonTypeId", "Name", person.PersonTypeId);
            return View(person);
        }

        // GET: Persons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .Include(p => p.AppUser)
                .Include(p => p.PersonType)
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Persons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            _uow.Persons.Remove(person);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.Persons.Any(e => e.PersonId == id);
        }
    }
}
