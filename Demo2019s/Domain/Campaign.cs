using System;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Campaign : Campaign<Guid, AppUser>, IDomainEntityBaseMetadata, IDomainEntityUser<AppUser>
    {
    }
    

    public class Campaign<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        public TKey AppUserId { get; set; } = default!;
        public TUser? AppUser { get; set; }

        public string NameOfCampaign { get; set; } = default!;
    }
}