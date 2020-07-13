using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ee.itcollege.carwash.kristjan.Contracts.Domain;

namespace BLL.App.DTO
{
    public class IsInService : IDomainEntityId
    { 
        public Guid Id { get; set; }
        
        public Guid CarId { get; set; } = default!;
        [Display(Name = nameof(Car), ResourceType = typeof(Resources.BLL.App.DTO.IsInService))]
        [JsonIgnore]
        public Car? Car { get; set; }

        public Guid ServiceId { get; set; } = default!;
        [Display(Name = nameof(Car), ResourceType = typeof(Resources.BLL.App.DTO.IsInService))]
        [JsonIgnore]
        public Service? Service { get; set; }

        [Display(Name = nameof(From), ResourceType = typeof(Resources.BLL.App.DTO.IsInService))]
        public DateTime From { get; set; }
        
        [Display(Name = nameof(To), ResourceType = typeof(Resources.BLL.App.DTO.IsInService))]
        public DateTime To { get; set; }
    }
}