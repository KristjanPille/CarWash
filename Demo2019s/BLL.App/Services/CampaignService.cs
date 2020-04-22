using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;

namespace BLL.App.Services
{
    public class CampaignService : BaseEntityService<ICampaignRepository, IAppUnitOfWork, DAL.App.DTO.Campaign, Campaign>, ICampaignService
    {
        public CampaignService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Campaign, Campaign>(), unitOfWork.Campaigns)
        {
        }
    }
}