using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;
using Campaign = DAL.App.DTO.Campaign;

namespace WebApp.Controllers
{
    [Authorize(Roles = "User")]
    public class CampaignsController : Controller
    {
        private readonly IAppBLL _bll;

        public CampaignsController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: Campaigns
        public async Task<IActionResult> Index()
        {
            var campaigns = await _bll.Campaigns.AllAsync(User.UserGuidId());

            return View(campaigns);

        }

        // GET: Campaigns/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = await _bll.Campaigns.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);

        }

        // GET: Campaigns/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Campaigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.Campaign campaign)
        {
            campaign.AppUserId = User.UserGuidId();

            if (ModelState.IsValid)
            {
                _bll.Campaigns.Add(campaign);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(campaign);

        }

        // GET: Campaigns/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = await _bll.Campaigns.FirstOrDefaultAsync(id.Value, User.UserGuidId());

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
        public async Task<IActionResult> Edit(Guid id, BLL.App.DTO.Campaign campaign)
        {
            campaign.AppUserId = User.UserGuidId();

            if (id != campaign.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Campaigns.Update(campaign);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _bll.Campaigns.ExistsAsync(campaign.Id, User.UserGuidId()))
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

            var campaign = await _bll.Campaigns.FirstOrDefaultAsync(id.Value, User.UserGuidId());

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
            await _bll.Campaigns.DeleteAsync(id, User.UserGuidId());
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));

        }

    }
}
