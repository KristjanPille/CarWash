using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace DAL.App.EF.Repositories
{
    public class PersonCarRepository : EFBaseRepository<PersonCar, AppDbContext>, IPersonCarRepository
    {
        public PersonCarRepository(AppDbContext dbContext) : base(dbContext)
        {
        }        
        public async Task<IEnumerable<PersonCar>> AllAsync(Guid? userId = null)
        {
            if (userId == null)
            {
                return await base.AllAsync(); // base is not actually needed, using it for clarity
            }

            return await RepoDbSet.ToListAsync();

        }
        
        public async Task<PersonCar> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(a => a.Id == id)
                .AsQueryable();

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(a => a.Id == id);
            }

            return await RepoDbSet.AnyAsync(a => a.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var check = await FirstOrDefaultAsync(id, userId);
            base.Remove(check);
        }

        public async Task<IEnumerable<PersonCarDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(o => o.Person)
                .Include(o => o.Car)
                .AsQueryable();
            if (userId != null)
            {
                query = query.Where(o => o.Car!.AppUserId == userId && o.Person!.AppUserId == userId);
            }

            return await query
                .Select(o => new PersonCarDTO()
                {
                    Id = o.Id,
                    CarId = o.CarId,
                    PersonId = o.PersonId,
                    Car = new CarDTO()
                    {
                        Id = o.Car!.Id,
                        LicenceNr = o.Car.LicenceNr,
                    },
                    Person = new PersonDTO()
                    {
                        Id = o.Person!.Id,
                        FirstName = o.Person!.FirstName,
                        LastName = o.Person!.LastName,
                    }
                })
                .ToListAsync();

        }

        public Task<PersonCarDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            throw new NotImplementedException();
        }
    }
}