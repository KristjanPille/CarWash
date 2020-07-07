using System;
using carwash.kristjan.Contracts.Domain;
using DAL.App.DTO.Identity;
using System.Text.Json.Serialization;

namespace DAL.App.DTO
{
    public class Car : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid ModelMarkId { get; set; }
        
        [JsonIgnore]
        public ModelMark? ModelMark { get; set; }

        public Guid AppUserId { get; set; }
        [JsonIgnore]
        public AppUser? AppUser { get; set; }
    }
}