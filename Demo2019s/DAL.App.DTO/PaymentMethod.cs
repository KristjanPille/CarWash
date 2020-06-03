using System;

using Contracts.Domain;

namespace DAL.App.DTO
{
    public class PaymentMethod: IDomainEntityId
    {
        public Guid Id { get; set; }
        public string PaymentMethodName { get; set; } = default!;
    }
}