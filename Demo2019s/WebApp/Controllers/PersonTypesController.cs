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
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class PersonTypesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAppUnitOfWork _uow;

        public PersonTypesController(AppDbContext context, IAppUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        // GET: PersonTypes
        public async Task<IActionResult> Index()
        {
            return View(await _uow.PersonTypes.AllAsync());
        }

        // GET: PersonTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
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

        // GET: PersonTypes/Create
        public IActionResult Create()
        {
            var vm = new PersonTypeCreateEditViewModel();
            return View(vm);
        }

        // POST: PersonTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonTypeCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.PersonTypes.Add(vm.PersonType);
                await _uow.SaveChangesAsync();
            
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: PersonTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vm = new PersonTypeCreateEditViewModel();

            vm.PersonType = await _uow.PersonTypes.FindAsync(id);
            if (vm.PersonType == null)
            {
                return NotFound();
            }
            return View(vm);
        }

        // POST: PersonTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, PersonTypeCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.PersonTypes.Update(vm.PersonType);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonTypeExists(vm.PersonType.Id))
                    {
                        return NotFound();
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: PersonTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
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

        // POST: PersonTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _uow.PersonTypes.Remove(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonTypeExists(Guid id)
        {
            return _context.PersonTypes.Any(e => e.Id == id);
        }
    }
}
