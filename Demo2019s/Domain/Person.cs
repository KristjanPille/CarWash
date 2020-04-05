using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Versioning;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Person : Person<Guid>, IDomainEntity{
    
    }
    public class Person<TKey> : DomainEntity<TKey>
        where TKey : struct, IEquatable<TKey>
    {
        [MaxLength(64)] [MinLength(1)]
        //[Display(Name = nameof(Name), ResourceType = typeof(Resources.Domain.Person))]
        public string Name { get; set; } = default!;
        
        public virtual TKey AppUserId{ get; set; }
        public virtual AppUser? AppUser { get; set; }
        
        public int PersonTypeId { get; set; }
        public PersonType? PersonType { get; set; }

        [MaxLength(64)]
        public string Email { get; set; } = default!;
        public int PhoneNr { get; set; }
    }
}