using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class Payment : Payment<Guid>, IDomainBaseEntity
    {
    }
    
    public class Payment<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;

        public virtual TKey PersonId { get; set; }
        public virtual Person? Person { get; set; }

        public TKey PaymentMethodId { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        
        public TKey CheckId { get; set; }
        public Check? Check { get; set; }
        
        public int PaymentAmount { get; set; }
        public DateTime TimeOfPayment { get; set; }
        
        public TKey AppUserId { get; set; } = default!;
        public AppUser<TKey>? AppUser { get; set; }

    }
}