using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using ee.itcollege.carwash.kristjan.BLL.Base.Services;
using Campaign = BLL.App.DTO.Campaign;

namespace BLL.App.Services
{
    public class CampaignService : BaseEntityService<IAppUnitOfWork, ICampaignRepository, ICampaignServiceMapper,
            DAL.App.DTO.Campaign, BLL.App.DTO.Campaign>, ICampaignService
    {
        public CampaignService(IAppUnitOfWork uow) : base(uow, uow.Campaigns,
            new CampaignServiceMapper())
        {
        }
    }
}