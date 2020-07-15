using System;
using ee.itcollege.carwash.kristjan.Domain.Base;


namespace Domain.App
{
    public class Campaign : DomainEntityIdMetadata
    {
        public Guid NameOfCampaignId { get; set; }
        public LangStr NameOfCampaign { get; set; } = default!;
        
        public Guid DescriptionId { get; set; }
        public LangStr Description { get; set; } = default!;
        public double DiscountAmount { get; set; } = default!;
    }
}