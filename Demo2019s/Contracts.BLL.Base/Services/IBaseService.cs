using System;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;

namespace Contracts.BLL.Base.Services
{
    public interface IBaseService
    {
        
    }
    public interface IEntityService<TBLLEntity> : IBaseService, IBaseRepository<TBLLEntity>
        where TBLLEntity : class, IDomainEntity<Guid>, new()
    {
        
    }
}