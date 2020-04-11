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
    public class CarTypeRepository : EFBaseRepository<CarType, AppDbContext>, ICarTypeRepository
    {
        public CarTypeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        public async Task<IEnumerable<CarType>> AllAsync(Guid? userId = null)
        {
         if (userId == null)
         {
             return await base.AllAsync(); // base is not actually needed, using it for clarity
         }
         return await RepoDbSet.ToListAsync();

        }
        public async Task<CarType> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
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
            var campaign = await FirstOrDefaultAsync(id, userId);
            base.Remove(campaign);
        }

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
    }
}