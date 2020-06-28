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

        public Guid ServiceId { get; set; }
        [JsonIgnore]
        public Service? Service { get; set; }

        
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
    }
}