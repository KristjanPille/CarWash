using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class Wash : Wash<Guid>, IDomainBaseEntity
    {
    }

    public class Wash<TKey> : IDomainBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;

        public TKey CheckId { get; set; }
        public ICollection<Check>? Check { get; set; }
        
        public TKey WashTypeId { get; set; }
        public WashType? WashType { get; set; }
        
        public TKey OrderId { get; set; }
        public Order? Order { get; set; }

        public string NameOfWashType { get; set; } = default!;

        public TKey AppUserId { get; set; } = default!;
        public AppUser<TKey>? AppUser { get; set; }
    }
}