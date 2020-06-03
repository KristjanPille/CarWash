﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
 using Domain.App;
 using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class IsInServiceRepository :
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.IsInService, DAL.App.DTO.IsInService>,
        IIsInServiceRepository
    {
        public IsInServiceRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Domain.App.IsInService, DTO.IsInService>())
        {
        }

        public virtual async Task<IEnumerable<DTO.IsInService>> GetAllAsync(Guid isInServiceId, Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query.Where(e => e.Id == isInServiceId);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }
        
    }
}