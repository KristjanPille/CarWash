﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
 using DAL.App.EF.Mappers;
using ee.itcollege.carwash.kristjan.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;
 using PublicApi.DTO.v1;
 using Car = PublicApi.DTO.v1.Car;
 using ModelMark = DAL.App.DTO.ModelMark;

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

        public async Task<IEnumerable<ModelMark>> FindMarkModels(string mark)
        {
            var query = PrepareQuery();

            query = query.Where(e => e.Mark == mark);
            var domainEntities = await query.ToListAsync();

            var result = domainEntities.Select(e => Mapper.Map(e));
            
            return result;
        }

        // return ModelMarks without dupliactes
        public async Task<IEnumerable<ModelMark>> FindMarks()
        {
            var nonDuplicateList = new List<Domain.App.ModelMark>();
            
            var query = PrepareQuery();

            var domainEntities = await query.ToListAsync();

            foreach (var entity in domainEntities.Where(entity => !nonDuplicateList.Select(e => e.Mark).Contains(entity.Mark)))
            {
                nonDuplicateList.Add(entity);
            }

            //removes duplicates
            var result = nonDuplicateList.Select(e => Mapper.Map(e));
            
            return result;
        }
    }
}