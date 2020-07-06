using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF;
using PublicApi.DTO.v1;
using Payment = PublicApi.DTO.v1.Payment;

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

        public PublicApi.DTO.v1.Check CreateCheck(Payment payment)
        {
            var check = new PublicApi.DTO.v1.Check()
            {
                ServiceId = payment.ServiceId,
                CarId = payment.CarId,
                DateTimeCheck = DateTime.Now,
                PaymentAmount = payment.PaymentAmount
            };

            return check;
        }
    }
}