using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class Check : IDomainEntityId
    {
        public Guid Id { get; set; }

        public Guid NameId { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(64)]
        public string Name { get; set; } = default!;
        
        public Guid ServiceId { get; set; }
        [JsonIgnore]
        public Service? Service { get; set; }
        
        public DateTime DateTimeCheck { get; set; }
        
        public double AmountExcludeVat { get; set; }
        public double AmountWithVat { get; set; }
        public double Vat { get; set; }

        public string Comment { get; set; }  = default!;

    }
}