using System;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Payment : DomainEntityIdMetadataUser<AppUser>
    {

        public Guid PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; } = default!;
        
        public Guid CheckId { get; set; }
        public Check? Check { get; set; }
        
        public double PaymentAmount { get; set; }
        public DateTime TimeOfPayment { get; set; }
    }
}
