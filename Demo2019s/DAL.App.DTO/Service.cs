using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class Service: IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public string NameOfService { get; set; } = default!;
        
        [JsonIgnore]
        public Campaign? Campaign { get; set; }
        public Guid? CampaignId { get; set; }
    }
}