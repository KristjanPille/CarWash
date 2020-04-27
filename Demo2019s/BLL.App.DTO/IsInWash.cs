using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class IsInWash : IsInWash<Guid>, IDomainBaseEntity
    {
    }
    
    public class IsInWash<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public TimeSpan From { get; set; }
        
        public TimeSpan To { get; set; }
        

        public TKey AppUserId { get; set; } = default!;
        public AppUser<TKey>? AppUser { get; set; }
    }
    
}