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
            return View(await _context.PersonTypes.ToListAsync());
        }

        // GET: PersonTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personType = await _context.PersonTypes
                .FirstOrDefaultAsync(m => m.PersonTypeId == id);
            if (personType == null)
            {
                return NotFound();
            }

            return View(personType);
        }

        // GET: PersonTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PersonTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonTypeId,Name")] PersonType personType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personType);
        }

        // GET: PersonTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personType = await _context.PersonTypes.FindAsync(id);
            if (personType == null)
            {
                return NotFound();
            }
            return View(personType);
        }

        // POST: PersonTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonTypeId,Name")] PersonType personType)
        {
            if (id != personType.PersonTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonTypeExists(personType.PersonTypeId))
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

        // GET: PersonTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personType = await _context.PersonTypes
                .FirstOrDefaultAsync(m => m.PersonTypeId == id);
            if (personType == null)
            {
                return NotFound();
            }

            return View(personType);
        }

        // POST: PersonTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personType = await _context.PersonTypes.FindAsync(id);
            _context.PersonTypes.Remove(personType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonTypeExists(int id)
        {
            return _context.PersonTypes.Any(e => e.PersonTypeId == id);
        }
    }
}
