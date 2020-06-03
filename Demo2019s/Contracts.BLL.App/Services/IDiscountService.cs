using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using Discount = BLL.App.DTO.Discount;

namespace Contracts.BLL.App.Services
{
    public interface IDiscountService : IBaseEntityService<Discount>, IDiscountRepositoryCustom<Discount>
    {
    }
}