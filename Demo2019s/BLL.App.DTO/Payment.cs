using System;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class Payment : IDomainEntityId
    { 
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; }
        
        public Guid PaymentMethodId { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        
        public Guid CheckId { get; set; }
        public Check? Check { get; set; }
        
        public int PaymentAmount { get; set; }
        public DateTime TimeOfPayment { get; set; }
    }
}