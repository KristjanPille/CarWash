using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using carwash.kristjan.Contracts.Domain;

namespace DAL.App.DTO
{
    public class LangStr : IDomainEntityId
    {
        public Guid Id { get; set; }

        public ICollection<LangStrTranslation>? Translations { get; set; }

        [InverseProperty(nameof(Campaign.NameOfCampaign))]
        [JsonIgnore]
        public ICollection<Campaign>? CampaignNames { get; set; }

    }
}