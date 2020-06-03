using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using Order = BLL.App.DTO.Order;

namespace Contracts.BLL.App.Services
{
    public interface IOrderService : IBaseEntityService<Order>, IOrderRepositoryCustom<Order>
    {
    }
}