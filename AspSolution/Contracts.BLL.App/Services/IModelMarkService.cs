using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.carwash.kristjan.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using PublicApi.DTO.v1;
using ModelMark = BLL.App.DTO.ModelMark;
using ModelMarkDTO = DAL.App.DTO.ModelMark;

namespace Contracts.BLL.App.Services
{
    public interface IModelMarkService : IBaseEntityService<ModelMark>, IModelMarkRepositoryCustom<ModelMark>
    {
        Task<Guid> GetModelMarkId(Car gpsLocation);
        
        Task<IEnumerable<ModelMarkDTO>> GetMarkModels(string mark);
        
        Task<IEnumerable<ModelMarkDTO>> GetMarks();
    }
}