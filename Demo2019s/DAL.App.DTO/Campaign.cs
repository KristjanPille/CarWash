using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;
using Domain;

namespace DAL.App.DTO
{
    public class Campaign : Campaign<Guid>, IDomainBaseEntity
    {
    }
    
    public class Campaign<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public TKey CampaignId { get; set; }
        
        public TKey ServiceId { get; set; }
        public Service? Service { get; set; }
        
        public string NameOfCampaign { get; set; } = default!;

        public TKey AppUserId { get; set; } = default!;
        public AppUser<TKey>? AppUser { get; set; }
    }
}