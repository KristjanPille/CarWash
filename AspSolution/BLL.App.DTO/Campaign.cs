using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.carwash.kristjan.Contracts.Domain;

namespace BLL.App.DTO
{
    public class Campaign : IDomainEntityId
    { 
        public Guid Id { get; set; }

        [Display(Name = nameof(NameOfCampaign), ResourceType = typeof(Resources.BLL.App.DTO.Campaign))]
        public virtual string NameOfCampaign { get; set; } = default!;
        
        [Display(Name = nameof(Description), ResourceType = typeof(Resources.BLL.App.DTO.Campaign))]
        
        public virtual string Description { get; set; } = default!;
        
        [Display(Name = nameof(DiscountAmount), ResourceType = typeof(Resources.BLL.App.DTO.Campaign))]
        
        public double DiscountAmount { get; set; } = default!;
    }
}