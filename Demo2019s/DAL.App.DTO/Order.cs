using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;
using Domain;

namespace DAL.App.DTO
{
    public class Order : Order<Guid>, IDomainBaseEntity
    {
    }
    
    public class Order<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public DateTime DateAndTime { get; set; }
        
        public TKey WashId { get; set; }
        public ICollection<Wash>? Wash { get; set; }
        
        public string Comment { get; set; } = default!;
        
        public TKey AppUserId { get; set; } = default!;
        public AppUser<TKey>? AppUser { get; set; }

    }
}