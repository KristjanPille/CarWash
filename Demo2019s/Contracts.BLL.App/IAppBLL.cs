using Contracts.BLL.App.Services;
using Contracts.BLL.Base;
using Domain;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        public IPersonService Persons { get; }
        public ICarService Cars { get; }
        public IPersonCarService PersonCars { get; }
        
        public ICampaignService Campaigns { get; }
        public ICarTypeService CarTypes { get; }
        public ICheckService Checks { get; }
        public IDiscountService Discounts { get; }
        public IIsInWashService IsInWashes { get; }
        public IModelMarkService ModelMarks { get; }
        public IOrderService Orders { get; }
        public IPaymentMethodService PaymentMethods { get; }
        public IPaymentService Payments { get; }
        public IServiceService Services { get; }
        public IWashService Washes { get; }
        public IWashTypeService WashTypes { get; }
    }
}