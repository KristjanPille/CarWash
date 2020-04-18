using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using IsInWash = DAL.App.DTO.IsInWash;

namespace Contracts.DAL.App.Repositories
{
    public interface IIsInWashRepository : IBaseRepository<IsInWash>
    {
        Task<IEnumerable<IsInWash>> AllAsync(Guid? userId = null);
        Task<IsInWash> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
    }
}