using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class WashRepository : EFBaseRepository<Wash, AppDbContext>, IWashRepository
    {
        public WashRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        public async  Task<IEnumerable<Wash>> AllASync()
        {
            return await RepoDbSet.ToListAsync();
        }
    }
}