using System;
using System.Collections.Generic;
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
        [Display(Name = nameof(FirstName), ResourceType = typeof(Resources.Domain.Person))]
        public string FirstName { get; set; } = default!;
        
        [MaxLength(64)] [MinLength(1)]
        [Display(Name = nameof(LastName), ResourceType = typeof(Resources.Domain.Person))]
        public string LastName { get; set; } = default!;
        public virtual string FirstLastName => FirstName + " " + LastName;
        public virtual TKey AppUserId{ get; set; }
        public virtual AppUser? AppUser { get; set; }
        
        public virtual ICollection<PersonCar>? Cars { get; set; }
        

        [MaxLength(64)]
        public string Email { get; set; }
        public int PhoneNr { get; set; }
    }
}