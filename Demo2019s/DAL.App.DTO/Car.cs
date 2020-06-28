using System;
using Contracts.Domain;
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

        /* 1-3
         1=> Small hatchback
         2=> Mid Sized car
         3=> Bigger suv
        */
        public int? CarSize{ get; set; } = default!;
        
        public Guid AppUserId { get; set; }
        [JsonIgnore]
        public AppUser? AppUser { get; set; }
    }
}