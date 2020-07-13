using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BLL.App.DTO.Identity;
using ee.itcollege.carwash.kristjan.Contracts.Domain;

namespace BLL.App.DTO
{
    public class Car : IDomainEntityId
    { 
        public Guid Id { get; set; }

        public Guid ModelMarkId { get; set; }
        
        [Display(Name = nameof(ModelMark), ResourceType = typeof(Resources.BLL.App.DTO.Car))]
        [JsonIgnore]
        public ModelMark? ModelMark { get; set; }

        public Guid AppUserId { get; set; }
  
        [Display(Name = nameof(AppUser), ResourceType = typeof(Resources.BLL.App.DTO.Car))]
        [JsonIgnore]
        public AppUser? AppUser { get; set; }
    }
}