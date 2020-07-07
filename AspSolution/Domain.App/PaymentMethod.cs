using System;
using ee.itcollege.carwash.kristjan.Domain.Base;

namespace Domain.App
{
    public class PaymentMethod : DomainEntityIdMetadata
    {
        public string PaymentMethodName { get; set; } = default!;
        
    }
}