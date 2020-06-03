﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ServiceRepository :
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Service, DAL.App.DTO.Service>,
        IServiceRepository
    {
        public ServiceRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Domain.App.Service, DTO.Service>())
        {
        }

        public virtual async Task<IEnumerable<DTO.Service>> GetAllAsync(Guid gpsSessionId, Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query.Where(e => e.Id == gpsSessionId)
                .OrderBy(e => e.CreatedAt);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }
    }
}