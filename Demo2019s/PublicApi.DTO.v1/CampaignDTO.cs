using System;

namespace PublicApi.DTO.v1
{
    public class CampaignDTO
    {
        public Guid Id { get; set; }
        public int CampaignId { get; set; }
        
        public int ServiceId { get; set; }
    }
}