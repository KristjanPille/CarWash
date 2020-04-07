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
    public class DiscountsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public DiscountsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: discounts
        public async Task<IActionResult> Index()
        {
            var discounts = await _uow.Discounts.AllAsync(User.UserGuidId());
            return View(discounts);

        }

        // GET: discounts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discount = await _uow.Discounts.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (discount == null)
            {
                return NotFound();
            }

            return View(discount);
        }

        // GET: discounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: discounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Discount discount)
        {

            if (ModelState.IsValid)
            {
                _uow.Discounts.Add(discount);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(discount);

        }

        // GET: discounts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discount = await _uow.Discounts.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (discount == null)
            {
                return NotFound();
            }
            return View(discount);

        }

        // POST: discounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Discount discount)
        {
            if (id != discount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Discounts.Update(discount);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.Discounts.ExistsAsync(id, User.UserGuidId()))
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
            return View(discount);

        }

        // GET: discounts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discount = await _uow.Discounts.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (discount == null)
            {
                return NotFound();
            }

            return View(discount);

        }

        // POST: discounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Discounts.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
