﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;

using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.carwash.kristjan.DAL.Base.EF.Repositories;
 using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class OrderRepository :
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Order, DAL.App.DTO.Order>,
        IOrderRepository
    {
        public OrderRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Domain.App.Order, DTO.Order>())
        {
        }

        public override async Task<IEnumerable<Order>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);

            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }
    }
}