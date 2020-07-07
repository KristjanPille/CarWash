using ee.itcollege.carwash.kristjan.Contracts.DAL.Base.Mappers;

using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface IDiscountServiceMapper: IBaseMapper<DALAppDTO.Discount, BLLAppDTO.Discount>
    {
    }
}