using System;
using Domain;

namespace PublicApi.DTO.v1
{
    public class CampaignDTO
    {
        public int Id { get; set; }
        
        public string NameOfCampaign { get; set; } = default!;
    }
}