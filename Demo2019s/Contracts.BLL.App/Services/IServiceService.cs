using BLL.App.DTO;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using ServiceMethod = BLL.App.DTO.Service;

namespace Contracts.BLL.App.Services
{
    public interface IServiceService : IBaseEntityService<Service>, IServiceRepositoryCustom<Service>
    {
    }
}