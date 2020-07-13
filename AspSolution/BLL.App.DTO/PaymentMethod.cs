using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.carwash.kristjan.Contracts.Domain;

namespace BLL.App.DTO
{
    public class PaymentMethod : IDomainEntityId
    { 
        public Guid Id { get; set; }

        [Display(Name = nameof(PaymentMethodName), ResourceType = typeof(Resources.BLL.App.DTO.PaymentMethod))]
        public string PaymentMethodName { get; set; } = default!;
    }
}