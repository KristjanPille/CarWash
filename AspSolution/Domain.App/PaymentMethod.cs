using System;
using ee.itcollege.carwash.kristjan.Domain.Base;

namespace Domain.App
{
    public class PaymentMethod : DomainEntityIdMetadata
    {
        public Guid PaymentMethodNameId { get; set; }
        public LangStr PaymentMethodName { get; set; } = default!;
        
    }
}