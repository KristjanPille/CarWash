using Contracts.DAL.App.Repositories;
using ee.itcollege.carwash.kristjan.Contracts.BLL.Base.Services;
using Campaign = BLL.App.DTO.Campaign;

namespace Contracts.BLL.App.Services
{
    public interface ICampaignService : IBaseEntityService<Campaign>, ICampaignRepositoryCustom<Campaign>
    {
    }
}