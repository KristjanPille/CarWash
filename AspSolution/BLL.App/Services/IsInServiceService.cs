using BLL.App.Mappers;
using ee.itcollege.carwash.kristjan.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF;

namespace BLL.App.Services
{
    public class IsInServiceService :
        BaseEntityService<IAppUnitOfWork, IIsInServiceRepository, IIsInServiceServiceMapper,
            DAL.App.DTO.IsInService, BLL.App.DTO.IsInService>, IIsInServiceService
    {
        public IsInServiceService(IAppUnitOfWork uow) : base(uow, uow.IsInServices,
            new IsInServiceServiceMapper())
        {
        }
    }
}