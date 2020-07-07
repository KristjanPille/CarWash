using Contracts.BLL.App.Services;
using ee.itcollege.carwash.kristjan.Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        ICarService Cars { get; }
        ICampaignService Campaigns { get; }
        ICheckService Checks { get; }
        IIsInServiceService IsInServices { get; }
        IModelMarkService ModelMarks { get; }
        IOrderService Orders { get; }
        IPaymentMethodService PaymentMethods { get; }
        IPaymentService Payments { get; }
        IServiceService Services { get; }
        ILangStrService LangStrs { get; }
        ILangStrTranslationService LangStrTranslation { get; }
    
    }
}