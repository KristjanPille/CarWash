using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
     public class CarType : CarType<Guid, AppUser>, IDomainEntityBaseMetadata, IDomainEntityUser<AppUser>
     {
     }

     public class CarType<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
         where TKey : IEquatable<TKey>
         where TUser : AppUser<TKey>
     {
         [MaxLength(64)] public string Name { get; set; } = default!;
        public TKey AppUserId { get; set; }
        public TUser? AppUser { get; set; }
     }
}