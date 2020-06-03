using System;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class Payment : IDomainEntityId
    {

        public Guid PaymentMethodId { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        
        public Guid CheckId { get; set; }
        public Check? Check { get; set; }
        
        public int PaymentAmount { get; set; }
        public DateTime TimeOfPayment { get; set; }
        public Guid Id { get; set; }
    }
}