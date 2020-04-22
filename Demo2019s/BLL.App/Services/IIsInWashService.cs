using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;

namespace BLL.App.Services
{
    public class IsInWashService : BaseEntityService<IIsInWashRepository, IAppUnitOfWork, DAL.App.DTO.IsInWash, IsInWash>, IIsInWashService
    {
        public IsInWashService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.IsInWash, IsInWash>(), unitOfWork.IsInWashes)
        {
        }
    }
}