using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Service
    {
        public int ServiceId { get; set; }
        [MaxLength(64)]
        public string NameOfService { get; set; } = default!;
        
        public int CampaignId { get; set; }
        public ICollection<Campaign>? Campaign { get; set; }
    }
}