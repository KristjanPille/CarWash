using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ee.itcollege.carwash.kristjan.DAL.Base.EF
{
    public class EFBaseUnitOfWork<TKey, TDbContext> : BaseUnitOfWork<TKey>
        where TDbContext : DbContext 
        where TKey : IEquatable<TKey>

    {
        protected readonly TDbContext UOWDbContext;
        

        public EFBaseUnitOfWork(TDbContext uowDbContext)
        {
            UOWDbContext = uowDbContext;
        }

        public override async Task<int> SaveChangesAsync()
        {
             var result = await UOWDbContext.SaveChangesAsync();
             
             UpdateTrackedEntities();
             
             return result;
        }

        
    }
}