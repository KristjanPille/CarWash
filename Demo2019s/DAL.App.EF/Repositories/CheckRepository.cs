using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class CheckRepository : BaseRepository<Check, AppDbContext>, ICheckRepository
    {
        public CheckRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        public async  Task<IEnumerable<Check>> AllASync()
        {
            return await RepoDbSet.ToListAsync();
        }
    }
}