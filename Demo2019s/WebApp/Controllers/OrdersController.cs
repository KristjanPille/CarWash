using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;
using Order = DAL.App.DTO.Order;

namespace WebApp.Controllers
{
    [Authorize(Roles = "User")]
    public class OrdersController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public OrdersController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: orders
        public async Task<IActionResult> Index()
        {
            var orders = await _uow.Orders.AllAsync(User.UserGuidId());
            return View(orders);

        }

        // GET: orders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _uow.Orders.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: orders/Create
        public IActionResult Create()
        {
            var vm = new OrderCreateEditViewModel();
            return View(vm);
        }

        // POST: orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.Orders.Add(vm.Order);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);

        }

        // GET: orders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _uow.Orders.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (order == null)
            {
                return NotFound();
            }
            return View(order);

        }

        // POST: orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Orders.Update(order);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.Orders.ExistsAsync(id, User.UserGuidId()))
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
            return View(order);

        }

        // GET: orders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _uow.Orders.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (order == null)
            {
                return NotFound();
            }

            return View(order);

        }

        // POST: orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Orders.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
