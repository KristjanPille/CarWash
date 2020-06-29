using System;
using System.Collections.Generic;
using Contracts.Domain;
using Domain.App;

namespace BLL.App.DTO
{
    public class Service : IDomainEntityId
    { 
        public Guid Id { get; set; }

        public string NameOfService { get; set; } = default!;
        
        public double PriceOfService { get; set; } = default!;
        
        public Campaign? Campaign { get; set; }
        
        public Guid CampaignId { get; set; }
    }
}