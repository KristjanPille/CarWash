using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace DAL.App.EF.Repositories
{
      public class PersonRepository : EFBaseRepository<AppDbContext, Domain.Person, DAL.App.DTO.Person>, IPersonRepository
    {
        public PersonRepository(AppDbContext dbContext) : base(dbContext,
            new BaseDALMapper<Domain.Person, DAL.App.DTO.Person>())
        {
        }

        public async Task<IEnumerable<DAL.App.DTO.Person>> AllAsync(Guid? userId = null)
        {
            if (userId == null)
            {
                return await base.AllAsync(); // base is not actually needed, using it for clarity
            }

            return (await RepoDbSet
                    .Where(o => o.AppUserId == userId)
                    .Select(dbEntity => new PersonDisplay()
                    {
                        Id = dbEntity.Id,
                        FirstName = dbEntity.FirstName, 
                        LastName = dbEntity.LastName,
                    })
                    .ToListAsync())
                .Select(dbEntity => Mapper.Map<PersonDisplay,DAL.App.DTO.Person>(dbEntity));
        }

        public async Task<DAL.App.DTO.Person> FirstOrDefaultAsync(Guid id, Guid? userId = null)
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
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }

            var Person = await query.AsNoTracking().FirstOrDefaultAsync();
            base.Remove(Person.Id);
        }

    }
}