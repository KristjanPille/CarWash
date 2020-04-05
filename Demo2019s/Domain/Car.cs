using System;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Car : Car<Guid>, IDomainEntity{
        
    }
    
    public class Car<TKey> : DomainEntity<TKey> 
        where TKey : struct, IEquatable<TKey>
    {
        public int CarId { get; set; }
   

        public virtual TKey PersonId { get; set; }
        public virtual Person? Person { get; set; }

        public int CarTypeId { get; set; }
        public CarType? CarType { get; set; }

        public string LicenceNr { get; set; } = default!;
    }
}