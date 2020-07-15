using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.carwash.kristjan.Contracts.Domain;

namespace PublicApi.DTO.v1
{
    public class Service : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        
        public Guid NameOfServiceId { get; set; }
        [MinLength(1)]
        [MaxLength(64)]
        public string NameOfService { get; set; } = default!;

        public double PriceOfService { get; set; } = default!;
        
        
        public Guid DescriptionId { get; set; }
        public string? Description { get; set; }
        
        public int? Duration { get; set; }
        
        public Guid? CampaignId { get; set; }
    }
}