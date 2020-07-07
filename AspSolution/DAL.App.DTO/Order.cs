using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using ee.itcollege.carwash.kristjan.Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class Order : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public DateTime DateAndTime { get; set; } = default!;
        
        public Guid ServiceId { get; set; } = default!;
        [JsonIgnore]
        public Service? Service { get; set; }
        
        public Guid CarId { get; set; } = default!;
        [JsonIgnore]
        public Car? Car { get; set; }
        
        public Guid AppUserId { get; set; }
        [JsonIgnore]
        public AppUser? AppUser { get; set; }
        
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}