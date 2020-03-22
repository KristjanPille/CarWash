using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Campaign : DomainEntity
    {
        public int CampaignId { get; set; }
        
        public int ServiceId { get; set; }
        public Service? Service { get; set; }
        
        public string NameOfCampaign { get; set; } = default!;
    }
}