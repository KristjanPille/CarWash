using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class CampaignRepository : EFBaseRepository<Campaign, AppDbContext>, ICampaignRepository
    {
        public CampaignRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        public async  Task<IEnumerable<Campaign>> AllASync()
        {
            return await RepoDbSet.ToListAsync();
        }
    }
}