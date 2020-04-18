using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;
using ModelMark = DAL.App.DTO.ModelMark;

namespace WebApp.Controllers
{
    [Authorize(Roles = "User")]
    public class ModelMarksController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ModelMarksController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: modelMarks
        public async Task<IActionResult> Index()
        {
            var modelMarks = await _uow.ModelMarks.AllAsync(User.UserGuidId());
            return View(modelMarks);

        }

        // GET: modelMarks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelMark = await _uow.ModelMarks.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (modelMark == null)
            {
                return NotFound();
            }

            return View(modelMark);
        }

        // GET: modelMarks/Create
        public IActionResult Create()
        {
            var vm = new ModelMarkCreateEditViewModel();
            return View(vm);
        }

        // POST: modelMarks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ModelMarkCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.ModelMarks.Add(vm.ModelMark);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);

        }

        // GET: modelMarks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelMark = await _uow.ModelMarks.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (modelMark == null)
            {
                return NotFound();
            }
            return View(modelMark);

        }

        // POST: modelMarks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ModelMark modelMark)
        {
            if (id != modelMark.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.ModelMarks.Update(modelMark);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.ModelMarks.ExistsAsync(id, User.UserGuidId()))
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
            return View(modelMark);

        }

        // GET: modelMarks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelMark = await _uow.ModelMarks.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (modelMark == null)
            {
                return NotFound();
            }

            return View(modelMark);

        }

        // POST: modelMarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.ModelMarks.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
