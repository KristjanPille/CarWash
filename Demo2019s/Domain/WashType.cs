using System;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class WashType : Was<Guid, AppUser>, IDomainEntityUser<AppUser>
    {
    }


    public class Was<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey> 
        where TUser : AppUser<TKey>
    {
        public string NameOfWash { get; set; } = default!;
        public TKey AppUserId { get; set; }
        public TUser? AppUser { get; set; }
    }
}