using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        //public TKey PersonId { get; set; }
        [MaxLength(64)]
        [MinLength(1)]
        public string Name { get; set; } = default!;

        [MaxLength(36)] 
        public TKey AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
        
        public int PersonTypeId { get; set; }
        public PersonType? PersonType { get; set; }

        [MaxLength(64)]
        public string Email { get; set; } = default!;
        public int PhoneNr { get; set; }
    }
}