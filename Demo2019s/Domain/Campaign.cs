namespace Domain
{
    public class Campaign
    {
        public int CampaignId { get; set; }
        
        public int ServiceId { get; set; }
        public Service? Service { get; set; }
        
        public string NameOfCampaign { get; set; } = default!;
    }
}