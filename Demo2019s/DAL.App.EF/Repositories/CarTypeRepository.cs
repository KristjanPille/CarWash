using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class CarTypeRepository : EFBaseRepository<CarType, AppDbContext>, ICarTypeRepository
    {
        public CarTypeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        public async  Task<IEnumerable<CarType>> AllASync()
        {
            return await RepoDbSet.ToListAsync();
        }
    }
}