using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class IsInWashRepository : EFBaseRepository<IsInWash, AppDbContext>, IIsInWashRepository
    {
        public IsInWashRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        public async  Task<IEnumerable<IsInWash>> AllASync()
        {
            return await RepoDbSet.ToListAsync();
        }
    }
}