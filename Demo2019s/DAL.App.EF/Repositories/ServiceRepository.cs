using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Service = DAL.App.DTO.Service;

namespace DAL.App.EF.Repositories
{
    public class ServiceRepository : EFBaseRepository<AppDbContext, Domain.Service, DAL.App.DTO.Service>, IServiceRepository
    {
        public ServiceRepository(AppDbContext dbContext) : base(dbContext, new BaseDALMapper<Domain.Service, DAL.App.DTO.Service>())
        {
        }

        public async Task<IEnumerable<DAL.App.DTO.Service>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(a => a.Campaign)
                .AsQueryable();
            
            return (await query.ToListAsync())
                .Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DAL.App.DTO.Service> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(a => a.Campaign)
                .Where(a => a.Id == id)
                .AsQueryable();

            return Mapper.Map(  await query.FirstOrDefaultAsync());
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
            var service = await FirstOrDefaultAsync(id, userId);
            base.Remove(service);
        }

    }
}