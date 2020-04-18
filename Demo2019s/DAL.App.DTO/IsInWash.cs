using System;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;
using Domain;

namespace DAL.App.DTO
{
    public class IsInWash : IsInWash<Guid>, IDomainBaseEntity
    {
    }
    
    public class IsInWash<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;

        public TKey CarId { get; set; } = default!;
        public Car? Car { get; set; }

        public TKey PersonId { get; set; }
        public Person? Person { get; set; }
        
        public int WashId { get; set; }
        public Wash? Wash { get; set; }
        
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
        public TKey AppUserId { get; set; } = default!;
        public AppUser<TKey>? AppUser { get; set; }
    }
}