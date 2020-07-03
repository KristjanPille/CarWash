using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using Car = PublicApi.DTO.v1.Car;
using ServiceMethod = BLL.App.DTO.Service;

namespace Contracts.BLL.App.Services
{
    public interface IServiceService : IBaseEntityService<Service>, IServiceRepositoryCustom<Service>
    {
        Task<Service> ApplyDiscount(Service id);
        
        Task<double> GetServicePrice(Car car, Guid serviceId);
    }
}