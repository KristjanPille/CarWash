﻿using System.Collections.Generic;
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
    public class PaymentMethodRepository:
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.PaymentMethod, DAL.App.DTO.PaymentMethod>,
        IPaymentMethodRepository
    {
        public PaymentMethodRepository(AppDbContext repoDbContext) : base(repoDbContext,  
            new DALMapper<Domain.App.PaymentMethod, DTO.PaymentMethod>())
        {
        }

        public override async Task<IEnumerable<PaymentMethod>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(l => l.PaymentMethodName).
                ThenInclude(t => t!.Translations);

            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }
    }
}