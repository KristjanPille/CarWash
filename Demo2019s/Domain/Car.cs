using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Car : Car<Guid, AppUser>, IDomainEntityBaseMetadata, IDomainEntityUser<AppUser>
    {
        
    }

    public class Car<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>

    {
        public TKey AppUserId { get; set; } = default!;
        public TUser? AppUser { get; set; }

        public TKey ModelMarkId { get; set; }
        public ModelMark? ModelMark { get; set; }
        public TKey CarTypeId { get; set; }
        public CarType? CarType { get; set; }

        public string LicenceNr { get; set; } = default!;
        
        public virtual ICollection<PersonCar>? PersonCars { get; set; }
    }
}