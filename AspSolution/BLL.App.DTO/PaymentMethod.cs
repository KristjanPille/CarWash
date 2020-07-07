using System;
using ee.itcollege.carwash.kristjan.Contracts.Domain;

namespace BLL.App.DTO
{
    public class PaymentMethod : IDomainEntityId
    { 
        public Guid Id { get; set; }

        public string PaymentMethodName { get; set; } = default!;
    }
}