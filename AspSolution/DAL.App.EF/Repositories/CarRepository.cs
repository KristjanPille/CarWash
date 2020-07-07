﻿using System;
 using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.carwash.kristjan.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;
 using ModelMark = BLL.App.DTO.ModelMark;


 namespace DAL.App.EF.Repositories
{
    public class CarRepository :
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Car, DAL.App.DTO.Car>,
        ICarRepository
    {
        public CarRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Domain.App.Car, DTO.Car>())
        {
        }

        public override async Task<IEnumerable<Car>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(g => g.AppUser)
                .Include(g => g.ModelMark);
            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(e => Mapper.Map(e));
            return result;
        }
    }
}