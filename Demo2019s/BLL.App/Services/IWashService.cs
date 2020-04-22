using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;

namespace BLL.App.Services
{
    public class WashService : BaseEntityService<IWashRepository, IAppUnitOfWork, DAL.App.DTO.Wash, Wash>, IWashService
    {
        public WashService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Wash, Wash>(), unitOfWork.Washes)
        {
        }
    }
}