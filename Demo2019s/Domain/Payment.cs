using System;
using DAL.Base;

namespace Domain
{
    public class Payment : Payment<Guid>
    {
        
    }

    public class Payment<TKey> : DomainEntity<TKey> 
        where TKey : struct, IEquatable<TKey>

    {
        public int PaymentId { get; set; }
        
        public virtual TKey PersonId { get; set; }
        public virtual Person? Person { get; set; }

        public int PaymentMethodId { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        
        public int CheckId { get; set; }
        public Check? Check { get; set; }
        
        public int PaymentAmount { get; set; }
        public DateTime TimeOfPayment { get; set; }
    }
}