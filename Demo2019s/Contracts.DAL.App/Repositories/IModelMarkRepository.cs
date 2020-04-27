using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IModelMarkRepository : IModelMarkRepository<Guid, ModelMark>, IBaseRepository<ModelMark>
    {
    }

    public interface IModelMarkRepository<TKey, TDALEntity> : IBaseRepository<TKey,TDALEntity> 
        where TDALEntity : class, IDomainBaseEntity<TKey>, new() 
        where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TDALEntity>> AllAsync(Guid? userId = null);
        Task<TDALEntity> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        
        // DTO methods
        //Task<IEnumerable<ModelMarkDTO>> DTOAllAsync(Guid? userId = null);
        //Task<ModelMarkDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
        
    }
}