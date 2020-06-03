using Contracts.BLL.Base.Mappers;
using Contracts.DAL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface IPaymentServiceMapper: IBaseMapper<DALAppDTO.Payment, BLLAppDTO.Payment>
    {
    }
}