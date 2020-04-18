using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPersonCarRepository : IBaseRepository<PersonCar>
    {
        Task<IEnumerable<PersonCar>> AllAsync(Guid? userId = null);
        Task<PersonCar> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        // DTO methods
        /*
        Task<IEnumerable<PersonCarDTO>> DTOAllAsync(Guid? userId = null);
        Task<PersonCarDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
        */
    }
}