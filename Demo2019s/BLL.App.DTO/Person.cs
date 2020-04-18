using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;
using Contracts.DAL.Base;
using Domain;

namespace BLL.App.DTO
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
        
        public String LicenceNr { get; set; }

        public virtual TKey AppUserId{ get; set; } = default!;
        public virtual AppUser<TKey>? AppUser { get; set; }
    }
}