using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class Order : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public DateTime DateAndTime { get; set; }
        
        public Guid ServiceId { get; set; }
        [JsonIgnore]
        public Service? Service { get; set; }
    }
}