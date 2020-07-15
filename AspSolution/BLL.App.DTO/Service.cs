using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.carwash.kristjan.Contracts.Domain;
using Domain.App;

namespace BLL.App.DTO
{
    public class Service : IDomainEntityId
    { 
        public Guid Id { get; set; }

        public Guid NameOfServiceId { get; set; }
        [Display(Name = nameof(NameOfService), ResourceType = typeof(Resources.BLL.App.DTO.Service))]
        public virtual string NameOfService { get; set; } = default!;
        
        public Guid DescriptionId { get; set; }
        [Display(Name = nameof(Description), ResourceType = typeof(Resources.BLL.App.DTO.Service))]
        public virtual string? Description { get; set; }
        public double PriceOfService { get; set; } = default!;
        
        public int? Duration { get; set; }
        
        public Campaign? Campaign { get; set; }
        
        public Guid? CampaignId { get; set; }
    }
}