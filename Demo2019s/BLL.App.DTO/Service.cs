using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using Contracts.DAL.Base;
using Domain;

namespace BLL.App.DTO
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
        public TKey CampaignId { get; set; } = default!;
        public ICollection<Campaign>? Campaign { get; set; }

        public TKey AppUserId { get; set; } = default!;
        public AppUser<TKey>? AppUser { get; set; }
    }
}