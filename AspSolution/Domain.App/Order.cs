using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.App.Identity;
using ee.itcollege.carwash.kristjan.Domain.Base;

namespace Domain.App
{
    public class Order : DomainEntityIdMetadataUser<AppUser>
    {
        public DateTime DateAndTime { get; set; } = default!;
        
        public Guid ServiceId { get; set; } = default!;
        [JsonIgnore]
        public Service? Service { get; set; }
        
        public Guid CarId { get; set; } = default!;
        [JsonIgnore]
        public Car? Car { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime From { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime To { get; set; }
    }
}