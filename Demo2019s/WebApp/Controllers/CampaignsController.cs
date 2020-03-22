using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace WebApp.Controllers
{
    public class CampaignsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICampaignRepository _campaignRepository;

        public CampaignsController(AppDbContext context)
        {
            _context = context;
            _campaignRepository = new CampaignRepository(_context);
        }

        // GET: Campaigns
        public async Task<IActionResult> Index()
        {
            return View(await _campaignRepository.AllAsync());
        }

        // GET: Campaigns/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = await _campaignRepository.FindAsync(id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // GET: Campaigns/Create
        public IActionResult Create()
        {
            ViewData["ServiceId"] = new SelectList(_context.Set<Service>(), "ServiceId", "NameOfService");
            return View();
        }

        // POST: Campaigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CampaignId,ServiceId,NameOfCampaign,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Campaign campaign)
        {
            if (ModelState.IsValid)
            {
                //campaign.Id = Guid.NewGuid();
                _campaignRepository.Add(campaign);
                await _campaignRepository.SaveChangesAsync();
            
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServiceId"] = new SelectList(_context.Set<Service>(), "ServiceId", "NameOfService", campaign.ServiceId);
            return View(campaign);
        }

        // GET: Campaigns/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = await _campaignRepository.FindAsync(id);
            if (campaign == null)
            {
                return NotFound();
            }
            ViewData["ServiceId"] = new SelectList(_context.Set<Service>(), "ServiceId", "NameOfService", campaign.ServiceId);
            return View(campaign);
        }

        // POST: Campaigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CampaignId,ServiceId,NameOfCampaign,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Campaign campaign)
        {
            if (id != campaign.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _campaignRepository.Update(campaign);
                await _campaignRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServiceId"] = new SelectList(_context.Set<Service>(), "ServiceId", "NameOfService", campaign.ServiceId);
            return View(campaign);
        }

        // GET: Campaigns/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = await _campaignRepository.FindAsync(id);
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
            var campaign = _campaignRepository.Remove(id);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
