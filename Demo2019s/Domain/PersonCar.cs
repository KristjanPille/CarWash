using System;
using DAL.Base;

namespace Domain

{
    public class PersonCar : PersonCar<Guid>
    {
    }

    public class PersonCar<TKey> : DomainEntity<TKey> 
        where TKey : struct, IEquatable<TKey>
    {
        public virtual TKey PersonId { get; set; }
        public virtual Person? Person { get; set; }

        public virtual TKey CarId { get; set; }
        public virtual Car? Car { get; set; }

    }
}