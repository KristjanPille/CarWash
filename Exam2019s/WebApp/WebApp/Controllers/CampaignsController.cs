#pragma warning disable 1591
using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    [Authorize]
    public class CampaignsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAppBLL _bll;

        public CampaignsController(AppDbContext context, IAppBLL bll)
        {
            _context = context;
            _bll = bll;
        }

        // GET: campaigns
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var result = await _bll.Campaigns.GetAllAsync();
            return View(result);
        }


        public async Task<IActionResult> UpdateStatistics(Guid? id)
        {
            if (id != null)
            {
                var campaign = await _bll.Campaigns.FirstOrDefaultAsync(id.Value);
                if (campaign != null)
                {
                    await _bll.Campaigns.UpdateAsync(campaign);
                }
            }

            return RedirectToAction(nameof(Index));
        }


        [AllowAnonymous]
        // GET: campaigns/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = await _context.Campaigns
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }
        
        // GET: campaigns/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: campaigns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind(
                "NameOfCampaign,Description,DiscountAmount")]
            Campaign campaign)
        {
            if (ModelState.IsValid)
            {
                campaign.Id = Guid.NewGuid();
                _context.Add(campaign);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(campaign);
        }

        // GET: campaigns/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = await _context.Campaigns.FindAsync(id);
            if (campaign == null)
            {
                return NotFound();
            }
            return View(campaign);
        }

        // POST: campaigns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind(
                "Name,Description,RecordedAt,Duration,Speed,Distance,Climb,Descent,PaceMin,PaceMax,campaignTypeId,AppUserId,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")]
            Campaign campaign)
        {
            if (id != campaign.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campaign);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!campaignExists(campaign.Id))
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

        // GET: campaigns/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = await _context.Campaigns
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // POST: campaigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var campaign = await _context.Campaigns.FindAsync(id);
            _context.Campaigns.Remove(campaign);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool campaignExists(Guid id)
        {
            return _context.Campaigns.Any(e => e.Id == id);
        }
    }
}
