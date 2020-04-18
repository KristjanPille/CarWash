using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace DAL.App.EF.Repositories
{
    public class CarTypeRepository : EFBaseRepository<AppDbContext, Domain.CarType, DAL.App.DTO.CarType>, ICarTypeRepository
    {
        public CarTypeRepository(AppDbContext dbContext) : base(dbContext, new BaseDALMapper<Domain.CarType, DAL.App.DTO.CarType>())
        {
        }
        public async Task<IEnumerable<DTO.CarType>> AllAsync(Guid? userId = null)
        {
            if (userId == null)
            {
                return await base.AllAsync(); // base is not actually needed, using it for clarity
            }

            return (await RepoDbSet.Where(o => o.AppUserId == userId)
                .ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DTO.CarType> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }

            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(a => a.Id == id);
            }

            return await RepoDbSet.AnyAsync(a => a.Id == id && a.AppUserId == userId);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var CarType = await FirstOrDefaultAsync(id, userId);
            base.Remove(CarType);
        }
/*
        public async Task<IEnumerable<CarTypeDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
    
            return await query
                .Select(o => new CarTypeDTO()
                {
                    Id = o.Id,
                    Name = o.Name
                })
                .ToListAsync();
        }

        public async Task<CarTypeDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();

            var carTypeDTO = await query.Select(o => new CarTypeDTO()
            {
                Id = o.Id,
                Name = o.Name
            }).FirstOrDefaultAsync();

            return carTypeDTO;
        }
        */
    }
}