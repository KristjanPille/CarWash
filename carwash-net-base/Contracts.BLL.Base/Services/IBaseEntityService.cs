using System;

using ee.itcollege.carwash.kristjan.Contracts.DAL.Base.Repositories;
using ee.itcollege.carwash.kristjan.Contracts.Domain;

namespace ee.itcollege.carwash.kristjan.Contracts.BLL.Base.Services
{
    public interface IBaseEntityService<TEntity> : IBaseEntityService<Guid, TEntity>
        where TEntity : class, IDomainEntityId<Guid>, new()
    {
    }

    //Gets DAL repo methods(saveasync, remove etc) and applies to BLL services.
    // IBaseService is being implemented from but not really used as of now.
    public interface IBaseEntityService<in TKey, TEntity> : IBaseService, IBaseRepository<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IDomainEntityId<TKey>, new()
    {
        
    }

}