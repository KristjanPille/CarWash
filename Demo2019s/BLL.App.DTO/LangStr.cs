using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using carwash.kristjan.Contracts.Domain;

namespace BLL.App.DTO
{
    public class LangStr : IDomainEntityId
    {
        public Guid Id { get; set; }

        public ICollection<LangStrTranslation>? Translations { get; set; }

        [InverseProperty(nameof(Campaign.NameOfCampaign))]
        public ICollection<Campaign>? CampaignNames { get; set; }

    }
}