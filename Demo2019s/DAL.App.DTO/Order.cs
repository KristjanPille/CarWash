using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class Order : IDomainEntityId
    {
        public DateTime DateAndTime { get; set; }
        
        public Guid ServiceId { get; set; }
        [JsonIgnore]
        public Service? Service { get; set; }

        
        public string Comment { get; set; } = default!;
        public Guid Id { get; set; }
    }
}