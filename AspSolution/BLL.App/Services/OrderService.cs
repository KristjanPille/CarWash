using BLL.App.DTO;
using BLL.App.Mappers;
using ee.itcollege.carwash.kristjan.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF;
using Payment = PublicApi.DTO.v1.Payment;

namespace BLL.App.Services
{
    public class OrderService :
        BaseEntityService<IAppUnitOfWork, IOrderRepository, IOrderServiceMapper,
            DAL.App.DTO.Order, BLL.App.DTO.Order>, IOrderService
    {
        public OrderService(IAppUnitOfWork uow) : base(uow, uow.Orders,
            new OrderServiceMapper())
        {
        }

        public PublicApi.DTO.v1.Order CreateOrder(Payment payment)
        {
            var order = new PublicApi.DTO.v1.Order()
            {
                ServiceId = payment.ServiceId,
                CarId = payment.CarId,
                DateAndTime = payment.TimeOfPayment,
                From = payment.From,
                To = payment.To
            };

            return order;
        }
    }
}