using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.carwash.kristjan.Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using Car = PublicApi.DTO.v1.Car;

namespace Contracts.DAL.App.Repositories
{
    public interface IModelMarkRepository : IBaseRepository<ModelMark>, IModelMarkRepositoryCustom
    {
        Task<ModelMark> FindModelMarkFromCarDTO(Car car);
        Task<IEnumerable<ModelMark>> FindMarkModels(string car);
        
        Task<IEnumerable<ModelMark>> FindMarks();
    }
}