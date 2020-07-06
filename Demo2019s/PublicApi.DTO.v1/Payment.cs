using System;
using System.Text.Json.Serialization;
using Contracts.Domain;

namespace PublicApi.DTO.v1
{
    public class Payment : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; }

        public Guid PaymentMethodId { get; set; } = default!;

        public Guid CheckId { get; set; } = default!;
        
        public Guid CarId { get; set; } = default!;

        public Guid ServiceId { get; set; } = default!;

        public double PaymentAmount { get; set; } = default!;
        public DateTime TimeOfPayment { get; set; }

        public string? PayPalEmail { get; set; }
        
        public string? CreditCardNumber { get; set; }
        
        public string? ExpMonth { get; set; }
        public string? ExpYear { get; set; }
        
        public int? CVV { get; set; }
        
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}