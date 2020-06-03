using System;
using System.Collections.Generic;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class Service: IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public string NameOfService { get; set; } = default!;

        public Campaign? Campaign { get; set; }
        public Guid? CampaignId { get; set; }
    }
}