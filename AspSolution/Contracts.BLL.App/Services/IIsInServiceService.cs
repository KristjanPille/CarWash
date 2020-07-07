using ee.itcollege.carwash.kristjan.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using IsInService = BLL.App.DTO.IsInService;


namespace Contracts.BLL.App.Services
{
    public interface IIsInServiceService : IBaseEntityService<IsInService>, IIsInServiceRepositoryCustom<IsInService>
    {
    }
}