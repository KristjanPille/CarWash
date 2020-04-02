using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using DAL.App.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class CampaignsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAppUnitOfWork _uow;

        public CampaignsController(AppDbContext context, IAppUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
            //_campaignRepository = campaignRepository;
            //_campaignRepository = new CampaignRepository(_context);
        }

        // GET: Campaigns
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Campaigns.AllAsync());
        }

        // GET: Campaigns/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = await _uow.Campaigns.FindAsync(id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // GET: Campaigns/Create
        public IActionResult Create()
        {
            var vm = new CampaignCreateEditViewModel();
            vm.ServiceSelectList = new SelectList(
                _context.Set<Service>(),
                nameof(Service.ServiceId), nameof(Service.NameOfService));
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
            return View(vm);
        }

        // GET: Campaigns/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vm = new CampaignCreateEditViewModel();

            vm.Campaign = await _uow.Campaigns.FindAsync(id);
            if (vm.Campaign == null)
            {
                return NotFound();
            }
            vm.ServiceSelectList = new SelectList(
                _context.Set<Service>(),
                nameof(Service.ServiceId), nameof(Service.NameOfService));

            return View(vm);
        }

        // POST: Campaigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id,  CampaignCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Campaigns.Update(vm.Campaign);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampaignExists(vm.Campaign.Id))
                    {
                        return NotFound();
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            vm.ServiceSelectList = new SelectList(
                _context.Set<Service>(),
                nameof(Service.ServiceId), nameof(Service.NameOfService));
            return View(vm);
        }

        // GET: Campaigns/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                
                return NotFound();
            }

            var campaign = await _uow.Campaigns.FindAsync(id);
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
            _uow.Campaigns.Remove(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        private bool CampaignExists(Guid id)
        {
            return _uow.Campaigns.Find(id).Equals(null);
        }
    }
}
