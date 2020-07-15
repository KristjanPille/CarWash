using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using ee.itcollege.carwash.kristjan.Contracts.Domain;

namespace DAL.App.DTO
{
    public class LangStr : IDomainEntityId
    {
        public Guid Id { get; set; }

        public ICollection<LangStrTranslation>? Translations { get; set; }

        [InverseProperty(nameof(Campaign.NameOfCampaign))]
        [JsonIgnore]
        public ICollection<Campaign>? CampaignNames { get; set; }
        
        [InverseProperty(nameof(Service.Description))]
        [JsonIgnore]
        public ICollection<Service>? ServiceDescriptions { get; set; }
        
        [InverseProperty(nameof(Service.NameOfService))]
        [JsonIgnore]
        public ICollection<Service>? ServiceNameOfServices { get; set; }

        [InverseProperty(nameof(PaymentMethod.PaymentMethodName))]
        [JsonIgnore]
        public ICollection<PaymentMethod>? PaymentMethodNames { get; set; }
    }
}