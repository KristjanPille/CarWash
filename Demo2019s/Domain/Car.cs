using System;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Car : Car<Guid>, IDomainEntity
    {
        
    }

    public class Car<TKey> : DomainEntity<TKey> 
        where TKey : struct, IEquatable<TKey>
    {
        public virtual TKey AppUserId { get; set; }
        public virtual AppUser? AppUser { get; set; }

        public int ModelMarkId { get; set; }
        public ModelMark? ModelMark { get; set; }
        public int CarTypeId { get; set; }
        public CarType? CarType { get; set; }

        public string LicenceNr { get; set; } = default!;
        public PersonCar? PersonCar { get; set; }
    }
}