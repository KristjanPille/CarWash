using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ee.itcollege.carwash.kristjan.Domain.Base;

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
        
        [DataType(DataType.DateTime)]
        public DateTime From { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime To { get; set; }
    }
}
