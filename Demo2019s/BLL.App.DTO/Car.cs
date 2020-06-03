using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;
using System.Text.Json.Serialization;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class Car : IDomainEntityId
    { 
        public Guid Id { get; set; }

        public Guid AppUserId { get; set; }
        [JsonIgnore]
        public AppUser? AppUser { get; set; }

        public Guid ModelMarkId { get; set; }
        public ModelMark ModelMark { get; set; } = default!;
       
        public int? CarSize{ get; set; } = default!;
    }
}