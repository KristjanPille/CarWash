using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Wash : Wash<Guid, AppUser>, IDomainEntityUser<AppUser>
    {
    }


    public class Wash<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey> 
        where TUser : AppUser<TKey>
    {
        public TKey CheckId { get; set; }
        public ICollection<Check>? Check { get; set; }
        
        public TKey WashTypeId { get; set; }
        public WashType? WashType { get; set; }
        
        public TKey OrderId { get; set; }
        public Order? Order { get; set; }
        //nameOfWashType implies to exterior or interior wash
        public string NameOfWashType { get; set; } = default!;
        
        public TKey AppUserId { get; set; }
        public TUser? AppUser { get; set; }
    }
}