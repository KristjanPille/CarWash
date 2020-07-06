using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using PublicApi.DTO.v1;
using Order = BLL.App.DTO.Order;

namespace Contracts.BLL.App.Services
{
    public interface IOrderService : IBaseEntityService<Order>, IOrderRepositoryCustom<Order>
    {
        PublicApi.DTO.v1.Order CreateOrder(Payment payment);
    }
}