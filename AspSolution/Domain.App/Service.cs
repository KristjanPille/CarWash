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
        [MaxLength(64)]
        public string NameOfService { get; set; } = default!;

        public double PriceOfService { get; set; } = default!;
        
        public string? Description { get; set; }
        public int? Duration { get; set; }

        public Campaign? Campaign { get; set; }
        [JsonIgnore]
        public Guid? CampaignId { get; set; }
    }
}
