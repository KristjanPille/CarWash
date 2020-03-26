using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class WashTypeRepository : BaseRepository<WashType, AppDbContext>, IWashTypeRepository
    {
        public WashTypeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        public async  Task<IEnumerable<WashType>> AllASync()
        {
            return await RepoDbSet.ToListAsync();
        }
    }
}