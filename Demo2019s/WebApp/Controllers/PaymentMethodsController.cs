using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PaymentMethod = DAL.App.DTO.PaymentMethod;

namespace WebApp.Controllers
{
    [Authorize(Roles = "User")]
    public class PaymentMethodsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public PaymentMethodsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: paymentMethods
        public async Task<IActionResult> Index()
        {
            var paymentMethods = await _uow.PaymentMethods.AllAsync(User.UserGuidId());
            return View(paymentMethods);

        }

        // GET: paymentMethods/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentMethod = await _uow.PaymentMethods.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (paymentMethod == null)
            {
                return NotFound();
            }

            return View(paymentMethod);
        }

        // GET: paymentMethods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: paymentMethods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DAL.App.DTO.PaymentMethod paymentMethod)
        {

            if (ModelState.IsValid)
            {
                _uow.PaymentMethods.Add(paymentMethod);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paymentMethod);

        }

        // GET: paymentMethods/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentMethod = await _uow.PaymentMethods.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (paymentMethod == null)
            {
                return NotFound();
            }
            return View(paymentMethod);

        }

        // POST: paymentMethods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PaymentMethod paymentMethod)
        {
            if (id != paymentMethod.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.PaymentMethods.Update(paymentMethod);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.PaymentMethods.ExistsAsync(id, User.UserGuidId()))
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
            return View(paymentMethod);

        }

        // GET: paymentMethods/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentMethod = await _uow.PaymentMethods.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (paymentMethod == null)
            {
                return NotFound();
            }

            return View(paymentMethod);

        }

        // POST: paymentMethods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.PaymentMethods.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
