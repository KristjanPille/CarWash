using System;

using Contracts.Domain;

namespace DAL.App.DTO
{
    public class PaymentMethod: IDomainEntityId
    {
        public Guid Id { get; set; }
        public string PaymentMethodName { get; set; } = default!;
        
        public string? PayPalEmail { get; set; }
        
        public string? CreditCardNumber { get; set; }
        
        public string? ExpMonth { get; set; }
        public string? ExpYear { get; set; }
        
        public int? CVV { get; set; }
    }
}