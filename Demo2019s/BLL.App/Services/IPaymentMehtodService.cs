using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;

namespace BLL.App.Services
{
    public class PaymentMethodService : BaseEntityService<IPaymentMethodRepository, IAppUnitOfWork, DAL.App.DTO.PaymentMethod, PaymentMethod>, IPaymentMethodService
    {
        public PaymentMethodService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.PaymentMethod, PaymentMethod>(), unitOfWork.PaymentMethods)
        {
        }
    }
}