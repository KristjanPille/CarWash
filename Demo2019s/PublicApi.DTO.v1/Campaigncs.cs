using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class Campaign : CampaignEdit
    {
        public string NameOfCampaign { get; set; } = default!;
    }
    
    public class CampaignCreate
    {
        [MinLength(1)] [MaxLength(64)] 
        public string NameOfCampaign { get; set; } = default!;
    }
    
    public class CampaignEdit : CampaignCreate
    {
        public Guid Id { get; set; }
    }
    
}