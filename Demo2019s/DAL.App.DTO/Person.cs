using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    
    public class Person : Person<Guid>, IDomainBaseEntity
    {
        
    }
    public class Person<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>

    {
        public TKey Id { get; set; } = default!;

        public virtual string FirstName { get; set; } = default!;
        public virtual string LastName { get; set; } = default!;
        
        public virtual string FirstLastName => FirstName + " " + LastName;

        [MinLength(1)][MaxLength(64)]
        public string Email { get; set; }
        [MinLength(1)][MaxLength(32)]
        public int PhoneNr { get; set; }

        public virtual ICollection<PersonCar>? PersonCars { get; set; }
        

        public virtual TKey AppUserId{ get; set; } = default!;
        public virtual AppUser<TKey>? AppUser { get; set; }
    }

    public class PersonDisplay
    {
        public Guid Id { get; set; } = default!;
        public virtual string FirstName { get; set; } = default!;
        public virtual string LastName { get; set; } = default!;

    }
    
    
}