using BLL.App.Services;
using BLL.Base;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        public AppBLL(IAppUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IPersonService Persons =>
            GetService<IPersonService>(() => new PersonService(UnitOfWork));

        public ICarService Cars =>
            GetService<ICarService>(() => new Services.CarService(UnitOfWork));

        public IPersonCarService PersonCars =>
            GetService<IPersonCarService>(() => new PersonCarService(UnitOfWork));

        public ICampaignService Campaigns =>
            GetService<ICampaignService>(() => new CampaignService(UnitOfWork));

        public ICarTypeService CarTypes =>
            GetService<ICarTypeService>(() => new CarTypeService(UnitOfWork));
        
        public ICheckService Checks =>
            GetService<ICheckService>(() => new CheckService(UnitOfWork));
        
        public IDiscountService Discounts =>
            GetService<IDiscountService>(() => new DiscountService(UnitOfWork));
        
        public IIsInWashService IsInWashes =>
            GetService<IsInWashService>(() => new IsInWashService(UnitOfWork));

        public IModelMarkService ModelMarks =>
            GetService<ModelMarkService>(() => new ModelMarkService(UnitOfWork));

        public IOrderService Orders =>
            GetService<OrderService>(() => new OrderService(UnitOfWork));

        public IPaymentMethodService PaymentMethods =>
            GetService<PaymentMethodService>(() => new PaymentMethodService(UnitOfWork));

        public IPaymentService Payments =>
            GetService<PaymentService>(() => new PaymentService(UnitOfWork));
        
        public IServiceService Services =>
            GetService<ServiceService>(() => new ServiceService(UnitOfWork));

        public IWashService Washes =>
            GetService<WashService>(() => new WashService(UnitOfWork));


        public IWashTypeService WashTypes =>
            GetService<WashTypeService>(() => new WashTypeService(UnitOfWork));
    }
}