using System;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Check : Check<Guid, AppUser>, IDomainEntityBaseMetadata, IDomainEntityUser<AppUser>
    {
    }

    public class Check<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        public TKey PersonId { get; set; }
        public Person? Person { get; set; }

        public TKey WashId { get; set; }
        public Wash? Wash { get; set; }
        
        public DateTime DateTimeCheck { get; set; }
        
        public int AmountExcludeVat { get; set; }
        public int AmountWithVat { get; set; }
        public int Vat { get; set; }
        
        public string Comment { get; set; }  = default!;
        
        public TKey AppUserId { get; set; }
        public TUser? AppUser { get; set; }
    }
}