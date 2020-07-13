using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BLL.App.DTO.Identity;
using ee.itcollege.carwash.kristjan.Contracts.Domain;

namespace BLL.App.DTO
{
    public class Payment : IDomainEntityId
    { 
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; }
  
        [Display(Name = nameof(AppUser), ResourceType = typeof(Resources.BLL.App.DTO.Payment))]
        [JsonIgnore]
        public AppUser? AppUser { get; set; }
        
        public Guid PaymentMethodId { get; set; }
        
        [Display(Name = nameof(PaymentMethod), ResourceType = typeof(Resources.BLL.App.DTO.Payment))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        [JsonIgnore]
        public PaymentMethod? PaymentMethod { get; set; }
        
        public Guid CheckId { get; set; } = default!;
        
        [Display(Name = nameof(Check), ResourceType = typeof(Resources.BLL.App.DTO.Payment))]
        [JsonIgnore]
        public Check? Check { get; set; }
        
        public Guid CarId { get; set; }
        
        [Display(Name = nameof(Car), ResourceType = typeof(Resources.BLL.App.DTO.Payment))]
        [JsonIgnore]
        public Car? Car { get; set; }

        public Guid ServiceId { get; set; } = default!;
        
        [Display(Name = nameof(Service), ResourceType = typeof(Resources.BLL.App.DTO.Payment))]
        [JsonIgnore]
        public Service? Service { get; set; }
        
        
        [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [MaxLength(32)]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        [Display(Name = nameof(PaymentAmount), ResourceType = typeof(Resources.BLL.App.DTO.Payment))]
        public double PaymentAmount { get; set; } = default!;
        
        [Display(Name = nameof(TimeOfPayment), ResourceType = typeof(Resources.BLL.App.DTO.Payment))]
        public DateTime TimeOfPayment { get; set; }
            
        [Display(Name = nameof(PayPalEmail), ResourceType = typeof(Resources.BLL.App.DTO.Payment))]

        public string? PayPalEmail { get; set; }
        
        [Display(Name = nameof(CreditCardNumber), ResourceType = typeof(Resources.BLL.App.DTO.Payment))]
        public string? CreditCardNumber { get; set; }
        
        [Display(Name = nameof(ExpMonth), ResourceType = typeof(Resources.BLL.App.DTO.Payment))]
        public string? ExpMonth { get; set; }
        
        [Display(Name = nameof(ExpYear), ResourceType = typeof(Resources.BLL.App.DTO.Payment))]
        public string? ExpYear { get; set; }
        
        [Display(Name = nameof(CVV), ResourceType = typeof(Resources.BLL.App.DTO.Payment))]
        public int? CVV { get; set; }
        
        [Display(Name = nameof(From), ResourceType = typeof(Resources.BLL.App.DTO.Payment))]
        public DateTime From { get; set; }
        
        [Display(Name = nameof(To), ResourceType = typeof(Resources.BLL.App.DTO.Payment))]
        public DateTime To { get; set; }
    }
}