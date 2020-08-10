﻿using System;
using System.Threading.Tasks;
using BLL.App.Mappers;
using ee.itcollege.carwash.kristjan.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using Car = BLL.App.DTO.Car;
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
        
        public async Task<Service> ApplyDiscount(Service service, double priceOfService)
        {
            Campaign? campaign = null;
            if (service.CampaignId != null)
            {
                campaign = await UOW.Campaigns.FirstOrDefaultAsync((Guid) service.CampaignId);
            }

            if (campaign == null)
            {
                return service;
            }
            
            service.PriceOfService = priceOfService * (1 - campaign.DiscountAmount);
            
            return service;
        }

        public async Task<double> GetServicePrice(PublicApi.DTO.v1.Car car, Guid serviceId)
        {
            var service = await UOW.Services.FirstOrDefaultAsync(serviceId);

            var bllcar = await UOW.Cars.FirstOrDefaultAsync(car.Id);
            
            var modelMark = await UOW.ModelMarks.FirstOrDefaultAsync(bllcar.ModelMarkId);

            return service.PriceOfService + (modelMark.ModelMarkSize * 1.5);
        }
        
    }
}