using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace DAL.App.EF.Repositories
{
    public class CampaignRepository : EFBaseRepository<Campaign, AppDbContext>, ICampaignRepository
    {
        public CampaignRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        public async Task<IEnumerable<Campaign>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(a => a.Service)
                .AsQueryable();
            
            return await query.ToListAsync();
        }
        public async Task<Campaign> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(a => a.Service)
                .Where(a => a.Id == id)
                .AsQueryable();

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(a => a.Id == id);
            }

            return await RepoDbSet.AnyAsync(a => a.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var campaign = await FirstOrDefaultAsync(id, userId);
            base.Remove(campaign);
        }

        public async Task<IEnumerable<CampaignDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            if (userId != null)
            {
                query = query.Where(o => o.Id == userId);
            }
            return await query
                .Select(o => new CampaignDTO()
                {
                    Id = o.Id,
                    NameOfCampaign = o.NameOfCampaign
                })
                .ToListAsync();
        }

        public async Task<CampaignDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.Id == userId);
            }

            var campaignDTO = await query.Select(o => new CampaignDTO()
            {
                Id = o.Id,
                NameOfCampaign = o.NameOfCampaign
            }).FirstOrDefaultAsync();

            return campaignDTO;
        }

    }
}