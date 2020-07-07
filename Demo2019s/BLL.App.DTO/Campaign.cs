using System;
using carwash.kristjan.Contracts.Domain;

namespace BLL.App.DTO
{
    public class Campaign : IDomainEntityId
    { 
        public Guid Id { get; set; }

        public virtual string NameOfCampaign { get; set; } = default!;
        
        public virtual string Description { get; set; } = default!;
        
        public double DiscountAmount { get; set; } = default!;
    }
}