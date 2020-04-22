using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;

namespace BLL.App.Services
{
    public class CheckService : BaseEntityService<ICheckRepository, IAppUnitOfWork, DAL.App.DTO.Check, Check>, ICheckService
    {
        public CheckService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Check, Check>(), unitOfWork.Checks)
        {
        }
    }
}