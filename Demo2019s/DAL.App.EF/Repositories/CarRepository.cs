using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace DAL.App.EF.Repositories
{
    public class CarRepository : EFBaseRepository<Car, AppDbContext>, ICarRepository
    {
        public CarRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<Car>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(a => a.CarType)
                .Include(a => a.ModelMark)
                .AsQueryable();
            
            return await query.ToListAsync();
        }
        public async Task<Car> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(a => a.CarType)
                .Include(a => a.ModelMark)
                .AsQueryable();

            return await query.FirstOrDefaultAsync();
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
            var car = await FirstOrDefaultAsync(id, userId);
            base.Remove(car);
        }

        public Task<IEnumerable<CarDTO>> DTOAllAsync(Guid? userId = null)
        {
            throw new NotImplementedException();
        }

        public async Task<CarDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }

            var carDTO = await query.Select(o => new CarDTO()
            {
                Id = o.Id,
                LicenceNr = o.LicenceNr,
            }).FirstOrDefaultAsync();

            return carDTO;
        }
    }
}