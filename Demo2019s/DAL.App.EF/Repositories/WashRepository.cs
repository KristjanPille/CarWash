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
    public class WashRepository : EFBaseRepository<Wash, AppDbContext>, IWashRepository
    {
        public WashRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<Wash>> AllAsync(Guid? userId = null)
        {
            if (userId == null)
            {
                return await base.AllAsync(); // base is not actually needed, using it for clarity
            }

            return await RepoDbSet.ToListAsync();
        }
        
        public async Task<Wash> FirstOrDefaultAsync(Guid id, Guid? userId = null)
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
            var wash = await FirstOrDefaultAsync(id, userId);
            base.Remove(wash);
        }

        public async Task<IEnumerable<WashDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
    
            return await query
                .Select(o => new WashDTO()
                {
                    Id = o.Id,
                    NameOfWashType = o.NameOfWashType
                })
                .ToListAsync();
        }

        public async Task<WashDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();

            var washDTO = await query.Select(o => new WashDTO()
            {
                Id = o.Id,
                NameOfWashType = o.NameOfWashType
            }).FirstOrDefaultAsync();

            return washDTO;
        }
    }
}