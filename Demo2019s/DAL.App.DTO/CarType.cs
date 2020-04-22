using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class CarType : CarType<Guid>, IDomainBaseEntity
    {
    }
    
    public class CarType<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;

        [MaxLength(64)] public string Name { get; set; } = default!;

        public TKey AppUserId { get; set; } = default!;
        public AppUser<TKey>? AppUser { get; set; }
    }
}