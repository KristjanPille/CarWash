using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;
using Service = DAL.App.DTO.Service;

namespace WebApp.Controllers
{
    [Authorize(Roles = "User")]
    public class ServicesController : Controller
    {
        private readonly IAppBLL _bll;

        public ServicesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: services
        public async Task<IActionResult> Index()
        {
            var services = await _bll.Services.AllAsync(User.UserGuidId());

            return View(services);

        }

        // GET: services/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _bll.Services.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (service == null)
            {
                return NotFound();
            }

            return View(service);

        }

        // GET: services/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.Service service)
        {
            service.AppUserId = User.UserGuidId();

            if (ModelState.IsValid)
            {
                _bll.Services.Add(service);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(service);
        }

        // GET: services/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _bll.Services.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (service == null)
            {
                return NotFound();
            }

            return View(service);

        }

        // POST: services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BLL.App.DTO.Service service)
        {
            service.AppUserId = User.UserGuidId();

            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Services.Update(service);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _bll.Services.ExistsAsync(service.Id, User.UserGuidId()))
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

            return View(service);

        }

        // GET: services/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _bll.Services.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (service == null)
            {
                return NotFound();
            }

            return View(service);


        }

        // POST: services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Services.DeleteAsync(id, User.UserGuidId());
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));

        }
    }
}
