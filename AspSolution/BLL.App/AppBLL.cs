using BLL.App.Services;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using ee.itcollege.carwash.kristjan.BLL.Base;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        public AppBLL(IAppUnitOfWork uow) : base(uow)
        {
        }

        public ICarService Cars =>
            GetService<ICarService>(() => new Services.CarService(UOW));

        public ICampaignService Campaigns =>
            GetService<ICampaignService>(() => new CampaignService(UOW));

        public ICheckService Checks =>
            GetService<ICheckService>(() => new CheckService(UOW));

        public IIsInServiceService IsInServices =>
            GetService<IIsInServiceService>(() => new IsInServiceService(UOW));

        public IModelMarkService ModelMarks =>
            GetService<ModelMarkService>(() => new ModelMarkService(UOW));

        public IOrderService Orders =>
            GetService<OrderService>(() => new OrderService(UOW));

        public IPaymentMethodService PaymentMethods =>
            GetService<PaymentMethodService>(() => new PaymentMethodService(UOW));

        public IPaymentService Payments =>
            GetService<PaymentService>(() => new PaymentService(UOW));
        
        public IServiceService Services =>
            GetService<ServiceService>(() => new ServiceService(UOW));
        
        public ILangStrService LangStrs =>
            GetService<ILangStrService>(() => new LangStrService(UOW));

        public ILangStrTranslationService LangStrTranslation =>
            GetService<ILangStrTranslationService>(() => new LangStrTranslationService(UOW));
    }
}