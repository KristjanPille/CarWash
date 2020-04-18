using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Order : Order<Guid, AppUser>, IDomainEntityBaseMetadata, IDomainEntityUser<AppUser>
    {
    }

    public class Order<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        public DateTime DateAndTime { get; set; }
        
        public TKey WashId { get; set; }
        public ICollection<Wash>? Wash { get; set; }
        
        public string Comment { get; set; } = default!;
        public TKey AppUserId { get; set; }
        public TUser? AppUser { get; set; }
    }
}