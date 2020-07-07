using ee.itcollege.carwash.kristjan.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using PublicApi.DTO.v1;
using Check = BLL.App.DTO.Check;
using CheckDTO = PublicApi.DTO.v1.Check;

namespace Contracts.BLL.App.Services
{
    public interface ICheckService : IBaseEntityService<Check>, ICheckRepositoryCustom<Check>
    {
        CheckDTO CreateCheck(Payment payment);
    }
}