using System;
using DAL.Base;

namespace Domain
{
    public class Car : Car<Guid>{
        
    }
    
    public class Car<TKey> : DomainEntity<TKey> 
        where TKey : struct, IEquatable<TKey>
    {
        //car
        public int CarId { get; set; }
        public Guid PersonId { get; set; }
        public Person? Person { get; set; }
        public int CarTypeId { get; set; }
        public CarType? CarType { get; set; }

        public string LicenceNr { get; set; } = default!;
    }
}