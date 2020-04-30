using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    
    public class Service : Service<Guid, AppUser>, IDomainEntityBaseMetadata, IDomainEntityUser<AppUser>
    {
    }

    public class Service<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>

    {
        [MaxLength(64)]
        public string NameOfService { get; set; } = default!;
        public int CampaignId { get; set; }
        public ICollection<Campaign>? Campaign { get; set; }
        
        public TKey AppUserId { get; set; }
        public TUser? AppUser { get; set; }
    }
}