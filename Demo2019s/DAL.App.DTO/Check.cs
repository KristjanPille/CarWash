using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
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
        
        public DateTime DateTimeCheck { get; set; }
        
        public double PaymentAmount { get; set; }
        
        public Guid AppUserId { get; set; }
        [JsonIgnore]
        public AppUser? AppUser { get; set; }
    }
}