using System;
using System.Text.Json.Serialization;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class Payment : IDomainEntityId
    {
        public Guid Id { get; set; }
        
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
        public DateTime TimeOfPayment { get; set; }

        public string? PayPalEmail { get; set; }
        
        public string? CreditCardNumber { get; set; }
        
        public string? ExpMonth { get; set; }
        public string? ExpYear { get; set; }
        
        public int? CVV { get; set; }
    }
}