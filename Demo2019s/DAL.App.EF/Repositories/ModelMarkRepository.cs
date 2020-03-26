using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ModelMarkRepository : EFBaseRepository<ModelMark, AppDbContext>, IModelMarkRepository
    {
        public ModelMarkRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        public async  Task<IEnumerable<ModelMark>> AllASync()
        {
            return await RepoDbSet.ToListAsync();
        }
    }
}