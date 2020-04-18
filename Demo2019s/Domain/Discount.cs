using System;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Discount : Check<Guid, AppUser>, IDomainEntityBaseMetadata, IDomainEntityUser<AppUser>
    {
    }

    public class Discount<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        public TKey CheckId { get; set; }
        public Check? Check { get; set; }
        
        public int DiscountAmount { get; set; }
        
        public TKey WashId { get; set; }
        public Wash? Wash { get; set; }
        
        public TKey AppUserId { get; set; }
        public TUser? AppUser { get; set; }
    }
}