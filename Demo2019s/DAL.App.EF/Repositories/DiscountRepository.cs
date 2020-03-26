using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class DiscountRepository : BaseRepository<Discount, AppDbContext>, IDiscountRepository
    {
        public DiscountRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        public async  Task<IEnumerable<Discount>> AllASync()
        {
            return await RepoDbSet.ToListAsync();
        }
    }
}