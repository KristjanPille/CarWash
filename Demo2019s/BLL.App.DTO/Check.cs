using System;
using BLL.App.DTO.Identity;
using System.Text.Json.Serialization;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class Check : IDomainEntityId
    { 
        public Guid Id { get; set; }

        public Guid ServiceId { get; set; }
        [JsonIgnore]
        public Service? Service { get; set; }
        
        public Guid CarId { get; set; }
        [JsonIgnore]
        public Car? Car { get; set; }
        
        public Guid AppUserId { get; set; }
        [JsonIgnore]
        public AppUser? AppUser { get; set; }
        
        public DateTime DateTimeCheck { get; set; }
        
        public double PaymentAmount { get; set; }
    }
}