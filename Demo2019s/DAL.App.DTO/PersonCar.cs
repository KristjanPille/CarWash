using System;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class PersonCar : PersonCar<Guid>, IDomainBaseEntity
    {
    }

    public class PersonCar<TKey> : IDomainBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;

        public virtual TKey PersonId { get; set; }
        public virtual Person? Person { get; set; }

        public virtual TKey CarId { get; set; }
        public virtual Car? Car { get; set; }

        public TKey AppUserId { get; set; } = default!;
        public AppUser<TKey>? AppUser { get; set; }
    }

}