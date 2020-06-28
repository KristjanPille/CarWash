﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain;
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
            //query = query
            //    .Include(l => l.NameOfCampaign);

            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }
    }
}