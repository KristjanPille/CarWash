using System;
using System.Collections.Generic;
using ee.itcollege.carwash.kristjan.Contracts.Domain;
using Domain.App;

namespace BLL.App.DTO
{
    public class Service : IDomainEntityId
    { 
        public Guid Id { get; set; }

        public string NameOfService { get; set; } = default!;
        
        public string? Description { get; set; }
        public double PriceOfService { get; set; } = default!;
        
        public int? Duration { get; set; }
        
        public Campaign? Campaign { get; set; }
        
        public Guid CampaignId { get; set; }
    }
}