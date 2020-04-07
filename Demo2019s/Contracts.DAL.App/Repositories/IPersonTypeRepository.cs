using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
namespace Contracts.DAL.App.Repositories
{
    public interface IPersonTypeRepository : IBaseRepository<PersonType>
    {
        Task<IEnumerable<PersonType>> AllAsync(Guid? userId = null);
        Task<PersonType> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id);
        Task DeleteAsync(Guid id, Guid? userId = null);
    }
}