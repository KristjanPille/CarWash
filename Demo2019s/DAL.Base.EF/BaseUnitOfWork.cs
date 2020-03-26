using System.Threading.Tasks;
using Contracts.DAL.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF
{
    public class BaseUnitOfWork<TDbContext> : IBaseUnitOfWork
    where TDbContext: DbContext
    {
        protected TDbContext UOWDbContext;

        public BaseUnitOfWork(TDbContext uowDbContext)
        {
            UOWDbContext = uowDbContext;
        }

        public int SaveChanges()
        {
            return UOWDbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await UOWDbContext.SaveChangesAsync();
        }
    }
}