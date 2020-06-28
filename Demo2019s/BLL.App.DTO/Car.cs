using System;
using System.Text.Json.Serialization;
using BLL.App.DTO.Identity;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class Car : IDomainEntityId
    { 
        public Guid Id { get; set; }

        public Guid? ModelMarkId { get; set; }
        
        [JsonIgnore]
        public ModelMark? ModelMark { get; set; }
       
        public int? CarSize{ get; set; } = default!;
        
        public Guid AppUserId { get; set; }
  
        [JsonIgnore]
        public AppUser? AppUser { get; set; }
    }
}