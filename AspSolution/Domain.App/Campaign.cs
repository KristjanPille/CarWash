using System;
using ee.itcollege.carwash.kristjan.Domain.Base;


namespace Domain.App
{
    public class Campaign : DomainEntityIdMetadata
    {
        public string NameOfCampaign { get; set; } = default!;
        public string Description { get; set; } = default!;
        public double DiscountAmount { get; set; } = default!;
    }
}