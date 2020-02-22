namespace Domain
{
    public class Campaign
    {
        public int campaignID { get; set; }
        
        public int serviceID { get; set; }
        public Service Service { get; set; }
        
        public string nameOfCampaign { get; set; }
    }
}