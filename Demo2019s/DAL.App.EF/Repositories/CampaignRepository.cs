using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class CampaignRepository : EFBaseRepository<AppDbContext, Domain.Campaign, DAL.App.DTO.Campaign>, ICampaignRepository
    {
        public CampaignRepository(AppDbContext dbContext) : base(dbContext, new BaseDALMapper<Domain.Campaign, DAL.App.DTO.Campaign>())
        {
        }
        public async Task<IEnumerable<Campaign>> AllAsync(Guid? userId = null)
        {
            if (userId == null)
            {
                return await base.AllAsync(); // base is not actually needed, using it for clarity
            }

            return (await RepoDbSet.Where(o => o.AppUserId == userId)
                .ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<Campaign> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }

            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(a => a.Id == id);
            }

            return await RepoDbSet.AnyAsync(a => a.Id == id && a.AppUserId == userId);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var owner = await FirstOrDefaultAsync(id, userId);
            base.Remove(owner);
        }


/*
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
*/
}
}