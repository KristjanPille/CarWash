using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using Campaign = BLL.App.DTO.Campaign;

namespace Contracts.BLL.App.Services
{
    public interface ICampaignService : IBaseEntityService<Campaign>, ICampaignRepositoryCustom<Campaign>
    {
    }
}