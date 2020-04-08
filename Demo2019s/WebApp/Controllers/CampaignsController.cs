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

namespace WebApp.Controllers
{
    [Authorize(Roles = "User")]
    public class CampaignsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public CampaignsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Campaigns
        public async Task<IActionResult> Index()
        {
            var campaigns = await _uow.Campaigns.AllAsync(User.UserGuidId());
            return View(campaigns);

        }

        // GET: Campaigns/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = await _uow.Campaigns.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // GET: Campaigns/Create
        public async Task<IActionResult> Create()
        {
            var vm = new CampaignCreateEditViewModel();
            
            vm.ServiceSelectList = new SelectList(await _uow.Services.AllAsync(User.UserGuidId()), nameof(Service.ServiceId),
                nameof(Service.NameOfService));
            return View(vm);
        }

        // POST: Campaigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CampaignCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.Campaigns.Add(vm.Campaign);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.ServiceSelectList = new SelectList(await _uow.Services.AllAsync(User.UserGuidId()), nameof(Service.ServiceId),
                nameof(Service.NameOfService));
            return View(vm);
        }

        // GET: Campaigns/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = await _uow.Campaigns.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (campaign == null)
            {
                return NotFound();
            }
            return View(campaign);

        }

        // POST: Campaigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Campaign campaign)
        {
            if (id != campaign.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Campaigns.Update(campaign);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.Campaigns.ExistsAsync(id, User.UserGuidId()))
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
            return View(campaign);

        }

        // GET: Campaigns/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = await _uow.Campaigns.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);

        }

        // POST: Campaigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Campaigns.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
