using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;

namespace BLL.App.Services
{
    public class WashTypeService : BaseEntityService<IWashTypeRepository, IAppUnitOfWork, DAL.App.DTO.WashType, WashType>, IWashTypeService
    {
        public WashTypeService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.WashType, WashType>(), unitOfWork.WashTypes)
        {
        }
    }
}