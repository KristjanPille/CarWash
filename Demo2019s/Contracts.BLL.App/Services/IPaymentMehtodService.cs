using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using PaymentMethod = BLL.App.DTO.PaymentMethod;

namespace Contracts.BLL.App.Services
{
    public interface IPaymentMethodService : IBaseEntityService<PaymentMethod>, IPaymentMethodRepositoryCustom<PaymentMethod>
    {
    }
}