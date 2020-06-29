using System;
using System.Threading.Tasks;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using Service = BLL.App.DTO.Service;

namespace BLL.App.Services
{
    public class ServiceService :
        BaseEntityService<IAppUnitOfWork, IServiceRepository, IServiceServiceMapper,
            DAL.App.DTO.Service, BLL.App.DTO.Service>, IServiceService
    {
        public ServiceService(IAppUnitOfWork uow) : base(uow, uow.Services,
            new ServiceServiceMapper())
        {
        }
        
        public async Task<Service> ApplyDiscount(Service service)
        {
            var campaign = await UOW.Campaigns.FirstOrDefaultAsync(service.CampaignId);
            
            if (campaign == null)
            {
                return service;
            }
            
            service.PriceOfService *= (1 - campaign.DiscountAmount);
            
            return service;
        }
    }
}