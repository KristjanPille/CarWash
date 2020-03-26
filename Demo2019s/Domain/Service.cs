using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class Service : DomainEntity
    {
        public int ServiceId { get; set; }
        [MaxLength(64)]
        public string NameOfService { get; set; } = default!;
        
        public int? CampaignId { get; set; }
        public ICollection<Campaign>? Campaign { get; set; }
    }
}