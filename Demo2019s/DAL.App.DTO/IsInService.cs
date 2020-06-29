using System;
using System.Text.Json.Serialization;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class IsInService: IDomainEntityId
    {
        public Guid Id { get; set; }

        public Guid CarId { get; set; } = default!;
        
        [JsonIgnore]
        public Car? Car { get; set; }

        public Guid ServiceId { get; set; } = default!;
        
        [JsonIgnore]
        public Service? Service { get; set; }
        
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}