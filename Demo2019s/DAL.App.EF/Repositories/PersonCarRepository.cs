using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace DAL.App.EF.Repositories
{
    public class PersonCarRepository : EFBaseRepository<AppDbContext, Domain.PersonCar, DAL.App.DTO.PersonCar>, IPersonCarRepository
    {
        public PersonCarRepository(AppDbContext dbContext) : base(dbContext, new BaseDALMapper<Domain.PersonCar, DAL.App.DTO.PersonCar>())
        {
        }

        public async Task<IEnumerable<DAL.App.DTO.PersonCar>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(a => a.Car)
                .Include(a => a.Person)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(o => o.Person!.AppUserId == userId && o.Car!.AppUserId == userId);
            }

            return (await query.ToListAsync())
                .Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DAL.App.DTO.PersonCar> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(a => a.Car)
                .Include(a => a.Person)
                .Where(a => a.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(a => a.Person!.AppUserId == userId && a.Car!.AppUserId == userId);
            }

            return Mapper.Map(  await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(a => a.Id == id);
            }

            return await RepoDbSet.AnyAsync(a =>
                a.Id == id && a.Person!.AppUserId == userId && a.Car!.AppUserId == userId);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var owner = await FirstOrDefaultAsync(id, userId);
            base.Remove(owner);
        }

    }
}