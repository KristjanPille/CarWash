using System;
using ee.itcollege.carwash.kristjan.Contracts.Domain;

namespace PublicApi.DTO.v1
{
    public class PaymentMethod : IDomainEntityId
    {
        public Guid Id { get; set; }
        public Guid PaymentMethodNameId { get; set; }
        public string PaymentMethodName { get; set; } = default!;
    }
}