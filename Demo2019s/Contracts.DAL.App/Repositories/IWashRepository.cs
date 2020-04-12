using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface IWashRepository : IBaseRepository<Wash>
    {
        Task<IEnumerable<Wash>> AllAsync(Guid? userId = null);
        Task<Wash> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        // DTO methods
        Task<IEnumerable<WashDTO>> DTOAllAsync(Guid? userId = null);
        Task<WashDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}