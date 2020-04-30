using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class Service : Service<Guid>, IDomainBaseEntity
    {
    }

    public class Service<TKey> : IDomainBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        public string NameOfService { get; set; } = default!;
        
        public int CampaignId { get; set; }
        public ICollection<Campaign>? Campaign { get; set; }

        public TKey AppUserId { get; set; } = default!;
        public AppUser<TKey>? AppUser { get; set; }
    }
}