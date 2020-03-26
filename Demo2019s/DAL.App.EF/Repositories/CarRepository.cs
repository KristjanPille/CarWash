using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class CarRepository : BaseRepository<Car, AppDbContext>, ICarRepository
    {
        public CarRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        public async  Task<IEnumerable<Car>> AllASync()
        {
            return await RepoDbSet.ToListAsync();
        }
    }
}