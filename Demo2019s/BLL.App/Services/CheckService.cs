using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF;

namespace BLL.App.Services
{
    public class CheckService :
        BaseEntityService<IAppUnitOfWork, ICheckRepository, ICheckServiceMapper,
            DAL.App.DTO.Check, BLL.App.DTO.Check>, ICheckService
    {
        public CheckService(IAppUnitOfWork uow) : base(uow, uow.Checks,
            new CheckServiceMapper())
        {
        }
    }
}