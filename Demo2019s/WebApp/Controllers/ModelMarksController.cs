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
    public class ModelMarksController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAppUnitOfWork _uow;


        public ModelMarksController(AppDbContext context, IAppUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        // GET: ModelMarks
        public async Task<IActionResult> Index()
        {
            return View(await _uow.ModelMarks.AllAsync());
        }

        // GET: ModelMarks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelMark = await _uow.ModelMarks.FindAsync(id);
            if (modelMark == null)
            {
                return NotFound();
            }

            return View(modelMark);
        }

        // GET: ModelMarks/Create
        public IActionResult Create()
        {
            var vm = new ModelMarkCreateEditViewModel();
            return View(vm);
        }

        // POST: ModelMarks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ModelMarkCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vm.ModelMark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: ModelMarks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vm = new ModelMarkCreateEditViewModel();

            vm.ModelMark = await _uow.ModelMarks.FindAsync(id);
            if (vm.ModelMark == null)
            {
                return NotFound();
            }
            return View(vm);
        }

        // POST: ModelMarks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, ModelMarkCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.ModelMarks.Update(vm.ModelMark);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModelMarkExists(vm.ModelMark.Id))
                    {
                        return NotFound();
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: ModelMarks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelMark = await _uow.ModelMarks.FindAsync(id);
            if (modelMark == null)
            {
                return NotFound();
            }

            return View(modelMark);
        }

        // POST: ModelMarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _uow.ModelMarks.Remove(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));;
        }

        private bool ModelMarkExists(Guid id)
        {
            return _context.ModelMarks.Any(e => e.Id == id);
        }
    }
}
