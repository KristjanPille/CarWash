using BLL.App.DTO;
using ee.itcollege.carwash.kristjan.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using PaymentMethod = BLL.App.DTO.Payment;

namespace Contracts.BLL.App.Services
{
    public interface IPaymentService : IBaseEntityService<Payment>, IPaymentRepositoryCustom<Payment>
    {
    }
}