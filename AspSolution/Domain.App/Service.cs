using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ee.itcollege.carwash.kristjan.Domain.Base;

namespace Domain.App
{
    public class Service : DomainEntityIdMetadata
    {
        //Service has washes, interior cleanings, car protecting etc, specification will be done in Description
        public Guid NameOfServiceId { get; set; }
        [MaxLength(64)]
        public LangStr NameOfService { get; set; } = default!;

        public double PriceOfService { get; set; } = default!;
        
        public Guid DescriptionId { get; set; }
        public LangStr? Description { get; set; }
        public int? Duration { get; set; }

        public Campaign? Campaign { get; set; }
        [JsonIgnore]
        public Guid? CampaignId { get; set; }
    }
}
