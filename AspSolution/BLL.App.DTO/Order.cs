using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BLL.App.DTO.Identity;
using ee.itcollege.carwash.kristjan.Contracts.Domain;

namespace BLL.App.DTO
{
    public class Order : IDomainEntityId
    { 
        public Guid Id { get; set; }
        
        public DateTime DateAndTime { get; set; } = default!;
        
        public Guid ServiceId { get; set; } = default!;
        
        [Display(Name = nameof(Service), ResourceType = typeof(Resources.BLL.App.DTO.Order))]
        [JsonIgnore]
        public Service? Service { get; set; }
        
        public Guid CarId { get; set; } = default!;
        
        [Display(Name = nameof(Car), ResourceType = typeof(Resources.BLL.App.DTO.Order))]
        [JsonIgnore]
        public Car? Car { get; set; }
        
        public Guid AppUserId { get; set; }
        
        [Display(Name = nameof(AppUser), ResourceType = typeof(Resources.BLL.App.DTO.Order))]
        [JsonIgnore]
        public AppUser? AppUser { get; set; }
        
        [Display(Name = nameof(From), ResourceType = typeof(Resources.BLL.App.DTO.Order))]
        public DateTime From { get; set; }
        
        [Display(Name = nameof(To), ResourceType = typeof(Resources.BLL.App.DTO.Order))]
        public DateTime To { get; set; }
    }
}