using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using Check = BLL.App.DTO.Check;

namespace Contracts.BLL.App.Services
{
    public interface ICheckService : IBaseEntityService<Check>, ICheckRepositoryCustom<Check>
    {
    }
}