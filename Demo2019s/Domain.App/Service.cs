using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Service : DomainEntityIdMetadata
    {
        //Service has washes, interior cleanings, car protecting etc, specification will be done in Description
        [MaxLength(64)]
        public string NameOfService { get; set; } = default!;

        public double PriceOfService { get; set; } = default!;
        
        public Campaign? Campaign { get; set; }
        public Guid? CampaignId { get; set; }
    }
}
