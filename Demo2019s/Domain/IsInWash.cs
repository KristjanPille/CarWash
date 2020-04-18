using System;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class IsInWash : IsInWash<Guid, AppUser>, IDomainEntityBaseMetadata, IDomainEntityUser<AppUser>
    {
    }

    public class IsInWash<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        public TKey CarId { get; set; } = default!;
        public Car? Car { get; set; }

        public TKey PersonId { get; set; }
        public Person? Person { get; set; }
        
        public TKey WashId { get; set; }
        public Wash? Wash { get; set; }
        
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
        public TKey AppUserId { get; set; }
        public TUser? AppUser { get; set; }
    }
}