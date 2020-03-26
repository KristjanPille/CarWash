using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PersonTypeRepository : EFBaseRepository<PersonType, AppDbContext>, IPersonTypeRepository
    {
        public PersonTypeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        public async  Task<IEnumerable<PersonType>> AllASync()
        {
            return await RepoDbSet.ToListAsync();
        }
    }
}