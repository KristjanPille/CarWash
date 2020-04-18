using System;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class PaymentMethod : PaymentMethod<Guid, AppUser>, IDomainEntityBaseMetadata, IDomainEntityUser<AppUser>
    {
    }

    public class PaymentMethod<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        public string PaymentMethodName { get; set; } = default!;
        public TKey AppUserId { get; set; }
        public TUser? AppUser { get; set; }
    }
}