﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.carwash.kristjan.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;
 using Service = BLL.App.DTO.Service;

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

        public override async Task<IEnumerable<DAL.App.DTO.Service>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(l => l.NameOfService)
                .ThenInclude(t => t!.Translations)
                .Include(l => l.Description)
                .ThenInclude(t => t!.Translations);
            
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }
    }
}