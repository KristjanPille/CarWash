using System;
using System.Collections.Generic;
using Contracts.Domain;
using Domain.App;

namespace BLL.App.DTO
{
    public class Service : IDomainEntityId
    { 
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; }

        public double PriceOfService { get; set; } = default!;
        
        public Campaign? Campaign { get; set; }
        
        public Guid? CampaignId { get; set; }

    }
}