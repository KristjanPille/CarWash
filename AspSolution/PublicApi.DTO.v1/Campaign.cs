using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.carwash.kristjan.Contracts.Domain;

namespace PublicApi.DTO.v1
{
    public class Campaign: IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid NameOfCampaignId { get; set; }
        public string NameOfCampaign { get; set; } = default!;
        
        public Guid DescriptionId { get; set; }
        public string Description { get; set; } = default!;
        
        public double DiscountAmount { get; set; } = default!;
    }

}