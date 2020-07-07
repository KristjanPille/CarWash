using System;
using ee.itcollege.carwash.kristjan.Contracts.Domain;

namespace DAL.App.DTO
{
    public class Campaign : IDomainEntityId
    {
        public Guid Id { get; set; }
        public string NameOfCampaign { get; set; } = default!;
        
        public string Description { get; set; } = default!;
        
        public double DiscountAmount { get; set; } = default!;
    }
}