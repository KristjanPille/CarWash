using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.App.Identity;
using ee.itcollege.carwash.kristjan.Domain.Base;

namespace Domain.App
{
    public class Payment : DomainEntityIdMetadataUser<AppUser>
    {
        public Guid PaymentMethodId { get; set; }
        [JsonIgnore]
        public PaymentMethod? PaymentMethod { get; set; }
        
        public Guid CheckId { get; set; } = default!;
        [JsonIgnore]
        public Check? Check { get; set; }
        
        public Guid CarId { get; set; }
        [JsonIgnore]
        public Car? Car { get; set; }

        public Guid ServiceId { get; set; } = default!;
        [JsonIgnore]
        public Service? Service { get; set; }
        
        public double PaymentAmount { get; set; } = default!;
        
        [DataType(DataType.DateTime)]
        public DateTime? TimeOfPayment { get; set; }

        public string? PayPalEmail { get; set; }
        
        public string? CreditCardNumber { get; set; }
        
        public string? ExpMonth { get; set; }
        public string? ExpYear { get; set; }
        
        public int? CVV { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime From { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime To { get; set; }
    }
}
