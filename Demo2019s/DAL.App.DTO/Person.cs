using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;
using Domain;

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