using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Discount = DAL.App.DTO.Discount;

namespace Contracts.DAL.App.Repositories
{
    public interface IDiscountRepository : IBaseRepository<Discount>
    {
        Task<IEnumerable<Discount>> AllAsync(Guid? userId = null);
        Task<Discount> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
    }
}