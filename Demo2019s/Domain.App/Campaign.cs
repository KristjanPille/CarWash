using System;
using DAL.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Campaign : DomainEntityIdMetadata
    {
        public string NameOfCampaign { get; set; } = default!;
        public string Description { get; set; } = default!;
        public double DiscountAmount { get; set; } = default!;
    }
}