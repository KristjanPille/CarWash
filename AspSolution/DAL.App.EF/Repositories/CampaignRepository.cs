﻿using System;
 using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.carwash.kristjan.DAL.Base.EF.Repositories;

 using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class CampaignRepository :
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Campaign, DAL.App.DTO.Campaign>,
        ICampaignRepository
    {
        public CampaignRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Domain.App.Campaign, DTO.Campaign>())
        {
        }

        public override async Task<IEnumerable<Campaign>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(l => l.NameOfCampaign)
                .ThenInclude(t => t!.Translations)
                .Include(l => l.Description)
                .ThenInclude(t => t!.Translations);
            
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }
        
        public override async Task<DAL.App.DTO.Campaign> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntity = await query
                    .Include(l => l.NameOfCampaign)
                    .ThenInclude(t => t!.Translations)
                    .Include(l => l.Description)
                    .ThenInclude(t => t!.Translations)
                .FirstOrDefaultAsync();
            
            var result = Mapper.Map(domainEntity);
            return result;
        }
    }
}