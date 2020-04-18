using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using CarType = DAL.App.DTO.CarType;

namespace Contracts.DAL.App.Repositories
{
    public interface ICarTypeRepository : IBaseRepository<CarType>
    {
        Task<IEnumerable<CarType>> AllAsync(Guid? userId = null);
        Task<CarType> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        /*
        Task<IEnumerable<CarTypeDTO>> DTOAllAsync(Guid? userId = null);
        Task<CarTypeDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
        */
    }
}