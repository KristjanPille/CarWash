﻿﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;

using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.carwash.kristjan.DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class CheckRepository :
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Check, DAL.App.DTO.Check>,
        ICheckRepository
    {
        public CheckRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Domain.App.Check, DTO.Check>())
        {
        }

        public override async Task<IEnumerable<Check>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);

            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }
    }
}