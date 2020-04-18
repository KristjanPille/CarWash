using System;
using BLL.App.DTO.Identity;
using Contracts.DAL.Base;
using Domain;

namespace BLL.App.DTO
{
    public class PersonCar : PersonCar<Guid>, IDomainBaseEntity
    {
    }

    public class PersonCar<TKey> : IDomainBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;


        public virtual TKey PersonId { get; set; } = default!;
        public virtual Person? Person { get; set; }

        public virtual TKey CarId { get; set; } = default!;
        public virtual Car? Car { get; set; }

        public TKey AppUserId { get; set; } = default!;
        public AppUser<TKey>? AppUser { get; set; }
    }
}