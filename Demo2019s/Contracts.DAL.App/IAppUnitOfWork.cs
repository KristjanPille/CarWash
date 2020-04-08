using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {
        ICampaignRepository Campaigns { get; }
        ICarRepository Cars { get; }
        ICarTypeRepository CarTypes { get; }
        ICheckRepository Checks { get; }
        IDiscountRepository Discounts { get; }
        IIsInWashRepository IsInWashes { get; }
        IModelMarkRepository ModelMarks { get; }
        IOrderRepository Orders { get; }
        IPaymentRepository Payments { get; }
        IPaymentMethodRepository PaymentMethods { get; }
        IPersonRepository Persons { get; }
        IPersonCarRepository PersonCars { get; }
        IServiceRepository Services { get; }
        IWashRepository Washes { get; }
        IWashTypeRepository WashTypes { get; }
    }
}