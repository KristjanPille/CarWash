using System;
using System.Text.Json.Serialization;
using Domain.Base;

namespace Domain.App
{
    public class IsInService : DomainEntityIdMetadata
    {
        public Guid CarId { get; set; } = default!;
        [JsonIgnore]
        public Car? Car { get; set; }

        public Guid ServiceId { get; set; } = default!;
        [JsonIgnore]
        public Service? Service { get; set; }
        
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
    }
}
