using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;

namespace BLL.App.Services
{
    public class PaymentService : BaseEntityService<IPaymentRepository, IAppUnitOfWork, DAL.App.DTO.Payment, Payment>, IPaymentService
    {
        public PaymentService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Payment, Payment>(), unitOfWork.Payments)
        {
        }
    }
}