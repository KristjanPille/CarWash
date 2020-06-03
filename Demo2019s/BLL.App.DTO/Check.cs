using System;
using BLL.App.DTO.Identity;
using System.Text.Json.Serialization;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class Check : IDomainEntityId
    { 
        public Guid Id { get; set; }

        public Guid AppUserId { get; set; }
        [JsonIgnore]
        public AppUser? AppUser { get; set; }

        public virtual string NameOfCheck { get; set; } = default!;
        
        public int AmountExcludeVat { get; set; }
        
        public int AmountWithVat { get; set; }
        
        public int Vat { get; set; }
        
        public string Comment { get; set; }  = default!;
    }
}