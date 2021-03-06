using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.carwash.kristjan.Contracts.Domain;

namespace BLL.App.DTO
{
    public class LangStr : IDomainEntityId
    {
        public Guid Id { get; set; }

        public ICollection<LangStrTranslation>? Translations { get; set; }

        [InverseProperty(nameof(Campaign.NameOfCampaign))]
        public ICollection<Campaign>? CampaignNames { get; set; }
        
        //lisa siia
        [InverseProperty(nameof(Service.Description))]
        public ICollection<Service>? ServiceDescriptions { get; set; }
        [InverseProperty(nameof(Service.NameOfService))]
        public ICollection<Service>? ServiceNameOfServices { get; set; }
    }
}