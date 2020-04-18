using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;
using Payment = DAL.App.DTO.Payment;

namespace WebApp.Controllers
{
    [Authorize(Roles = "User")]
    public class PaymentsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public PaymentsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: payments
        public async Task<IActionResult> Index()
        {
            var payments = await _uow.Payments.AllAsync(User.UserGuidId());
            return View(payments);

        }

        // GET: payments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _uow.Payments.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: payments/Create
        public async Task<IActionResult> Create()
        {
            var vm = new PaymentCreateEditViewModel();
            
            vm.PersonSelectList = new SelectList(await _uow.Persons.AllAsync(User.UserGuidId()), nameof(Person.Id),
                nameof(Person.FirstName));
            vm.CheckSelectList = new SelectList(await _uow.Checks.AllAsync(User.UserGuidId()), nameof(Check.Id),
                nameof(Check.Wash.NameOfWashType));
            vm.PaymentMethodSelectList = new SelectList(await _uow.PaymentMethods.AllAsync(User.UserGuidId()), nameof(PaymentMethod.PaymentMethodName),
                nameof(PaymentMethod.PaymentMethodName));
            return View(vm);

        }

        // POST: payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.Payments.Add(vm.Payment);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.PersonSelectList = new SelectList(await _uow.Persons.AllAsync(User.UserGuidId()), nameof(Person.Id),
                nameof(Person.FirstName));
            vm.CheckSelectList = new SelectList(await _uow.Checks.AllAsync(User.UserGuidId()), nameof(Check.Id),
                nameof(Check.Wash.NameOfWashType));
            vm.PaymentMethodSelectList = new SelectList(await _uow.PaymentMethods.AllAsync(User.UserGuidId()), nameof(PaymentMethod.Id),
                nameof(PaymentMethod.PaymentMethodName));

            return View(vm);
        }

        // GET: payments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _uow.Payments.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);

        }

        // POST: payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Payment payment)
        {
            if (id != payment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Payments.Update(payment);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.Payments.ExistsAsync(id, User.UserGuidId()))
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
            return View(payment);

        }

        // GET: payments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _uow.Payments.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);

        }

        // POST: payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Payments.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
