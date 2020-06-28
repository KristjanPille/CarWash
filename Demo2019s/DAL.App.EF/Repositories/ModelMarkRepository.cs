﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;
 using Car = PublicApi.DTO.v1.Car;

 namespace DAL.App.EF.Repositories
{
    public class ModelMarkRepository :
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.ModelMark, DAL.App.DTO.ModelMark>,
        IModelMarkRepository
    {
        public ModelMarkRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Domain.App.ModelMark, DTO.ModelMark>())
        {
        }

        public virtual async Task<IEnumerable<DTO.ModelMark>> GetAllAsync(Guid gpsSessionId, Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query.Where(e => e.Id == gpsSessionId);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public async Task<ModelMark> FindModelMarkFromCarDTO(Car car)
        {
            var modelMark = await RepoDbSet.AsNoTracking()
                .FirstOrDefaultAsync(a => a.Mark == car.Mark && a.Model == car.Model);
            
            return Mapper.Map(modelMark);
        }
    }
}