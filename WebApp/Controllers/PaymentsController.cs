using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using WebApp.Data;

namespace WebApp.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Payments.Include(p => p.Check).Include(p => p.PaymentMethod).Include(p => p.Person);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Check)
                .Include(p => p.PaymentMethod)
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.paymentID == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            ViewData["checkID"] = new SelectList(_context.Checks, "checkID", "checkID");
            ViewData["paymentMethodID"] = new SelectList(_context.PaymentMethods, "paymentMethodID", "paymentMethodID");
            ViewData["personID"] = new SelectList(_context.Persons, "personID", "personID");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("paymentID,personID,paymentMethodID,checkID,paymentAmount,timeOfPayment")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["checkID"] = new SelectList(_context.Checks, "checkID", "checkID", payment.checkID);
            ViewData["paymentMethodID"] = new SelectList(_context.PaymentMethods, "paymentMethodID", "paymentMethodID", payment.paymentMethodID);
            ViewData["personID"] = new SelectList(_context.Persons, "personID", "personID", payment.personID);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["checkID"] = new SelectList(_context.Checks, "checkID", "checkID", payment.checkID);
            ViewData["paymentMethodID"] = new SelectList(_context.PaymentMethods, "paymentMethodID", "paymentMethodID", payment.paymentMethodID);
            ViewData["personID"] = new SelectList(_context.Persons, "personID", "personID", payment.personID);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("paymentID,personID,paymentMethodID,checkID,paymentAmount,timeOfPayment")] Payment payment)
        {
            if (id != payment.paymentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.paymentID))
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
            ViewData["checkID"] = new SelectList(_context.Checks, "checkID", "checkID", payment.checkID);
            ViewData["paymentMethodID"] = new SelectList(_context.PaymentMethods, "paymentMethodID", "paymentMethodID", payment.paymentMethodID);
            ViewData["personID"] = new SelectList(_context.Persons, "personID", "personID", payment.personID);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Check)
                .Include(p => p.PaymentMethod)
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.paymentID == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.paymentID == id);
        }
    }
}
