using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class ServiceService :
        BaseEntityService<IAppUnitOfWork, IServiceRepository, IServiceServiceMapper,
            DAL.App.DTO.Service, BLL.App.DTO.Service>, IServiceService
    {
        public ServiceService(IAppUnitOfWork uow) : base(uow, uow.Services,
            new ServiceServiceMapper())
        {
        }
    }
}