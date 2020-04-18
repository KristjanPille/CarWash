using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [MaxLength(64)]
        public string NameOfService { get; set; } = default!;
        
        public TKey CampaignId { get; set; }
        public ICollection<Campaign>? Campaign { get; set; }

        public TKey AppUserId { get; set; } = default!;
        public AppUser<TKey>? AppUser { get; set; }
    }
}