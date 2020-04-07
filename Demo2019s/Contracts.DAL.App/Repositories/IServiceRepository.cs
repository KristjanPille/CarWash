﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
namespace Contracts.DAL.App.Repositories
{
    public interface IServiceRepository : IBaseRepository<Service>
    {
        Task<IEnumerable<Service>> AllAsync(Guid? userId = null);
        Task<Service> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
    }
}