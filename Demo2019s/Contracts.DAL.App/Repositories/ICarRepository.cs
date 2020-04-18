﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICarRepository : IBaseRepository<Car>
    {
        Task<IEnumerable<Car>> AllAsync(Guid? userId = null);
        Task<Car> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        // DTO methods
        /*
        Task<IEnumerable<CarDTO>> DTOAllAsync(Guid? userId = null);
        Task<CarDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
        */
    }
}