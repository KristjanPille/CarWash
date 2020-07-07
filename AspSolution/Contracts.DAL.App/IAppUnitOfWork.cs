using Contracts.DAL.App.Repositories;
using ee.itcollege.carwash.kristjan.Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker
    {
 
        ILangStrRepository LangStrs { get; }
        ILangStrTranslationRepository LangStrTranslations { get; }
        ICampaignRepository Campaigns { get; }
        ICarRepository Cars { get; }
        ICheckRepository Checks { get; }
        IIsInServiceRepository IsInServices { get; }
        IModelMarkRepository ModelMarks { get; }
        IOrderRepository Orders { get; }
        IPaymentRepository Payments { get; }
        IPaymentMethodRepository PaymentMethods { get; }
        IServiceRepository Services { get; }
    }
}