using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;

namespace BLL.App.Services
{
    public class DiscountService : BaseEntityService<IDiscountRepository, IAppUnitOfWork, DAL.App.DTO.Discount, Discount>, IDiscountService
    {
        public DiscountService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Discount, Discount>(), unitOfWork.Discounts)
        {
        }
    }
}