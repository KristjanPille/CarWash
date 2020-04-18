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
    public class Person : Person<Guid, AppUser>, IDomainEntityBaseMetadata, IDomainEntityUser<AppUser>
    {
        
    }

    public class Person<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser: AppUser<TKey>

    {
        [MaxLength(64)] [MinLength(1)]
        [Display(Name = nameof(FirstName), ResourceType = typeof(Resources.Domain.Person))]
        public string FirstName { get; set; } = default!;
        
        [MaxLength(64)] [MinLength(1)]
        [Display(Name = nameof(LastName), ResourceType = typeof(Resources.Domain.Person))]
        public string LastName { get; set; } = default!;
        public virtual string FirstLastName => FirstName + " " + LastName;
        
        public virtual ICollection<PersonCar>? Cars { get; set; }
        
        [MaxLength(64)]
        public string Email { get; set; }
        public int PhoneNr { get; set; }
        
        public TKey AppUserId { get; set; }
        public TUser? AppUser { get; set; }
    }
}