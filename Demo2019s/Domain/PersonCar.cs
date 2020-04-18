using System;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain

{
    public class PersonCar : PersonCar<Guid, AppUser>, IDomainEntityUser<AppUser>
    {
    }


    public class PersonCar<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey> 
        where TUser : AppUser<TKey>
    {
        public virtual TKey PersonId { get; set; }
        public virtual Person? Person { get; set; }

        public virtual TKey CarId { get; set; }
        public virtual Car? Car { get; set; }

        public TKey AppUserId { get; set; }
        public TUser? AppUser { get; set; }
    }
}