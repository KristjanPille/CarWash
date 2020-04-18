using System;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Payment : Payment<Guid, AppUser>, IDomainEntityBaseMetadata, IDomainEntityUser<AppUser>
    {
    }


    public class Payment<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
  
    {
        public virtual TKey PersonId { get; set; }
        public virtual Person? Person { get; set; }

        public TKey PaymentMethodId { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        
        public TKey CheckId { get; set; }
        public Check? Check { get; set; }
        
        public int PaymentAmount { get; set; }
        public DateTime TimeOfPayment { get; set; }
        
        public TKey AppUserId { get; set; }
        public TUser? AppUser { get; set; }
    }
}