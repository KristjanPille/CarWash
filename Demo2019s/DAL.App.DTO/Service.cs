using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class Service: IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public double PriceOfService { get; set; } = default!;
        
        public string NameOfService { get; set; } = default!;
        
        public string? Description { get; set; }
        
        public int? Duration { get; set; }
        
        [JsonIgnore]
        public Campaign? Campaign { get; set; }
        public Guid CampaignId { get; set; }
    }
}