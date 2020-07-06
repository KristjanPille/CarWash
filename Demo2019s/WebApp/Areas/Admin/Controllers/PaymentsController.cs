using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PaymentsController : Controller
    {
        private readonly AppDbContext _context;

        public PaymentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Payments
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Payments.Include(p => p.AppUser).Include(p => p.Check).Include(p => p.PaymentMethod);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Payments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.AppUser)
                .Include(p => p.Check)
                .Include(p => p.PaymentMethod)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Admin/Payments/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["CheckId"] = new SelectList(_context.Checks, "Id", "Comment");
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "Id", "PaymentMethodName");
            return View();
        }

        // POST: Admin/Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentMethodId,CheckId,PaymentAmount,TimeOfPayment,AppUserId,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                payment.Id = Guid.NewGuid();
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", payment.AppUserId);
            ViewData["CheckId"] = new SelectList(_context.Checks, "Id", "Comment", payment.CheckId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "Id", "PaymentMethodName", payment.PaymentMethodId);
            return View(payment);
        }

        // GET: Admin/Payments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", payment.AppUserId);
            ViewData["CheckId"] = new SelectList(_context.Checks, "Id", "Comment", payment.CheckId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "Id", "PaymentMethodName", payment.PaymentMethodId);
            return View(payment);
        }

        // POST: Admin/Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PaymentMethodId,CheckId,PaymentAmount,TimeOfPayment,AppUserId,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] Payment payment)
        {
            if (id != payment.Id)
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
                    if (!PaymentExists(payment.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", payment.AppUserId);
            ViewData["CheckId"] = new SelectList(_context.Checks, "Id", "Comment", payment.CheckId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "Id", "PaymentMethodName", payment.PaymentMethodId);
            return View(payment);
        }

        // GET: Admin/Payments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.AppUser)
                .Include(p => p.Check)
                .Include(p => p.PaymentMethod)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Admin/Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var payment = await _context.Payments.FindAsync(id);
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(Guid id)
        {
            return _context.Payments.Any(e => e.Id == id);
        }
    }
}