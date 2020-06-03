using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    public class CarsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAppBLL _bll;

        public CarsController(AppDbContext context, IAppBLL bll)
        {
            _context = context;
            _bll = bll;
        }
        [AllowAnonymous]
        // GET: Cars
        public async Task<IActionResult> Index()
        {
            var result = await _bll.Cars.GetAllAsync();
            return View(result);
        }
        [AllowAnonymous]
        // GET: Cars/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.AppUser)
                .Include(c => c.ModelMark)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }
        // GET: Cars/Create
        public async Task<IActionResult> Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["ModelMarkId"] = new SelectList(_context.ModelMarks, "Id", "Mark");
            ViewData["ModelMarkId"] = new SelectList(_context.ModelMarks, "Id", "Model");
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("ModelMarkId,CarSize,NameId,AppUserId,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] Car car)
        {
            if (ModelState.IsValid)
            {
                car.Id = Guid.NewGuid();
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", car.AppUserId);
            ViewData["ModelMarkId"] = new SelectList(_context.ModelMarks, "Id", "Mark", car.ModelMarkId);
            return View(car);
        }
        [Authorize(Roles = "admin")]
        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", car.AppUserId);
            ViewData["ModelMarkId"] = new SelectList(_context.ModelMarks, "Id", "Mark", car.ModelMarkId);
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(Guid id, [Bind("ModelMarkId,CarSize,NameId,AppUserId,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", car.AppUserId);
            ViewData["ModelMarkId"] = new SelectList(_context.ModelMarks, "Id", "Mark", car.ModelMarkId);
            return View(car);
        }
[Authorize(Roles = "admin")]
        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.AppUser)
                .Include(c => c.ModelMark)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }
        [Authorize(Roles = "admin")]
        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var car = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(Guid id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
