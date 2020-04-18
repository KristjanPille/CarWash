using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using IsInWash = DAL.App.DTO.IsInWash;

namespace DAL.App.EF.Repositories
{
    public class IsInWashRepository : EFBaseRepository<AppDbContext, Domain.IsInWash, DAL.App.DTO.IsInWash>, IIsInWashRepository
    {
        public IsInWashRepository(AppDbContext dbContext) : base(dbContext, new BaseDALMapper<Domain.IsInWash, DAL.App.DTO.IsInWash>())
        {
        }
        public async Task<IEnumerable<IsInWash>> AllAsync(Guid? userId = null)
        {
            if (userId == null)
            {
                return await base.AllAsync(); // base is not actually needed, using it for clarity
            }

            return (await RepoDbSet.Where(o => o.AppUserId == userId)
                .ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DTO.IsInWash> FirstOrDefaultAsync(Guid id, Guid? userId = null)
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
            var Discount = await FirstOrDefaultAsync(id, userId);
            base.Remove(Discount);
        }
    }
}