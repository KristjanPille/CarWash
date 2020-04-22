using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
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
        
        private readonly IAppBLL _bll;

        public PersonsController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: Persons
        public async Task<IActionResult> Index()
        {
            
            var persons = await _bll.Persons.AllAsync(User.UserGuidId());

            return View(persons);
        }

        // GET: Persons/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _bll.Persons.FirstOrDefaultAsync(id.Value, User.UserGuidId());
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
        public async Task<IActionResult> Create(BLL.App.DTO.Person person)
        {
            person.AppUserId = User.UserGuidId();

            if (ModelState.IsValid)
            {
                _bll.Persons.Add(person);
                await _bll.SaveChangesAsync();
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

            var person = await _bll.Persons.FirstOrDefaultAsync(id.Value, User.UserGuidId());

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
        public async Task<IActionResult> Edit(Guid id,  BLL.App.DTO.Person person)
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
                    _bll.Persons.Update(person);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _bll.Persons.ExistsAsync(person.Id, User.UserGuidId()))
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
            var person = await _bll.Persons.FirstOrDefaultAsync(id.Value, User.UserGuidId());
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
            await _bll.Persons.DeleteAsync(id, User.UserGuidId());
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
