using System;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using Car = PublicApi.DTO.v1.Car;

namespace Contracts.DAL.App.Repositories
{
    public interface IModelMarkRepository : IBaseRepository<ModelMark>, IModelMarkRepositoryCustom
    {
        Task<ModelMark> FindModelMarkFromCarDTO(Car car);
    }
}